using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BubbleChat : MonoBehaviour
{
    private GameObject bubbleChat;
    private GameObject textChat;
    private float time = 0;
    //public string mess = "chào mừng các bạn!\nblabla";

    private float d = 25f;
    private float r = 0.3f;

    private Sprite[] spriteGet;
    private bool isEmoji = false;

    // Start is called before the first frame update
    void Awake()
    {
        bubbleChat = transform.Find("background").gameObject;
        textChat = bubbleChat.gameObject.transform.Find("textChat").gameObject;

        spriteGet = Resources.LoadAll<Sprite>(GAME.EMOJI_PATH);
    }

    //private void Start()
    //{
    //    Setup(mess);
    //}

    public void Setup(string v)
    {
        if(!isEmoji)
            textChat.GetComponent<SpriteRenderer>().enabled = false;

        textChat.GetComponent<Text>().text = v;
        float widthText = textChat.GetComponent<Text>().preferredWidth;
        float heightText = textChat.GetComponent<Text>().preferredHeight;

        textChat.GetComponent<RectTransform>().sizeDelta = new Vector2(widthText, heightText);

        Vector3 positionText = new Vector3();
        positionText.y = (heightText / 10)*(r);
        textChat.GetComponent<RectTransform>().localPosition = positionText;
        float wbubble = (float)widthText / d;
        //float hbubble = (float)heightText / 50;
        bubbleChat.GetComponent<SpriteRenderer>().size = new Vector2(wbubble, ((float)heightText/10));

    }

    // Update is called once per frame
    void Update()
    {
        if (isEmoji)
        {
            this.transform.GetComponent<RectTransform>().localScale = new Vector3(1.3f, 1.3f, 0);
        }
        else
        {
            this.transform.GetComponent<RectTransform>().localScale = new Vector3(1f, 1.3f, 0);
        }
        if (!isEmoji)
            Setup(textChat.GetComponent<Text>().text);

        time += Time.deltaTime;
        if (time >= 10)
            Destroy(this.gameObject);
    }

    public void SetupEmoji(int emoji)
    {
        isEmoji = true;
        textChat.GetComponent<Text>().enabled = false;
        Vector3 positionText = new Vector3();
        positionText.y = 0.72f;
        textChat.GetComponent<RectTransform>().localPosition = positionText;
        //float hbubble = (float)heightText / 50;
        bubbleChat.GetComponent<SpriteRenderer>().size = new Vector2(1, 2.7f);

        textChat.GetComponent<SpriteRenderer>().sprite = spriteGet[emoji];
        textChat.GetComponent<SpriteRenderer>().size = new Vector2(25, 30);
    }
}
