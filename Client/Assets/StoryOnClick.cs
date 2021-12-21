using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static NPC;

public class StoryOnClick : MonoBehaviour
{
    public int idStory;
    public StoryType type;

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StoryGameFunc.instance.indexStory = idStory;
            StoryGameFunc.NextPageStory();
        }
    }

    private void Start()
    {
        this.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);
        this.GetComponent<RectTransform>().localPosition = new Vector3(270, 80 + (idStory)*30 , 0);
    }
}
