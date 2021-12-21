using Assets.Script.Types;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Inventory;

public class ChatInput : MonoBehaviour
{
    public static ChatInput instance;
    public InputField inputMessage;
    public GameObject messagePrefab;
    public Transform messageContainer;
    private GameObject bubbleMessage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    private void Update()
    {
        PlayerControler.isWriting = inputMessage.isFocused;
    }


    public void SendMessage()
    {
        bubbleMessage = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/bubbleChat");
        if (inputMessage.text == "")
        {
            ChatInput.instance.inputMessage.text = null;
            PlayerControler.isWriting = false;
            ChatInput.instance.inputMessage.enabled = false;
            ChatInput.instance.inputMessage.DeactivateInputField();
            return;
        }

        //string message = GameManager.players[Client.instance.myId].username + " :"+inputMessage.text;
        //string message ="name_player :" + inputMessage.text;
        if (inputMessage.text.IndexOf("/") == 0)
        {
            string cmd = inputMessage.text.Split(' ')[0];

            switch (cmd.ToLower())
            {
                #region emoji cmd
                case "/emoji":
                    if (GameManager.players[Client.instance.myId].gameObject.transform.Find("bubbleChat(Clone)"))
                    {
                        Destroy(GameManager.players[Client.instance.myId].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
                    }
                    System.Random r = new System.Random();
                    int emojiGot = r.Next(0, 16);
                    //GameManager.players[Client.instance.myId].gameObject.GetComponent<TestingEmoji>().emoji = r.Next(0, 16).ToString();
                    inputMessage.text = null;
                    string messageSendParse = "     \n";
                    Vector3 possMessBubble = GameManager.players[Client.instance.myId].gameObject.transform.position;
                    possMessBubble.x += 0.09f;
                    possMessBubble.y += 0.67f;
                    GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
                    messBubble.GetComponent<BubbleChat>().SetupEmoji(emojiGot);
                    messBubble.GetComponent<BubbleChat>().Setup(messageSendParse);
                    messBubble.transform.SetParent(GameManager.players[Client.instance.myId].gameObject.transform);

                    ChatRec mess = new ChatRec();
                    mess.message.IsEmoji = true;
                    mess.message.text = emojiGot.ToString();
                    mess.idClient = Client.instance.myId;
                    mess.chatType = ChatType.Map;

                    ClientSend.PlayerSendMessageToServer(mess);//(-1, $"{Client.instance.myId}|{emojiGot}");
                    break;

                #endregion

                #region help cmd
                case "/help":
                    UpdateChatBar(9, "Help Commands!!!\n" +
                                     "Chat: Enter to chat and Esc to exit chat\n" +
                                     "      /t chat team\n" +
                                     "      /c chat clan\n" +
                                     "      /g chat All\n" +
                                     "      /dm someone chat DM into someone\n" +
                                     "Emoji: /emoji random emoji :))\n" +
                                     "Invite player:\n" +
                                     "      /invite name");
                    break;
                #endregion

                #region Chat cmd
                case "/t":
                    string messParty = inputMessage.text.Replace(cmd + " ", "");// lọc cmd rồi lấy chuỗi sau cmd
                    messParty = GameManager.players[Client.instance.myId].username + " :" + messParty;
                    Message(ChatType.Party, messParty);
                    break;
                case "/c":
                    UpdateChatBar(1, "System: cmd not support yet!");
                    break;
                case "/g":
                    string messSend = inputMessage.text.Replace(cmd + " ", "");// lọc cmd rồi lấy chuỗi sau cmd
                    messSend = GameManager.players[Client.instance.myId].username + " :" + messSend;
                    Message(ChatType.World, messSend);
                    break;
                case "/dm":
                    break;
                #endregion

                #region Party cmd
                case "/invite":
                    string playerGetName = inputMessage.text.Replace(cmd + " ", "");// get name player
                    if(playerGetName == GameManager.players[Client.instance.myId].username)
                    {
                        UpdateChatBar(1, "System: Không thể invite bản thân!");
                        break;
                    }                        
                    ChatRec chatRQ = new ChatRec();
                    chatRQ.idClient = Client.instance.myId;
                    chatRQ.chatType = ChatType.InvitePlayer;
                    chatRQ.message.text = playerGetName;
                    ClientSend.PlayerSendMessageToServer(chatRQ);
                    break;
                case "/leave":
                    ChatRec rqLeave = new ChatRec();
                    rqLeave.idClient = Client.instance.myId;
                    rqLeave.chatType = ChatType.InvitePlayer;
                    rqLeave.message.text = "/leave";
                    rqLeave.message.line = -1;
                    ClientSend.PlayerSendMessageToServer(rqLeave);
                    break;
                #endregion

                #region Admin
                case "/editmap":
                    //TODO check role rồi mới mở map edit
                    // show pannel editmap và hide UI game 
                    MapUpdateUI.RootUI.transform.Find("Canvas").gameObject.SetActive(false);
                    MapUpdateUI.RootUI.transform.Find("EditMap").gameObject.SetActive(true);
                    TestClickMap.blockList = Map.maps[GAME.MAP_START].block;
                    TestClickMap.lengthMap = Map.maps[GAME.MAP_START].lengthMap;                  

                    break;
                case "/admin":
                    MapUpdateUI.RootUI.transform.Find("Canvas").gameObject.SetActive(false);
                    MapUpdateUI.RootUI.transform.Find("Admin").gameObject.SetActive(true);


                    //TODO check role rồi mới mở admin panel
                    if (GameManager.players[Client.instance.myId].role.Equals(Role.Admin))
                    {
                        //TODO Show admin sence
                    }
                    break;

                case "/callitem": 
                    //if(!admin) break;
                    string getID = inputMessage.text.Replace(cmd + " ", "");// get name player
                    int numItem; 
                    int.TryParse(getID, out numItem);
                    try
                    {
                        ClientSend.AdminRequestCallItem(numItem, 1, ClientSend.TypeItemCall.Item);
                        break;
                    }
                    catch
                    {
                        Debug.Log($"ChatInput \"/callitem\"; numItem {numItem}");
                    }
                    break;
                case "/callskill":
                    //if(!admin) break;
                    string getIDSkill = inputMessage.text.Replace(cmd + " ", "");// get name player
                    int numSkill;
                    int.TryParse(getIDSkill, out numSkill);
                    try
                    {
                        ClientSend.AdminRequestCallItem(numSkill, 0, ClientSend.TypeItemCall.Skill);
                        break;
                    }
                    catch
                    {
                        Debug.Log($"ChatInput case \"/callskill\"; numItem {numSkill}");
                    }
                    break;

                case "/callmoney":
                    //if(!admin) break;
                    string numget = inputMessage.text.Replace(cmd + " ", "");// get name player
                    int numMoney;
                    int.TryParse(numget, out numMoney);
                    try
                    {
                        //Send request add money to server
                        ClientSend.AdminRequestCallItem(0, numMoney, ClientSend.TypeItemCall.Item);
                        break;
                    }
                    catch(Exception ex)
                    {
                        Debug.Log($"ChatInput \"/callmoney\"; ex: "+ ex);
                    }
                    break;

                #endregion
                default:
                    UpdateChatBar(1, "System: Lệnh không tìn thấy!");
                    break;
            }

            ChatInput.instance.inputMessage.text = null;
            PlayerControler.isWriting = false;
            ChatInput.instance.inputMessage.enabled = false;
            ChatInput.instance.inputMessage.DeactivateInputField();
            return;
        }
        else
        {
            string message = GameManager.players[Client.instance.myId].username + " :" + inputMessage.text;
            Message(ChatType.Map, message);
        }


        ChatInput.instance.inputMessage.text = null;
        PlayerControler.isWriting = false;
        ChatInput.instance.inputMessage.enabled = false;
        ChatInput.instance.inputMessage.DeactivateInputField();

    }


    public void Message(ChatType chatType, string inputMessage)
    {
        string[] message = inputMessage.Split(char.Parse(" "));
        message = message.Where(o => o != "").ToArray();
        if (string.Join(" ", message).Length > 120)
        {
            UpdateChatBar(1, "System: Tin nhắn quá 120 ký tự!");
            this.inputMessage.text = null;
            return;
        }

        string[] line = new string[6]{
                "",
                "",
                "",
                "",
                "",
                ""
            };
        int lineGot = 0;

        for (int i = 0; i < message.Length; i++)
        {
            if (line[lineGot].Length + message[i].Length < 30)
            {
                line[lineGot] += message[i] + " ";
            }
            else if (line[lineGot].Length + message[i].Length > 30)
            {
                if (message[i].Length > 15)
                {
                    int maxLength = 30 - line[lineGot].Length;
                    line[lineGot] += message[i].Substring(0, maxLength);
                    lineGot++;
                    string leftMessageI = message[i].Substring(maxLength);

                    int lineI = leftMessageI.Length / 30;
                    int leftI = leftMessageI.Length % 30;
                    if (lineI == 0 && leftI > 0)
                    {
                        line[lineGot] += leftMessageI + " ";
                        continue;
                    }
                    for (int z = 0; z < lineI; z++)
                    {
                        if (leftI > 0 && z == lineI - 1)
                        {
                            line[lineGot] += leftMessageI.Substring(z * 30, (z * 30) + leftI);
                        }
                        else
                        {
                            line[lineGot] += leftMessageI.Substring(z * 30, (z * 30) + 30);
                            lineGot++;
                        }

                    }
                }
                else
                {
                    lineGot++;
                    line[lineGot] += message[i] + " ";
                }
            }
            else if (line[lineGot].Length + message[i].Length == 30)
            {
                line[lineGot] += message[i] + " ";
                lineGot++;
            }
        }

        string messageSend = "";
        int count = 0;
        for (int i = 0; i < line.Length; i++)
        {
            if (line[i] != "")
            {
                messageSend += line[i] + Environment.NewLine;
                count++;
            }
        }


        //bubble
        //if (GameManager.players[Client.instance.myId].gameObject.transform.Find("bubbleChat(Clone)"))
        //{
        //    Destroy(GameManager.players[Client.instance.myId].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
        //}
        //int charPos = messageSend.IndexOf(" :");
        //string messageSendParse = messageSend.Substring(charPos + 2);
        //messageSendParse = messageSendParse.Remove(messageSendParse.Length - 3);
        //Vector3 possMessBubble = GameManager.players[Client.instance.myId].gameObject.transform.position;
        //possMessBubble.x += 0.09f;
        //possMessBubble.y += 0.67f;
        //GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
        //messBubble.GetComponent<BubbleChat>().Setup(messageSendParse);
        //messBubble.transform.SetParent(GameManager.players[Client.instance.myId].gameObject.transform);
        //end bubble


        ChatRec mess = new ChatRec();
        mess.message.text = messageSend;
        mess.message.line = count;
        mess.idClient = Client.instance.myId;
        mess.chatType = chatType;
        ClientSend.PlayerSendMessageToServer(mess);//(count, messageSend);
        //UpdateChatBar(count, messageSend);


        //this.inputMessage.text = null;
        //PlayerControler.isWriting = false;
        //this.inputMessage.DeactivateInputField();
    }

    public void UpdateChatBar(int count, string message)
    {
        if (count == -1)
        {
            UpdateChatEmoji(count, message);
            return;
        }
        GameObject messagePb = Instantiate(messagePrefab) as GameObject;
        messagePb.GetComponent<Text>().text = message;
        messagePb.transform.SetParent(messageContainer);
        messagePb.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 16 * count);
        messagePb.transform.localScale = Vector3.one;

        string[] name1 = message.Split(':');
        string who = name1[0].Remove(name1[0].Length - 1);
        message = message.Remove(0, who.Length + 2);


        Player user = GameManager.players.SingleOrDefault(x => x.Value.username == who).Value;
        if (!user) return;

        if (GameManager.players[user.id] == null)
            return;
        if (GameManager.players[user.id].mapID != GAME.MAP_START) return;


        if (GameManager.players[user.id].gameObject.transform.Find("bubbleChat(Clone)"))
        {
            Destroy(GameManager.players[user.id].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
        }

        message = message.Remove(message.Length - 3);
        Vector3 possMessBubble = GameManager.players[user.id].gameObject.transform.position;
        possMessBubble.x += 0.09f;
        possMessBubble.y += 0.67f;
        GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
        messBubble.GetComponent<BubbleChat>().Setup(message);
        messBubble.transform.SetParent(GameManager.players[user.id].gameObject.transform);
    }

    public void UpdateChatEmoji(int count, string message)
    {
        if (count != -1) return;
        bubbleMessage = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/bubbleChat");
        string[] messageSplit = message.Split('|');

        if (GameManager.players[int.Parse(messageSplit[0])] == null) return;
        if (GameManager.players[int.Parse(messageSplit[0])].mapID != GAME.MAP_START) return;

        if (GameManager.players[int.Parse(messageSplit[0])].gameObject.transform.Find("bubbleChat(Clone)"))
        {
            Destroy(GameManager.players[int.Parse(messageSplit[0])].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
        }
        //GameManager.players[Client.instance.myId].gameObject.GetComponent<TestingEmoji>().emoji = r.Next(0, 16).ToString();
        inputMessage.text = null;
        string messageSendParse = "     \n";
        Vector3 possMessBubble = GameManager.players[int.Parse(messageSplit[0])].gameObject.transform.position;
        possMessBubble.x += 0.09f;
        possMessBubble.y += 0.67f;
        GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
        messBubble.GetComponent<BubbleChat>().SetupEmoji(int.Parse(messageSplit[1]));
        messBubble.GetComponent<BubbleChat>().Setup(messageSendParse);
        messBubble.transform.SetParent(GameManager.players[int.Parse(messageSplit[0])].gameObject.transform);
    }

    public void UpdateChatBar(ChatRec mess)
    {
        if (mess.message.IsEmoji)
        {
            UpdateChatEmoji(mess);
            return;
        }

        string message = mess.message.text;
        int count = mess.message.line;


        GameObject messagePb = Instantiate(messagePrefab) as GameObject;
        messagePb.GetComponent<Text>().text = mess.message.text;
        messagePb.transform.SetParent(messageContainer);
        messagePb.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 16 * count);
        messagePb.transform.localScale = Vector3.one;

        if (mess.chatType != ChatType.Map) return;
        string[] name1 = message.Split(':');
        message = message.Remove(0, name1[0].Length + 1);

        Player user = GameManager.players.SingleOrDefault(x => x.Value.username == GameManager.players[mess.idClient].username).Value;
        if (!user) return;

        if (GameManager.players[user.id] == null)
            return;
        if (GameManager.players[user.id].mapID != GAME.MAP_START) return;


        if (GameManager.players[user.id].gameObject.transform.Find("bubbleChat(Clone)"))
        {
            Destroy(GameManager.players[user.id].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
        }

        bubbleMessage = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/bubbleChat");
        message = message.Remove(message.Length - 3);
        Vector3 possMessBubble = GameManager.players[user.id].gameObject.transform.position;
        possMessBubble.x += 0.09f;
        possMessBubble.y += 0.67f;
        GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
        messBubble.GetComponent<BubbleChat>().Setup(message);
        messBubble.transform.SetParent(GameManager.players[user.id].gameObject.transform);
       }

    public void UpdateChatEmoji(ChatRec mess)
    {
        string message = mess.message.text;
        int count = mess.message.line;

        message = message.Replace("[Map]", "");
        if (!mess.message.IsEmoji) return;
        if (mess.chatType != ChatType.Map) return;
        bubbleMessage = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/bubbleChat");

        if (GameManager.players[mess.idClient] == null) return;
        if (GameManager.players[mess.idClient].mapID != GAME.MAP_START) return;

        if (GameManager.players[mess.idClient].gameObject.transform.Find("bubbleChat(Clone)"))
        {
            Destroy(GameManager.players[mess.idClient].gameObject.transform.Find("bubbleChat(Clone)").gameObject);
        }
        //GameManager.players[Client.instance.myId].gameObject.GetComponent<TestingEmoji>().emoji = r.Next(0, 16).ToString();
        inputMessage.text = null;
        string messageSendParse = "     \n";
        Vector3 possMessBubble = GameManager.players[mess.idClient].gameObject.transform.position;
        possMessBubble.x += 0.09f;
        possMessBubble.y += 0.67f;
        GameObject messBubble = Instantiate(bubbleMessage, possMessBubble, new Quaternion()) as GameObject;
        messBubble.GetComponent<BubbleChat>().SetupEmoji(int.Parse(message));
        messBubble.GetComponent<BubbleChat>().Setup(messageSendParse);
        messBubble.transform.SetParent(GameManager.players[mess.idClient].gameObject.transform);
    }

}
