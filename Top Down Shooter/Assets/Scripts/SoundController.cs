using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : Lives
{
    public AudioSource[] soundFX;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            soundFX[0].Play();
        }
        if (enemyLives == 0)
        {
            soundFX[1].Play();
        }
    }
}
