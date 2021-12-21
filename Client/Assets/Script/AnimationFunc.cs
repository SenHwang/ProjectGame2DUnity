using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public enum AnimationType
{
    none,
    byTime,
    byRange
}

/// <summary>
/// Animation skills
/// gọi theo lớp animation đã tách sẵn
/// </summary>
public class AnimationFunc : MonoBehaviour
{
    public int index;
    public bool isDestroy = false;
    public float timeDestroy = -1;
    public float speedMove = -1;
    public float rangeDestroy = -1;

    public AnimationType type;

    private Vector3 oldPoss;

    private int currentIndex;
    private Sprite[] spriteDefault = null;  

    private Dictionary<string, Sprite> spriteSheet = new Dictionary<string, Sprite>();
    private SpriteRenderer animSkillCurrent;
    private Sprite[] LoadAnim(int index)
    {
        if (GAME.animation == null)
            AnimationManager.LoadAnimList();
        if (index-1 < GAME.animation.Length && index >= 0)
        {
            //Path GAME.ANIMATION_PATH;
            //find sprite bằng sprite name trong GAME.animation
            //gán vào spriteDefault rồi return;
            spriteDefault = Resources.LoadAll<Sprite>(GAME.ANIMATION_PATH + index.ToString());
            currentIndex = index;
            return spriteDefault;
        }
        else
        {
            Debug.Log("index anim out of range");
            return spriteDefault;
        }
    }

    private void Start()
    {
        this.animSkillCurrent = this.gameObject.GetComponent<SpriteRenderer>();
        this.spriteDefault = LoadAnim(index);
        oldPoss = this.transform.position;
    }

    private void LateUpdate()
    {
        if(type == AnimationType.byTime)
        {
            if (timeDestroy >= 0)
            {
                timeDestroy -= Time.deltaTime;
            }
            else
                isDestroy = true;
        }

        if(type == AnimationType.byRange)
        {
            if (rangeDestroy <= Vector3.Distance(oldPoss, transform.position))
                isDestroy = true;
        }

        try
        {
            if (index != currentIndex)
            {
                LoadAnim(index);
                LoadSpriteEach();
            }

            if(spriteSheet.Count == 0)
                LoadSpriteEach();

            this.animSkillCurrent.sprite = spriteSheet[this.animSkillCurrent.sprite.name];

            //nếu mà isDestroy chưa true thì không cho destroy animation
            if (!isDestroy) return;

            //khi animation đến cuối cùng là "_29"(0-29) animation có 30 sprite
            //khi đến cuối dùng thì tự động destroy object animation
            if(this.animSkillCurrent.sprite.name.ToString().LastIndexOf("_29") > 0)
            {
                Destroy(this.gameObject);
            }

        }
        catch
        {
            if (!isDestroy)
            {
                this.GetComponent<Animator>().Play("SkillAnim",0,0f);
                return;
            }
            //catch ra đây khi animation list tới cuối sprite hoặc list animation == null thì sẽ hiểu là hết animation và destroy object
            this.animSkillCurrent.sprite = null;
            Destroy(this.gameObject);
            return;
            //Debug.Log("Sprite Not Found: LateUpdate()| Exception:" + _ex);
        }
    }

    private void LoadSpriteEach()
    {
        spriteSheet = spriteDefault.Where(s => s.name.IndexOf("_") >= 0).ToArray()
            .ToDictionary(x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "9_"), x => x);//x => x.name.Replace(x.name.Substring(0, x.name.IndexOf("_") + 1), "1_"), x => x
    }
}
