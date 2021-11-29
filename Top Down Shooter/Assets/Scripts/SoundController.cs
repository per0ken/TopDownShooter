using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoundController : MonoBehaviour
{
    public AudioSource[] soundFX;
    public float timer;

    public static UnityEvent enemyDied = new UnityEvent();
    public static UnityEvent playerShot = new UnityEvent();
    public static UnityEvent enemyShot = new UnityEvent();

    private void Start()
    {
        enemyDied.AddListener(PlayEnemyDieSound);
        playerShot.AddListener(PlayerShoot);
        enemyShot.AddListener(enemyShoot);
    }

    private void OnEnable()
    {
        soundFX[2].Play();
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > 185)
        {
            soundFX[2].Play();
        }
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
}

