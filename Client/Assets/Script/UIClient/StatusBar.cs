using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusBar : MonoBehaviour
{
    private Text playerName;

    private Text srtengthText;
    private Text agilityText;
    private Text intellectText;
    private Text mpText;
    private Text hpText;
    private Text pointFreeText;

    private Button up1;
    private Button up2;
    private Button up3;
    private Button up4;
    private Button up5;

    Stats stats;


    // Start is called before the first frame update
    void Start()
    {
        stats = GameManager.players[Client.instance.myId].stat;
        playerName = transform.Find("PlayerName").GetComponent<Text>();
        srtengthText = transform.Find("Strength").GetComponent<Text>();
        agilityText = transform.Find("Agility").GetComponent<Text>();
        intellectText = transform.Find("Intellect").GetComponent<Text>();
        mpText = transform.Find("Mana").GetComponent<Text>();
        hpText = transform.Find("Health").GetComponent<Text>();
        pointFreeText = transform.Find("Point").GetComponent<Text>();

        up1 = transform.Find("Up1").GetComponent<Button>();
        up2 = transform.Find("Up2").GetComponent<Button>();
        up3 = transform.Find("Up3").GetComponent<Button>();
        up4 = transform.Find("Up4").GetComponent<Button>();
        up5 = transform.Find("Up5").GetComponent<Button>();

        playerName.text = GameManager.players[Client.instance.myId].username;

        srtengthText.text = "Strength: " + stats.strength;
        agilityText.text = "Agility: " + stats.agility;
        intellectText.text = "Intellect: "+ stats.intellect;
        mpText.text = "Mana: "+ stats.mp;
        hpText.text = "Health: "+ stats.hp;
        pointFreeText.text = "Point: "+ stats.pointFree;
    }

    private void Update()
    {
        stats = GameManager.players[Client.instance.myId].stat;
        srtengthText.text = "Strength: " + stats.strength;
        agilityText.text = "Agility: " + stats.agility;
        intellectText.text = "Intellect: " + stats.intellect;
        mpText.text = "Mana: " + stats.mp;
        hpText.text = "Health: " + stats.hp;
        pointFreeText.text = "Point: " + stats.pointFree;
        if (stats.pointFree == 0)
        {
            up1.gameObject.SetActive(false);
            up2.gameObject.SetActive(false);
            up3.gameObject.SetActive(false);
            up4.gameObject.SetActive(false);
            up5.gameObject.SetActive(false);
            return;
        }
        else
        {
            up1.gameObject.SetActive(true);
            up2.gameObject.SetActive(true);
            up3.gameObject.SetActive(true);
            up4.gameObject.SetActive(true);
            up5.gameObject.SetActive(true);
            return;
        }
    }

    public void UpClick(int value)
    {
        if (value < 1 || value > 5) return;
        if (stats.pointFree == 0) return;

        //if (value == 1)
        //{
        //    stats.strength++; stats.pointFree--;
        //}
        //if (value == 2)
        //{
        //    stats.agility++; stats.pointFree--;
        //}
        //if (value == 3){
        //    stats.intellect++; stats.pointFree--; 
        //}
        //if (value == 4) {
        //    stats.mp++; stats.pointFree--; 
        //}
        //if (value == 5)
        //{
        //    stats.hp++; stats.pointFree--;
        //}

        ClientSend.SendStatusUpdate(value);
    }
}
