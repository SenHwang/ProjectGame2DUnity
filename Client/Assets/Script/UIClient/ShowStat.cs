using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStat : MonoBehaviour
{
    public Text fpsText;
    public Text pingText;
    public float deltaTime;
    private float timeCount = 0;
    bool isGet = false;
    // Update is called once per frame
    private void FixedUpdate()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
        GetPing();
    }

    void GetPing()
    {
        if (isGet && timeCount <= 5f)
        {
            timeCount += Time.deltaTime;
            if (timeCount > 5f)
                isGet = false;
            return;
        }
         var PingTest = new Ping(Client.instance.ip);

        while (!PingTest.isDone)
            new WaitForSeconds(.1f);

        if (PingTest.isDone)
        {
            int pinging = PingTest.time;
            pingText.text = "Ping: " + pinging + " ms";

            isGet = true;
            timeCount = 0;
        }

    }
}
