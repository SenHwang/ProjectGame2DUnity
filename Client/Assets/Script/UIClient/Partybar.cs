using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Partybar : MonoBehaviour
{

    public int numPlayer = 0;

    private GameObject PlayerInParty;

    private List<GameObject> members = new List<GameObject>();
    private int numOnChange;
    // Start is called before the first frame update
    void Start()
    {
        PlayerInParty = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/BarHealthParty");
        numOnChange = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            if (GameManager.players.Count == 0) return;
            if (GameManager.players[Client.instance.myId].party.Member.Count != numOnChange)
            {
                foreach (GameObject m in members)
                {
                    Destroy(m.gameObject);
                }
                members = new List<GameObject>();

                for (int i = 0; i < GameManager.players[Client.instance.myId].party.Member.Count; i++)
                {
                    //nếu là local player thì k cần hiện lên
                    if (Client.instance.myId == GameManager.players[Client.instance.myId].party.Member[i].id) continue;

                    Vector3 possMem = new Vector3();
                    possMem.y = -(i * 40);
                    GameObject memBar = Instantiate(PlayerInParty, possMem, new Quaternion()) as GameObject;
                    memBar.transform.SetParent(this.transform);
                    memBar.transform.position = possMem;
                    memBar.gameObject.GetComponent<RectTransform>().anchoredPosition3D = possMem;
                    memBar.transform.localScale = new Vector3(1, 1, 1);

                    MiniPlayer player = GameManager.players[Client.instance.myId].party.Member[i];
                    float bHP = (float)player.hpLeft / player.hp;
                    float bMP = (float)player.mpLeft / player.mp;
                    if (bHP <= 0)
                    {
                        bMP = bHP = 1;
                    }

                    memBar.transform.Find("HPBar").gameObject.transform.Find("HP").transform.localScale = new Vector3(bHP, 1f);
                    memBar.transform.Find("MPBar").gameObject.transform.Find("MP").transform.localScale = new Vector3(bMP, 1f);
                    memBar.transform.Find("NameBar").gameObject.GetComponent<Text>().text = GameManager.players[Client.instance.myId].party.Member[i].username;
                    members.Add(memBar);
                }
                numOnChange = GameManager.players[Client.instance.myId].party.Member.Count;
            }

            if (members.Count > 0)
            {
                for (int i = 0; i < members.Count; i++)
                {
                    string name = members[i].transform.Find("NameBar").gameObject.GetComponent<Text>().text;

                    if (name != GameManager.players[Client.instance.myId].username)
                    {
                        MiniPlayer player = GameManager.players[Client.instance.myId].party.Member.Find(x => x.username == name);

                        float bHP = (float)player.hpLeft / player.hp;
                        float bMP = (float)player.mpLeft / player.mp;
                        if (bHP <= 0)
                        {
                            bMP = bHP = 1;
                        }
                        members[i].transform.Find("HPBar").gameObject.transform.Find("HP").transform.localScale = new Vector3(bHP, 1f);
                        members[i].transform.Find("MPBar").gameObject.transform.Find("MP").transform.localScale = new Vector3(bMP, 1f);
                    }
                }

            }
        }catch
        {

        }
        
    }
}
