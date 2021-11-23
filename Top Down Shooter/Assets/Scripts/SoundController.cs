using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    public AudioSource[] soundFX;

    public static UnityEvent enemyDied = new UnityEvent();

    private void Start()
    {
        enemyDied.AddListener(PlayEnemyDieSound);
    }

    private void PlayEnemyDieSound()
    {
        soundFX[1].Play();
    }

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            soundFX[0].Play();
        }
    }
}
