using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverUIController : UIController
{
    // SerializeField azt jelenti, hogy megjelenik �s assignolhat� az Inspectorban, viszont nem public m�s oszt�lyok fel� (vagyis m�sik k�db�l nem m�dos�that�)
    [SerializeField] Text scoreText;

    private void OnEnable()
    {
        SetupScoreKillText();
    }

    public void SetupScoreKillText()
    {
        if (scoreText == null)
            return;

        int latestScore = PlayerPrefs.GetInt("latestScore", 0);
        int latestKill = latestScore / 100;
        scoreText.text = "YOU KILLED: " + latestKill + " ENEMIES\nGOT " + latestScore + " POINTS";
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
