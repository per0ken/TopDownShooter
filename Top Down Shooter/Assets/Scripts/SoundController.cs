using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource[] soundFX;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            soundFX[0].Play();
        }
        if (Enemies.lives == 0)
        {
            soundFX[1].Play();
        }
    }
}
