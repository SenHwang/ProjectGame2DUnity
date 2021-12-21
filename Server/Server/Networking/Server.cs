using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;

namespace Server
{
    class Server
    {
        public static int MaxPlayers { get; private set; }
        public static int Port { get; private set; }
        public static Dictionary<int, Client> clients = new Dictionary<int, Client>();
        public delegate void PacketHandle(int _fromClient, Packet _packet);
        public static Dictionary<int, PacketHandle> packetHandlers;

        private static TcpListener tcpListener;
        private static UdpClient udpListener;
        public static void Start(int _maxplayers, int _port)
        {
            MaxPlayers = _maxplayers;
            Port = _port;            

            TaskQueue.listMessage.Add("Server: Starting server...");
            Console.WriteLine("Starting server...");

            InitializeServerData();

            tcpListener = new TcpListener(IPAddress.Any, Port);
            tcpListener.Start();
            tcpListener.BeginAcceptTcpClient(TCPConnectCallback, null);

            udpListener = new UdpClient(Port);
            udpListener.BeginReceive(UDPReceiveCallback, null);

            TaskQueue.listMessage.Add($"Server: Server started on {Port}");
            Console.WriteLine($"Server started on {Port}");
        }


        private static void TCPConnectCallback(IAsyncResult _result)
        {
            TcpClient _client = tcpListener.EndAcceptTcpClient(_result);
            tcpListener.BeginAcceptTcpClient(new AsyncCallback(TCPConnectCallback), null);

            TaskQueue.listMessage.Add($"Server: Incoming connection from {_client.Client.RemoteEndPoint}...");
            Console.WriteLine($"Incoming connection from {_client.Client.RemoteEndPoint}...");

            for (int i = 0; i < MaxPlayers; i++)
            {
                if (clients[i].tcp.socket == null)  
                {
                    clients[i].tcp.Connect(_client);
                    return;
                }
            }

            TaskQueue.listMessage.Add($"Server: {_client.Client.RemoteEndPoint} fail to connect: Server full!");
            Console.WriteLine($"{_client.Client.RemoteEndPoint} fail to connect: Server full!");
        }

        private static void UDPReceiveCallback(IAsyncResult _result)
        {
            try
            {
                IPEndPoint _clientEndPoint = new IPEndPoint(IPAddress.Any, 0);
                byte[] _data = udpListener.EndReceive(_result, ref _clientEndPoint);
                udpListener.BeginReceive(UDPReceiveCallback, null);

                if (_data.Length < 4)
                {
                    return;
                }

                using (Packet _packet = new Packet(_data))
                {
                    int _clientId = _packet.ReadInt();
                    //if (_clientId == 0)
                    //{
                    //    return;
                    //}

                    if (clients[_clientId].udp.endPoint == null)
                    {
                        clients[_clientId].udp.Connect(_clientEndPoint);
                        return;
                    }

                    if (clients[_clientId].udp.endPoint.ToString() == _clientEndPoint.ToString())
                    {
                        clients[_clientId].udp.HandleData(_packet);
                    }
                }
            }
            catch (Exception _ex)
            {
                TaskQueue.listMessage.Add($"Server: Error receiving UDP data: {_ex}");
                Console.WriteLine($"Error receiving UDP data: {_ex}");

                udpListener.Close();
                udpListener = new UdpClient(Port);
                udpListener.BeginReceive(UDPReceiveCallback, null);
            }
        }

        public static void SendUDPData(IPEndPoint _clientEndPoint, Packet _packet)
        {
            try
            {
                if (_clientEndPoint != null)
                {
                    udpListener.BeginSend(_packet.ToArray(), _packet.Length(), _clientEndPoint, null, null);
                }
            }
            catch (Exception _ex)
            {
                TaskQueue.listMessage.Add($"Server: Error sending data to {_clientEndPoint} via UDP: {_ex}");
                Console.WriteLine($"Error sending data to {_clientEndPoint} via UDP: {_ex}");
            }
        }


        private static void InitializeServerData()
        {
            for (int i = 0; i < MaxPlayers; i++)
            {
                clients.Add(i, new Client(i));
            }

            packetHandlers = new Dictionary<int, PacketHandle>()
            {
                { (int)ClientPackets.welcomeReceived,ServerHandle.WelcomeReceived},
                { (int)ClientPackets.playerMovement,ServerHandle.PlayerMovement},
                { (int)ClientPackets.playerTeleportMap,ServerHandle.PlayerTeleMap},
                { (int)ClientPackets.playerEquipment,ServerHandle.PlayerEquipment},
                { (int)ClientPackets.playerMessage,ServerHandle.MessageFromClientReceived},
                { (int)ClientPackets.playerRQArrow,ServerHandle.RQArrowFromClient},
                { (int)ClientPackets.playerUpdateStats,ServerHandle.PlayerRQUpdateStats},
                { (int)ClientPackets.playerSkillHitted,ServerHandle.PlayerSkillHitted},
                { (int)ClientPackets.adminSavingMap,ServerHandle.AdminSendSaveMap},
                { (int)ClientPackets.adminSavingNPC,ServerHandle.AdminSendSaveNPC},
                { (int)ClientPackets.adminCallItem,ServerHandle.AdminRequestItem},
                { (int)ClientPackets.spawnAnimation,ServerHandle.ClientCreateAnimation},
            };

            TaskQueue.listMessage.Add("Server: Initialize packet!");
            Console.WriteLine("Initialize packet!");

        }


    }
}
