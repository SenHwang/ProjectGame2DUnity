using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerFunc : MonoBehaviour
{
    public static UIManagerFunc instance;
    public GameObject canvasObject;
    public GameObject editMapObject;
    public GameObject adminObject;
    public GameObject storyObject;

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

    public void EnableObject(GameObject gameObject)
    {
        if (canvasObject == gameObject)
            this.canvasObject.SetActive(true);
        else
            this.canvasObject.SetActive(false);

        if (editMapObject == gameObject)
            this.editMapObject.SetActive(true);
        else
            this.editMapObject.SetActive(false);

        if (adminObject == gameObject)
            this.adminObject.SetActive(true);
        else
            this.adminObject.SetActive(false);

        if (storyObject == gameObject)
            this.storyObject.SetActive(true);
        else
            this.storyObject.SetActive(false);
    }

}
