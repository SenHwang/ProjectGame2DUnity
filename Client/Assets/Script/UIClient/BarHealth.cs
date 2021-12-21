using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarHealth : MonoBehaviour
{
    public static BarHealth instance;
    public Transform hp;
    public Transform mp;
    public GameObject infoPlayer;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SetPlayer(string name, int lv)
    {
        infoPlayer.GetComponent<Text>().text = $"{name} - Lv: {lv}";
    }

    public void SetHP(float sizeHP)
    {
        hp.localScale = new Vector3(sizeHP, 1f);
    }
    public void SetMP(float sizeMP)
    {
        mp.localScale = new Vector3(sizeMP, 1f);
    }
}
