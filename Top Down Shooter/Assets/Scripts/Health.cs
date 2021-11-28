using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class Health : MonoBehaviour
{
    public int numOfHearts = 3;

    public Image[] fullHeartImages;
    public Image[] empytHeartImages;

    // Start method is called on Application start (but if we will have multiple rounds) we need OnEnable
    void OnEnable() => PerformInitSetup();

    public void ReduceLife()
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
                empytHeartImages[i].gameObject.SetActive(true);
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
        numOfHearts = 3;
        foreach (Image hearthImage in fullHeartImages)
        {
            hearthImage.gameObject.SetActive(true);
        }

        foreach (Image hearthImage in empytHeartImages)
        {
            hearthImage.gameObject.SetActive(false);
        }
    }
}
