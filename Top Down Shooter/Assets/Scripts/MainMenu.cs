using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MainMenu : MonoBehaviour
{
    [Header("UI Screen Panels")]
    [SerializeField] GameObject defaultPanel;
    [SerializeField] GameObject settingsPanel;
    [SerializeField] GameObject scoresPanel;
    [SerializeField] GameObject aboutPanel;

    [Header("Volume Controll")]
    [SerializeField] Slider volumeSlider;
    [SerializeField] Sprite muteSprite;
    [SerializeField] Sprite unmuteSprite;

    bool isMute;

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

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
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
    }

    public void OpenAboutScreen()
    {
        var success = CloseAllPanels();
        if (success)
            aboutPanel.SetActive(true);
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

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        PlayerPrefs.SetFloat("volume", volume);
    }

    public void Mute(GameObject caller)
    {
        isMute = !isMute;
        if (PlayerPrefs.GetInt("isMuted") == 0)
        {
            PlayerPrefs.SetInt("isMuted", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isMuted", 0);
        }
        if (isMute)
        {
            PlayerPrefs.SetFloat("volumeBeforeMute", AudioListener.volume);
            AudioListener.volume = 0.0f;
            PlayerPrefs.SetFloat("volume", 0.0f);
            caller.GetComponent<Image>().sprite = unmuteSprite;
        }
        else
        {
            if (PlayerPrefs.GetFloat("volumeBeforeMute") != 0.0f)
            {
                AudioListener.volume = PlayerPrefs.GetFloat("volumeBeforeMute");
                PlayerPrefs.SetFloat("volume", AudioListener.volume);
            }
            else
            {
                AudioListener.volume = 1.0f;
                PlayerPrefs.SetFloat("volume", 1.0f);
            }
            caller.GetComponent<Image>().sprite = muteSprite;
        }
    }
}
