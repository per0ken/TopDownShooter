using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class UIController : MonoBehaviour
{
    [Header("UI Controller Volume Controll")]
    public Sprite muteSprite;
    public Sprite unmuteSprite;
    [HideInInspector] public bool isMute;

    public void PlayGame()
    {
        SceneManager.LoadScene("Game");
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
