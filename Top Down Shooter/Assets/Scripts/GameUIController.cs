using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameUIController : UIController
{
    [SerializeField] Text scoreText;
    [SerializeField] Text immortalText;
    float timing = 15;
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
        immortalText.text = "IMMORTAL: " + (newTime).ToString();
        Debug.Log(newTime);
        if (timing < 0)
        {
            startCount = false;
            timing = 0;
            immortalText.text = "IMMORTAL: 0.0";
        }
    }

    void Update()
    {
        if (startCount == true)
        {
            timing -= Time.fixedDeltaTime;
            if(timing % 0.1 ==0) UpdateImmortal(timing);
            if (timing < 0)
            {
                startCount = false;
                timing = 0;
                immortalText.text = "IMMORTAL: 0.0";
            }
        }
        
    }

    public void OnImmortality()
    {
        startCount = true;
    }
}
