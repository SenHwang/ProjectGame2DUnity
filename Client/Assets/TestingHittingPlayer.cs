using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingHittingPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<CircleCollider2D>().enabled = false;
    }

    //// Update is called once per frame
    //void Update()
    //{

    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name.IndexOf("Map") >= 0)            
                return;            

            if (collision.gameObject.name.IndexOf("Player") >= 0)
            {
                //ClientSend.SkillGetHit(idOwner, idSkill, collision.gameObject.GetComponent<Player>().id, GAME.MAP_START);
                Debug.Log($"Hitting {collision.gameObject.GetComponent<Player>().name}");
                this.GetComponent<CircleCollider2D>().enabled = false;
                if (Party.isMemberTeam(Client.instance.myId, collision.gameObject.GetComponent<Player>().id))
                    return;

                ClientSend.SkillGetHit(Client.instance.myId, "", collision.gameObject.GetComponent<Player>().id, GAME.MAP_START);
            }
            else if(collision.gameObject.name.IndexOf("NPC") >= 0)
            {
                //ClientSend.SkillGetHit(idOwner, idSkill, collision.gameObject.GetComponent<Player>().id, GAME.MAP_START);
                Debug.Log($"Hitting {collision.gameObject.GetComponent<MonsterEquipAnim>().NPCID-1}");
                this.GetComponent<CircleCollider2D>().enabled = false;
                UIManagerFunc.instance.EnableObject(UIManagerFunc.instance.storyObject);
                NPC nPC = GAME.npcs[collision.gameObject.GetComponent<MonsterEquipAnim>().NPCID-1];
                nPC.OnGetHandHit(0);
                //StoryGameFunc.SetStory(nPC.story, nPC.nameNPC, nPC.description);
            }
            //esle if nếu dmg monster thì gửi về server update monster sau đó server callback lại client để update list monster in map

        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }
}
