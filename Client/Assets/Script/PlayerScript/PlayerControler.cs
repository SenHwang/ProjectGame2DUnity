using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour {

    /// <summary>
    /// Key Bind của game 
    /// này sẽ lưu file vào resource. đọc khi mở game/ save key bind
    /// </summary>
    KeyCode[] keyBind = { KeyCode.W,//up
                          KeyCode.S,//down
                          KeyCode.A,//leff
                          KeyCode.D,//right
                          KeyCode.Tab,//switch player/npc
                          KeyCode.LeftAlt,//hand/weapon attack
                          KeyCode.Alpha1,//slot skill 1
                          KeyCode.Alpha2,//slot skill 2
                          KeyCode.Alpha3,//slot skill 3
                          KeyCode.Alpha4,//slot skill 4
                          KeyCode.Alpha5,//slot skill 5
                          KeyCode.Alpha6,//slot skill 6
                          KeyCode.Alpha7,//slot skill 7
                          KeyCode.Alpha8,//slot skill 8
                          KeyCode.Alpha9,//slot skill 9
                          KeyCode.Alpha0,//slot skill 10
                          KeyCode.C,//character info
                          KeyCode.I,//inventory bag
                          KeyCode.J,//inventory skill
                        };

    public static bool isWriting = false;

    public Rigidbody2D rb;
    public GameObject knife;
    public GameObject skillTest;

    /// <summary>
    /// testing punching
    /// </summary>
    public GameObject punchingObject;

    public int spriteStop = 0;
    public int speedAnim = 3;
    float counterHit = 0;
    bool isHit = false;

    //cache x và y lần cuối
    float cacheX, cacheY;

    private void FixedUpdate()
    {
        speedAnim = this.GetComponent<Player>().speedAnim;
        spriteStop = this.GetComponent<Player>().spriteStop;
        SendInputToServer();
        SkillCall.Update();
        if (isHit)
        {        
            if (counterHit <= 0.8f)
            {
                if (counterHit >= .3f)
                {                    
                    punchingObject.GetComponent<CircleCollider2D>().enabled = false;
                    this.GetComponent<Player>().IsPunching = false;
                    punchingObject.GetComponent<CircleCollider2D>().offset = new Vector2(0, -1.2f);
                }
                counterHit += Time.deltaTime;                
            }
            else
            {
                counterHit = 0;
                isHit = false;
            }
        }

    }


    private void SendInputToServer()
    {
        if(isWriting)
        {
            return;
        }
        float x = 0;
        float y = 0;
        if (Input.GetKey(keyBind[0]))
        {
            y += 1f;
            spriteStop = 12;
        }
        else if (Input.GetKey(keyBind[1]))
        {
            y -= 1f;
            spriteStop = 0;
        }

        if (Input.GetKey(keyBind[2]))
        {
            x -= 1f;
            spriteStop = 4;
        }
        else if (Input.GetKey(keyBind[3]))
        {
            x += 1f;
            spriteStop = 8;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            if (!ChatInput.instance.inputMessage.isFocused && isWriting == false)
            {
                ChatInput.instance.inputMessage.Select();
                ChatInput.instance.inputMessage.ActivateInputField();
                ChatInput.instance.inputMessage.enabled = true;
            }
        }

        if (Input.GetKey(keyBind[4]))
        {
            // tìm list monster hoặc player và target vào

        }

        if (Input.GetKey(KeyCode.Mouse1)) //chuột phải sẽ nằm
        {
            if (!isHit)
            {
                isHit = true;
                this.GetComponent<Player>().IsDead = !this.GetComponent<Player>().IsDead;
            }

        }

        //punching
        if (Input.GetKey(keyBind[5]))
        {
            if (!isHit)
            {
                //di chuyển lên 1 khoảng theo dir player
                Vector2 punchVec = new Vector2(0,-1.2f);

                if (spriteStop == 12)
                    punchVec.y += 1;
                else if (spriteStop == 0)
                    punchVec.y -= 1;
                else if (spriteStop == 4)
                    punchVec.x -= 0.7f;
                else if (spriteStop == 8)
                    punchVec.x += 0.7f;

                punchingObject.GetComponent<CircleCollider2D>().offset = punchVec;
                //enable circle collider 2d của puching
                punchingObject.GetComponent<CircleCollider2D>().enabled = true;
                this.GetComponent<Player>().IsPunching = true;
                //addAudio skill fireball
                AudioManager.SpawnAudioSRCByText("Damage2");
                isHit = true;

            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (!isHit)
            {
                isHit = true;
                Debug.Log("Skill");
                //Skill.skillCall ;
                ClientSend.RequestServerToCreateNewArrow(Client.instance.myId, spriteStop, 10, 0.45f, GameManager.players[Client.instance.myId].GetComponent<Transform>().position);
                
            }         
        }


        // skill bind 1
        if (Input.GetKey(keyBind[6]))
        {
            SkillSlot.OnUseSkill(0);
        }
        else if (Input.GetKey(keyBind[7]))// skill bind 2
        {
            SkillSlot.OnUseSkill(1);        
        }
        else if (Input.GetKey(keyBind[8]))// skill bind 3
        {
            SkillSlot.OnUseSkill(2);         
        }
        else if (Input.GetKey(keyBind[9]))// skill bind 4
        {
            SkillSlot.OnUseSkill(3);
        }
        else if (Input.GetKey(keyBind[10]))// skill bind 5
        {
            SkillSlot.OnUseSkill(4);
        }
        else if (Input.GetKey(keyBind[11]))// skill bind 6
        {
            SkillSlot.OnUseSkill(5);
        }
        else if (Input.GetKey(keyBind[12]))// skill bind 7
        {
            SkillSlot.OnUseSkill(6);
        }
        else if (Input.GetKey(keyBind[13]))// skill bind 8
        {
            SkillSlot.OnUseSkill(7);
        }
        else if (Input.GetKey(keyBind[14]))// skill bind 9
        {
            SkillSlot.OnUseSkill(8);
        }
        else if (Input.GetKey(keyBind[15]))// skill bind 0
        {
            SkillSlot.OnUseSkill(9);
        }

        speedAnim = 3;

        if (x == 0 && y == 0)
        {
            //khi mà player k di chuyển và cache cũ của x và y không đổi
            //thì k cần gửi về server
            if (cacheX == 0 && cacheY == 0 && this.GetComponent<Player>().IsPunching == false && this.GetComponent<Player>().IsKick == false) return;

            speedAnim = 0;
            this.GetComponent<Player>().speedAnim = speedAnim;
            this.GetComponent<Player>().spriteStop = spriteStop;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.velocity = Vector2.zero;

            cacheX = x; cacheY = y;
            GameManager.players[Client.instance.myId].gameObject.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
            ClientSend.PlayerMovement(transform.position, speedAnim, spriteStop, this.GetComponent<Player>().IsPunching, this.GetComponent<Player>().IsDead, this.GetComponent<Player>().IsKick);
            return;
        }
        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Player>().speedAnim = speedAnim;
        this.GetComponent<Player>().spriteStop = spriteStop;
        Vector3 moveDir = new Vector3(x, y, 0);
        Vector2 spOld = moveDir.normalized * 10f;
        rb.velocity = spOld;
        cacheX = x; cacheY = y;
        GameManager.players[Client.instance.myId].gameObject.transform.localPosition = new Vector3(transform.position.x, transform.position.y, transform.position.y);
        ClientSend.PlayerMovement(transform.position, speedAnim, spriteStop, this.GetComponent<Player>().IsPunching, this.GetComponent<Player>().IsDead, this.GetComponent<Player>().IsKick);
        
    }
}
