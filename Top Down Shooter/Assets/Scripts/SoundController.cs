using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    public AudioSource[] soundFX;

    public static UnityEvent enemyDied = new UnityEvent();
    public static UnityEvent playerShot = new UnityEvent();

    private void Start()
    {
        enemyDied.AddListener(PlayEnemyDieSound);
        playerShot.AddListener(PlayerShoot);
    }

    private void PlayEnemyDieSound()
    {
        soundFX[1].Play();
    }

    void PlayerShoot()
    {
        soundFX[0].Play();
    }
}
