using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportGate : MonoBehaviour
{
    public Vector2 telePoint;
    public int mapID = -1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (mapID == -1) return;
        if (collision.gameObject.name.IndexOf("Player") >= 0)
        {
            //TODO: send packet rq tele map to server.
            ClientSend.ClientRQTeleMap(mapID, telePoint);
            Debug.Log(telePoint);
        }
    }
}
