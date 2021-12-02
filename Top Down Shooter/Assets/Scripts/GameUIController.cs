using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class GameUIController : UIController
{
    [Header("Game UI")]
    [SerializeField] Text scoreText;
    [SerializeField] Text immortalText;
    float timing = 24f;
    public bool startCount = false;

    public static UnityEvent startCounting = new UnityEvent();

    private void OnEnable()
    {
        startCounting.AddListener(OnImmortality);
    }
    public void UpdateScore(int newScore)
    {
        scoreText.text = "SCORE: " + newScore;
    }

    public void UpdateImmortal(float newTime)
    {
        newTime = (float)Math.Round(newTime, 1);
        immortalText.text = "IMMORTAL: " + (newTime).ToString();
        Debug.Log(newTime);
        if (timing < 0)
        {
            startCount = false;
            timing = 20;
            immortalText.text = "IMMORTAL: 0.0";
        }
    }

    void Update()
    {
        if (startCount == true)
        {
            timing -= Time.fixedDeltaTime;
            if((int)(Math.Round(timing, 1) % 0.1) ==0) UpdateImmortal(timing % 60);
            if (timing < 0)
            {
                startCount = false;
                timing = 20;
                immortalText.text = "IMMORTAL: 0.0";
            }
        }
        
    }

    public void OnImmortality()
    {
        startCount = true;
    }
}
