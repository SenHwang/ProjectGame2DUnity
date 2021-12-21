using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// object nào set cái script này thì phải enable active object lên
/// </summary>

public class TargetBar : MonoBehaviour
{
    public static int idTarget = -1;
    
    //this object 
    public static GameObject Target;

    public GameObject nameBar;
    public GameObject HP;

    float bHPCache;
    private void Awake()
    {
        TargetBar.Target = this.gameObject;

        this.gameObject.SetActive(false);        
    }


    // Update is called once per frame
    void LateUpdate()
    {
        try
        {
            if (idTarget == -1 || GameManager.players[idTarget].mapID != GameManager.players[Client.instance.myId].mapID)
            {
                this.gameObject.SetActive(false);
                bHPCache = -1;
                return;
            }
            Player player = GameManager.players.SingleOrDefault(x => x.Value.id == idTarget).Value;

            if (!player)
            {
                this.gameObject.SetActive(false);
                return;
            }

            Stats stat = player.stat;
            float bHP = (float)stat.healthLeft / stat.health;
            
            //nếu cache cũ khác lần change mới thì return
            if (bHPCache == bHP) return;

            bHPCache = bHP;
            /*if (bHP <= 0)
            {
                bHP = 1;
            }*/

            SetHP(bHP, player.username);
        }
        catch
        {
            this.gameObject.SetActive(false);   
        }
       
    }

    void SetHP(float sizeHP, string name)
    {
        nameBar.GetComponent<Text>().text = name;

        HP.transform.localScale = new Vector3(sizeHP, 1f,0);
    }

    public static void SetTarget(int id)
    {
        //tắt targetbar khi id truyền vào == -1
        if (idTarget != -1 && id == -1)
        {
            GameManager.players[TargetBar.idTarget].GetComponent<SpriteRenderer>().sprite = null;
            TargetBar.Target.gameObject.SetActive(false);
            idTarget = -1;
            return;
        }

        if (id != -1)
        {
            if(idTarget != -1)
                GameManager.players[TargetBar.idTarget].GetComponent<SpriteRenderer>().sprite = null;

            idTarget = id;
            TargetBar.Target.gameObject.SetActive(true);  
        }
    }

}
