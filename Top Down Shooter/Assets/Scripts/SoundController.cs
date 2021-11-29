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
    public static UnityEvent enemyShot = new UnityEvent();
    public static UnityEvent healthAdd = new UnityEvent();
    public static UnityEvent immortalOn = new UnityEvent();

    private void Start()
    {
        enemyDied.AddListener(PlayEnemyDieSound);
        playerShot.AddListener(PlayerShoot);
        enemyShot.AddListener(enemyShoot);
        healthAdd.AddListener(healthPickUp);
        immortalOn.AddListener(immortalPickUp);
    }

    private void PlayEnemyDieSound()
    {
        soundFX[1].Play();
    }

    void PlayerShoot()
    {
        soundFX[0].Play();
    }

    void enemyShoot()
    {
        soundFX[0].Play();
    }

    void healthPickUp()
    {
        soundFX[2].Play();
    }
    void immortalPickUp()
    {
        soundFX[3].Play();
    }


}

