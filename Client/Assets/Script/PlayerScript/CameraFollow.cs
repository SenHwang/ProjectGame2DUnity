using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraFollow : MonoBehaviour {

    public static GameObject playerPrepab;

    //private Func<Vector3> GetCameraFollowPostionFunc;

    //public void Setup(Func<Vector3> GetCameraFollowPostionFunc)
    //{
    //    this.GetCameraFollowPostionFunc = GetCameraFollowPostionFunc;
    //}

    //private void FixedUpdate()
    //{
    //    try
    //    {
    //        if(playerPrepab != null) 
    //            Setup(() => playerPrepab.transform.position);
    //    }
    //    catch (Exception _ex)
    //    {
    //        Debug.Log(_ex.Message);
    //    }

    //}
    // Update is called once per frame
    void Update () {        
        if (playerPrepab != null)
        {           
            Vector3 playerPos = playerPrepab.transform.position;
            playerPos.z = this.transform.position.z;
            this.transform.position = playerPos;
        }
      
                  
    }
}
