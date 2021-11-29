using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Health : MonoBehaviour
{
    private int numOfHearts = 5;
    private bool immortal = false;

    public Image[] fullHeartImages;
    public Image[] emptyHeartImages;

    // Start method is called on Application start (but if we will have multiple rounds) we need OnEnable
    void OnEnable() => PerformInitSetup();

    public void ReduceLife()
    {
        if (immortal == false)
        {

        numOfHearts--;
        if (numOfHearts <= 0)
        {
            GameOver();
        }

        for (int i = fullHeartImages.Length -1; i >= 0; i--)
        {
            if(fullHeartImages[i].isActiveAndEnabled) // if there are any full lives remaining active
            {
                fullHeartImages[i].gameObject.SetActive(false);
                emptyHeartImages[i].gameObject.SetActive(true);
                return;
            }
        }

        }
    }

    public void RaiseLife()
    {
        if (numOfHearts<5)
        numOfHearts++;

            for (int i = 0; i<= fullHeartImages.Length - 1; i++)
            {
                if (emptyHeartImages[i].isActiveAndEnabled) // if there are any empty lives remaining active
                {
                    fullHeartImages[i].gameObject.SetActive(true);
                    emptyHeartImages[i].gameObject.SetActive(false);
                    return;
                }
            }
        
    }

        public static void GameOver()
    {
        Debug.Log("Game is Over!");
        SceneManager.LoadScene("GameOver");
    }

    private void PerformInitSetup()
    {
        numOfHearts = 5;
        foreach (Image hearthImage in fullHeartImages)
        {
            hearthImage.gameObject.SetActive(true);
        }

        foreach (Image hearthImage in emptyHeartImages)
        {
            hearthImage.gameObject.SetActive(false);
        }
    }
}
