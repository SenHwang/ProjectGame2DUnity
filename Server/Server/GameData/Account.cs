using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Server.Data
{
    class Account
    {        

        /// <summary>
        /// Đọc file binary data player 
        /// nếu file chưa có thì khởi tạo 1 file binary với name truyền vào
        /// có rồi thì gọi ra rea3 về dưới dạng player có id  = -1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Player Read(string name)
        {
            if (!File.Exists(GAMEDATA.APPLICATION_PATH + GAMEDATA.ACCOUNT_PATH + name + GAMEDATA.ACCOUNT_TYPE))
            {                
                return Write(name);
            }
            else
            {   //read file binary
                using (Stream stream = File.Open(GAMEDATA.APPLICATION_PATH + GAMEDATA.ACCOUNT_PATH + name + GAMEDATA.ACCOUNT_TYPE, FileMode.Open))
                {
                    var binaryFormatter = new BinaryFormatter();

                    Player playerRead = (Player)binaryFormatter.Deserialize(stream);
                    Console.WriteLine("Read user: " + playerRead.username);
                    playerRead.id = -1;
                    return playerRead;
                }                     
            }
        }

        //writefile binary new file new location user
        public static Player Write(string name)
        {

            using (Stream stream = File.Open(GAMEDATA.APPLICATION_PATH + GAMEDATA.ACCOUNT_PATH + name + GAMEDATA.ACCOUNT_TYPE, FileMode.Create))
            {
                var binaryFormatter = new BinaryFormatter();
                Vector3 v3;
                v3.x = 0; v3.y = 0; v3.z = 0;
                Player p = new Player(-1, name, v3);
                for(int i = 0; i < p.Equipment.Length; i++)
                {
                    if(i == 0)
                    {
                        p.Equipment[0] = "1";// khởi tạo equipment body = 1 cho user mới
                    }
                    else
                    {
                        p.Equipment[i] = "";
                    }
                }

                p.role = GameData.Role.Player; //khởi tạo role player cho user mới

                binaryFormatter.Serialize(stream, p);
                stream.Close();
                Console.WriteLine("Write user: " + p.username);
                return p;
            }
        }

        public static void SavePlayer(Player _player)
        {
            using (Stream stream = File.Open(GAMEDATA.APPLICATION_PATH + GAMEDATA.ACCOUNT_PATH + _player.username + GAMEDATA.ACCOUNT_TYPE, FileMode.OpenOrCreate))
            {
                var binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream, _player);
                stream.Close();
                Console.WriteLine("Saving user: " + _player.username);
            }
        }
    }
}
