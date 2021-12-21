using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    enum Dir{
        Up = 1,
        Down,
        Left,
        Right
    }

    public float speed;
    public Rigidbody2D rb;



    private Transform target;

    

    #region eyes check
    private CircleCollider2D ccEyes;
    Vector2 def = new Vector2(0, -0.4f);
    Vector2 Eyes_Up = new Vector2(0, 0.2f);
    Vector2 Eyes_Down = new Vector2(0, 1f);
    Vector2 Eyes_Left = new Vector2(-0.6f, -0.4f);
    Vector2 Eyes_Right = new Vector2(0.6f, -0.4f);
    #endregion
    private void Start()
    {
        //rb = this.transform.GetComponent<Rigidbody2D>();
        ccEyes = this.transform.Find("Eyes").GetComponent<CircleCollider2D>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        try
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            if (target == null) return;
            if (Vector2.Distance(transform.position, target.transform.position) > 2 && Vector2.Distance(transform.position, target.transform.position) < 10)
            {
                Vector2 posLocal = this.transform.position;
                Vector2 dirMoving = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                
                Vector2 moveDir = transform.position;
                //Vector2 moveDir = new Vector2();

                if (posLocal.y - dirMoving.y < 0) //up
                {
                    Debug.Log("u "+CanNPCMove(Dir.Up));
                    if (!CanNPCMove(Dir.Up))
                    {
                        moveDir.y += .1f * speed;
                    }
                }
                if (posLocal.y - dirMoving.y > 0) //down
                {
                    Debug.Log("d " + CanNPCMove(Dir.Down));
                    if (!CanNPCMove(Dir.Down))
                    {
                        moveDir.y -= .1f*speed;
                    }
                }
                if (posLocal.x - dirMoving.x > 0) //left
                {
                    Debug.Log("l " + CanNPCMove(Dir.Left));
                    if (!CanNPCMove(Dir.Left))
                    {
                        moveDir.x -= .1f*speed;
                    }
                }
                if(posLocal.x - dirMoving.x < 0) //right
                {
                    Debug.Log("r " + CanNPCMove(Dir.Right));
                    if (!CanNPCMove(Dir.Right))
                    {
                        moveDir.x += .1f * speed;
                    }
                }

                //transform.position = new Vector3(moveDir.normalized.x, moveDir.normalized.y,0);

                transform.position = Vector2.MoveTowards(transform.position, moveDir, speed * .1f);

                //TODO:cehck hướng có thể di chuyển và move 

                //transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

                //Debug.Log(Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime).ToString());
                //Debug.Log(Vector2.Distance(transform.position, target.transform.position));
                //this.GetComponent<Animator>().SetFloat("Dir", spriteStop);
                //this.GetComponent<Animator>().SetFloat("SpeedMove", speedAnim);

            }
            else
            {
                target = null;
            }

        }
        catch (Exception _ex)
        {
            Debug.Log(_ex);    
        }
        
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        try
        {
            if (collision.gameObject.name.IndexOf("Map") >= 0)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                return;
            }else if (collision.gameObject.CompareTag("Player"))
            {
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
               
                return;
            }
            else if (collision.gameObject.name.IndexOf("LocalPlayer") >= 0)
            {
                rb.constraints = RigidbodyConstraints2D.None;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                return;
            }


        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    private bool CanNPCMove(Dir dir)
    {
        if(dir == Dir.Up)
        {
            ccEyes.offset = Eyes_Up;
            return EyesTrigger.IsTrigger();
        }
        if (dir == Dir.Down)
        {
            ccEyes.offset = Eyes_Down;
            return EyesTrigger.IsTrigger();
        }
        if (dir == Dir.Left)
        {
            ccEyes.offset = Eyes_Left;
            return EyesTrigger.IsTrigger();
        }
        if (dir == Dir.Right)
        {
            ccEyes.offset = Eyes_Right;
            return EyesTrigger.IsTrigger();
        }
        return false;
    }
}
