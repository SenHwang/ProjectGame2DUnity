using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyesTrigger : MonoBehaviour
{
    public static bool trigger = false;

    private void OnTriggerExit(Collider other)
    {
        trigger = false;
    }

    private void FixedUpdate()
    {
        trigger = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // eyes check hướng nếu trigger với onject map hoặc player thì return false(can't move);
        try
        {
            if (collision.gameObject.name.IndexOf("Map") >= 0)
            {
                Debug.Log(this.transform.GetComponent<CircleCollider2D>().offset);
                trigger = true;
                return;
            }
            else if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log(this.transform.GetComponent<CircleCollider2D>().offset);
                trigger = true;
                return;
            }

            Debug.Log(this.transform.GetComponent<CircleCollider2D>().offset);
        }
        catch (Exception ex)
        {
            Debug.Log(ex);
        }

    }

    public static bool IsTrigger()
    {
        return trigger;
    }

}
