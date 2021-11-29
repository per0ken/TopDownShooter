using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class skullScript : MonoBehaviour
{
    public float timer;
    public int spawnTimeInternal = 7;

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
