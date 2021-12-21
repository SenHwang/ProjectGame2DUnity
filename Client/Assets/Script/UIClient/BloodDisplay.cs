using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodDisplay : MonoBehaviour
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
        if (map != GAME.MAP_START)
            Destroy(this.gameObject);
        if (time >= 5)
            Destroy(this.gameObject);
    }
}
