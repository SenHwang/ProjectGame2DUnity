using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MonsterEquipAnim : MonoBehaviour
{
    #region NPC setting
    public int NPCID;
    public int spriteStop = 0;
    public int speedAnim = 3;
    public int sprite = 1;
    private int spriteOld;
    #endregion

    private Sprite[] spriteGet;
    private Dictionary<string, Sprite> spriteSheet;
    private SpriteRenderer spriteRenderer;
    private Vector2 possOld;

    // Start is called before the first frame update
    void Start()
    {        
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        LoadSprite();
    }
    private void LateUpdate()
    {
        try
        {
            if (sprite == spriteOld) return;
            LoadSprite();
            this.spriteRenderer.sprite = spriteSheet[this.spriteRenderer.sprite.name];            
        }
        catch { }
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite == spriteOld) return;
        AnimatorObject();
        UpdateMovement();
    }

    private void UpdateMovement()
    {
        // so sánh vị trí hiện tại với vị trí cũ đã lưu
        // nếu vị trí x hay y cũ khác với vị trí x,y mới thì update animation rồi thay pos cũ thành pos hiện tại
        if(possOld.x < this.transform.position.x)
        {
            spriteStop = 4;
            speedAnim = 3;
            possOld.x = this.transform.position.x;
        }
        else if(possOld.x > this.transform.position.x)
        {
            spriteStop = 8;
            speedAnim = 3;
            possOld.x = this.transform.position.x;
        }

        if(possOld.y < this.transform.position.y)
        {
            spriteStop = 0;
            speedAnim = 3;
            possOld.y = this.transform.position.y;
        }
        else if(possOld.y > this.transform.position.y)
        {
            spriteStop = 12;
            speedAnim = 3;
            possOld.y = this.transform.position.y;
        }

        if(possOld.y == this.transform.position.y && possOld.x == this.transform.position.x)
        {
            speedAnim = 0;
        }
    }

    private void LoadSprite()
    {
        if (sprite == 0)
        {
            sprite = -1;
            return;
        }
        //load new sprite
        spriteGet = Resources.LoadAll<Sprite>("NPC/Sprites/" + sprite.ToString());

        spriteSheet = spriteGet.Where(s => s.name.IndexOf(sprite + "_") >= 0).ToArray()
        .ToDictionary(x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "1_"), x => x);//x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "1_"), x => x
        spriteOld = sprite;

        AnimatorObject();
    }

    void AnimatorObject()
    {        
        this.GetComponent<Animator>().enabled = spriteRenderer.enabled = (sprite != -1) ? true : false;
        EquipAnimAlot();
    }

    void EquipAnimAlot()
    {
        Debug.Log($"sprite stop {spriteStop * 4}");
        this.GetComponent<Animator>().SetFloat("Dir", spriteStop*4);
        this.GetComponent<Animator>().SetFloat("SpeedMove", speedAnim);       
    }
}
