using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisplay : MonoBehaviour
{
    private float time = 0;
    public int map;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;        
        if(map != GAME.MAP_START)
            Destroy(this.gameObject);
        if (time>=1)
            Destroy(this.gameObject);

        Vector3 vector3 = this.transform.position;
        this.transform.position = new Vector3(vector3.x, vector3.y += time/500, vector3.z);
    }
}
