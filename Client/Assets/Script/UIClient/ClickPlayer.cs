using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickPlayer : MonoBehaviour
{
    bool isHit = false;
    float refeshClick = 3f;
    //sprite khi di chuột vào
    public Sprite spriteH;

    //sprite khi click vào player
    public Sprite spriteC;

    private void Update()
    {
        if (isHit == false) return;

        refeshClick -= Time.deltaTime;
        if (refeshClick <= 0)
        {
            isHit = false;
            refeshClick = 3f;
        }
    }
    private void OnMouseDown()
    {
        //khi click vào
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isHit = true;
            if (TargetBar.idTarget == this.transform.GetComponent<Player>().id)
            {
                TargetBar.SetTarget(-1);
                return;
            }

            TargetBar.SetTarget(this.transform.GetComponent<Player>().id);
        }
    }

    void OnMouseOver()
    {
        //nếu mà chỉ di vào người không phải đã target sẵn
        if(TargetBar.idTarget != this.transform.GetComponent<Player>().id && this.transform.GetComponent<Player>().id != Client.instance.myId)
            this.transform.GetComponent<SpriteRenderer>().sprite = spriteH; 
    }
    private void OnMouseExit()
    {
        // khi di chuột ra khỏi player mà target = off thì tắt target over
        if (TargetBar.idTarget != this.transform.GetComponent<Player>().id && this.transform.GetComponent<Player>().id != Client.instance.myId)        
            this.transform.GetComponent<SpriteRenderer>().sprite = null;
    }

}
