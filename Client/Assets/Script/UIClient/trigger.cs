using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger : MonoBehaviour {
    public static bool isBlock = false;
    private void Update()
    {        
        CheckMovement();
    }
    void CheckMovement()
    {
        bool[] input = new bool[4];
        if (Input.GetKey(KeyCode.W)) //up
        {
            input[0] = true;
        }
        if (Input.GetKey(KeyCode.S))//down
        {
            input[1] = true;
        }
        if (Input.GetKey(KeyCode.A))//left
        {
            input[2] = true;
        }
        if (Input.GetKey(KeyCode.D))//right
        {
            input[3] = true;
        }

        if (input[0] == false && input[1] == false&& input[2] == false&& input[3] == false)
        {
            return; //idle character 
        }
        if (input[0] == true)//trên
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(0, 0.55f);
        }
        if (input[1] == true)//dưới
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(0, -1.25f);
        }
        if (input[2] == true)//trái
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(-.85f, -0.38f);
        }
        if (input[3] == true)//phải
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(.85f, -0.38f);
        }

        if (input[0] == true && input[2] == true)//trên trái
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(-.85f, 0.55f);
        }
        if (input[0] == true && input[3] == true)//trên phải
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(.85f, 0.55f);
        }
        if (input[1] == true && input[2] == true)//dưới trái
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(-.85f, -1.25f);
        }
        if (input[1] == true && input[3] == true)//dưới phải
        {
            this.GetComponent<CircleCollider2D>().offset = new Vector2(.85f, -1.25f);
        }        
        
        isBlock = this.GetComponent<CircleCollider2D>().IsTouchingLayers(1);
    }
}
