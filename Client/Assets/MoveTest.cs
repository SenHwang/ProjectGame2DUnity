using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTest : MonoBehaviour {
    // Update is called once per frame
    public Rigidbody2D rb;
	void FixedUpdate () {
        float x = 0;
        float y = 0;
        if (Input.GetKey(KeyCode.D))
        {
            x += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            x -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            y += 1f;
        }
        if (Input.GetKey(KeyCode.S))
        {
            y -= 1f;
        }

        
        if (x == 0 && y == 0)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        Vector3 moveDir = new Vector3(x, y, 0);
        Vector2 sd = moveDir.normalized * 10f;
        rb.velocity = sd;
    }
}
