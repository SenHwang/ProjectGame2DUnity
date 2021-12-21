using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapUpdateUI : MonoBehaviour
{
    public static GameObject blood;
    public static GameObject textScreen;

    public static GameObject RootUI;
    // Start is called before the first frame update
    void Start()
    {
        //UIGamePanel = this.gameObject.transform.Find("Canvas").gameObject;
        //EditMapPanel = this.gameObject.transform.Find("EditMap").gameObject;

        RootUI = this.gameObject;

        blood = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/blood");
        textScreen = Resources.Load<GameObject>("UI/MISC/PrefabsMISC/text");

        //for (int i = 0; i < 10; i++)
        //{
        //    Vector3 vector3 = new Vector3(i, 0, 0);
        //    CreateBlood(vector3);
        //}
    }

    void OnMouseOver()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            textScreen.GetComponent<TextDisplay>().map = GAME.MAP_START;
            textScreen.GetComponent<Text>().text = $"[{this.transform.position.ToString()}]";
            GameObject textScreen1 = Instantiate(textScreen, this.transform.position, new Quaternion());
        }
    }

    public static void CreateBlood(Vector3 vector3)
    {
        blood.GetComponent<BloodDisplay>().map = GAME.MAP_START;
        GameObject blood1 = Instantiate(blood, vector3, new Quaternion());
        
    }
    public static void CreateText(Vector3 vector3, string text)
    {
        textScreen.GetComponent<TextDisplay>().map = GAME.MAP_START;
        textScreen.GetComponent<Text>().text = $"<color=#ff0000ff>{text}</color>";
        GameObject textScreen1 = Instantiate(textScreen, vector3, new Quaternion());
    }

}
