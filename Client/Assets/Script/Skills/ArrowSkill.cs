using Assets.Script.Types;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ArrowSkill : MonoBehaviour
{
    public static ArrowSkill instance;
    public string sprite;
    public Vector3 locationShoot;
    public float length;
    public int idOwner;
    public string idSkill;
    public float speed;
    /// <summary>
    /// up:12
    /// down:0
    /// left:4
    /// right:8
    /// </summary>
    public int face;
    private float timeAlive = 0;

    private Vector3 possOld;
    //Start is called before the first frame update
    void Start()
    {
        possOld = this.transform.position;
        if (face == 12)//up
        {
            this.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 1);
            this.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (face == 0)//down
        {
            this.GetComponent<Transform>().rotation = new Quaternion(0, 0, 0, 1);
            this.GetComponent<SpriteRenderer>().flipY = false;
        }
        else if (face == 4)//left
        {
            this.GetComponent<Transform>().Rotate(0, 0, 90);
            this.GetComponent<SpriteRenderer>().flipY = true;
        }
        else if (face == 8)//right
        {
            this.GetComponent<Transform>().Rotate(0, 0, 90);
            this.GetComponent<SpriteRenderer>().flipY = false;
        }
    }

    private void Update()
    {
        if (this.gameObject.active == false) return;
        if (timeAlive >= 2f)
        {
            if(possOld == this.transform.position)
                this.gameObject.SetActive(false);
        }
        timeAlive += Time.deltaTime;
        possOld = this.transform.position;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name.IndexOf("Map") >= 0)
            {
                ClientSend.SkillGetHit(idOwner, idSkill, -1, GAME.MAP_START);
                return;
            }

            if (collision.gameObject.name.IndexOf("Player") >= 0)
            {
                if (idOwner != Client.instance.myId && collision.gameObject.GetComponent<Player>().id != Client.instance.myId ) return;
                //nếu player bị hit là thằng trong team thì bỏ qua
                if (Party.isMemberTeam(idOwner, collision.gameObject.GetComponent<Player>().id))
                    return;              

                ClientSend.SkillGetHit(idOwner, idSkill, collision.gameObject.GetComponent<Player>().id, GAME.MAP_START);
                
            }
            else if (collision.gameObject.name.IndexOf("NPC") >= 0)
            {
                ClientSend.SkillGetHit(idOwner, idSkill, -1, GAME.MAP_START);
            }else
                return;

            // DestroyObject();  
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

}
