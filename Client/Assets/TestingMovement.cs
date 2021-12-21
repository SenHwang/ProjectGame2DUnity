using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    private int spriteStop = 0;
    public int speedAnim = 3;
    float counterHit = 0;
    bool isHit = false;



    // Update is called once per frame
    void FixedUpdate()
    {
        
        speedAnim = this.GetComponent<Player>().speedAnim;
        spriteStop = this.GetComponent<Player>().spriteStop;
        SendInputToServer();

        if (isHit)
        {
            
            if (counterHit <= 0.8f)
            {
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
        float x = 0;
        float y = 0;

        if (Input.GetKey(KeyCode.W))
        {
            y += 1f;
            spriteStop = 12;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            y -= 1f;
            spriteStop = 0;
        }

        if (Input.GetKey(KeyCode.A))
        {
            x -= 1f;
            spriteStop = 4;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            x += 1f;
            spriteStop = 8;
        }

        if (Input.GetKey(KeyCode.Return))
        {
            if (!isHit)
            {
                isHit = true;
                this.GetComponent<Player>().IsDead = !this.GetComponent<Player>().IsDead;

            }

        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            if (!isHit)
            {
                isHit = true;
                this.GetComponent<Player>().IsPunching = true;
            }
            
        }

        speedAnim = 3;

        if (x == 0 && y == 0)
        {
            speedAnim = 0;
            this.GetComponent<Player>().speedAnim = speedAnim;
            this.GetComponent<Player>().spriteStop = spriteStop;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.velocity = Vector2.zero;
            //ClientSend.PlayerMovement(transform.position, speedAnim, spriteStop);
            return;
        }

        rb.constraints = RigidbodyConstraints2D.None;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        this.GetComponent<Player>().speedAnim = speedAnim;
        this.GetComponent<Player>().spriteStop = spriteStop;
        Vector3 moveDir = new Vector3(x, y, 0);
        Vector2 spOld = moveDir.normalized * 10f;
        rb.velocity = spOld;
        //ClientSend.PlayerMovement(transform.position, speedAnim, spriteStop);
    }
}
