using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeScript : MonoBehaviour
{
    public float timer;
    public int spawnTimeInternal = 10;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > spawnTimeInternal)
        {
            Destroy(this.gameObject);
        }
    }
}
