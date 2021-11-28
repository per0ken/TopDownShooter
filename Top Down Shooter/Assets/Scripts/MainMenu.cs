using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : UIController
{
    [Header("UI Screen Panels")] // Ezek csak ilyen szép header-ök az Inspectorban :;
    [SerializeField] GameObject defaultPanel; // SerializeField azt jelenti, hogy megjelenik és assignolható az Inspectorban, viszont nem public más osztályok felé (vagyis másik kódból nem módosítható)
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject scoresPanel;
    [SerializeField] GameObject aboutPanel;

    [Header("Volume Controll")]
    [SerializeField] Slider volumeSlider;

    [Header("Score Board UI")]
    [SerializeField] Text scoreKillText;
    [SerializeField] Text noScoreKillText;
    [SerializeField] Button deleteHistoryButton;

    void OnEnable()
    {
        OpenMainMenuDefaultScreen();
    }

    public void Start()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        if (PlayerPrefs.GetInt("isMuted") != 1 && PlayerPrefs.GetFloat("volume") != 0)
        {
            float volumeLevel = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = volumeLevel;
        }
        else
        {
            volumeSlider.value = 0.0f;
        }
    }

    void Update()
    {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        if (PlayerPrefs.GetInt("isMuted") != 1 && PlayerPrefs.GetFloat("volume") != 0)
        {
            float volumeLevel = PlayerPrefs.GetFloat("volume");
            volumeSlider.value = volumeLevel;
        }
        else
        {
            volumeSlider.value = 0.0f;
        }
    }

    public void OpenMainMenuDefaultScreen()
    {
        var success = CloseAllPanels();
        if (success)
            defaultPanel.SetActive(true);
    }

    public void OpenSettingsScreen()
    {
        var success = CloseAllPanels();
        if (success)
            settingsPanel.SetActive(true);
    }

    public void OpenScoresScreen()
    {
        var success = CloseAllPanels();
        if (success)
            scoresPanel.SetActive(true);
        SetupScoreBoard();
    }

    public void OpenAboutScreen()
    {
        var success = CloseAllPanels();
        if (success)
            aboutPanel.SetActive(true);
    }

    public void DeleteHistory()
    {
        PlayerPrefs.DeleteAll();
        SetupScoreBoard();
    }

    internal bool CloseAllPanels()
    {
        if (defaultPanel == null || aboutPanel == null || scoresPanel == null || settingsPanel == null)
            return false;
        defaultPanel.SetActive(false);
        aboutPanel.SetActive(false);
        settingsPanel.SetActive(false);
        scoresPanel.SetActive(false);
        return true;
    }

    internal void SetupScoreBoard()
    {
        if (scoreKillText == null || noScoreKillText == null)
            return;
        
        int latestScore = PlayerPrefs.GetInt("latestScore", 0);
        int latestKill = latestScore / 100;

        int hiScore = PlayerPrefs.GetInt("hiScore", 0);
        int hiKill = latestScore / 100;

        if(latestScore == 0 || hiScore == 0)
        {
            noScoreKillText.gameObject.SetActive(true);
            scoreKillText.gameObject.SetActive(false);
            deleteHistoryButton.gameObject.SetActive(false);
        }
        else
        {
            noScoreKillText.gameObject.SetActive(false);
            scoreKillText.gameObject.SetActive(true);
            scoreKillText.text = "YOUR RECORD:\n" + hiKill + " KILLS / " + hiScore + " POINTS\n\nYOU LAST KILLED: " + latestKill + " ENEMIES\nGOT " + latestScore + " POINTS";
            deleteHistoryButton.gameObject.SetActive(true);
        }
    }
}
