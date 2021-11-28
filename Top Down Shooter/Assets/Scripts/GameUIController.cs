using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : UIController
{
    [SerializeField] Text scoreText;

    public void UpdateScore(int newScore)
    {
        scoreText.text = "SCORE: " + newScore;
    }
}
