using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSRCFunc : MonoBehaviour
{
    // Update is called once per frame
    void LateUpdate()
    {
        if(gameObject.GetComponent<AudioSource>().isPlaying == false)
        {
            Destroy(this.gameObject);
        }
    }
}
