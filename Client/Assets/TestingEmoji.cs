using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingEmoji : MonoBehaviour
{
    
    public GameObject emoji2;

    public string emoji;


    private Sprite[] spriteGet;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        spriteGet = Resources.LoadAll<Sprite>(GAME.EMOJI_PATH);
        emoji2 = GameManager.players[Client.instance.myId].gameObject.GetComponent<TestingEmoji>().emoji2;

        emoji2.GetComponent<SpriteRenderer>().sprite = null;
    }

    // Update is called once per frame
    void Update()
    {
        if(time >=5)
        {
            time = 0;
            emoji = "";
            emoji2.GetComponent<SpriteRenderer>().sprite = null;
            return;
        }
        if(emoji != "")
        {
            time+= Time.deltaTime;
            emoji2.GetComponent<SpriteRenderer>().sprite = spriteGet[int.Parse(emoji)];
        }
    }
}
