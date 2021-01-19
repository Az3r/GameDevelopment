using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown graphics;
    public TMP_Dropdown resolutions;

    public List<GameObject> fullScreenToggle;
    public List<GameObject> menus;
    public GameObject globalState;

    public GameObject resumeButton;
    private void Awake()
    {
        // make framerate fixed
        Application.targetFrameRate = 60;

        // check if there is already a GlobalState object (when user return from ShopScene)
        if (FindObjectOfType<GlobalState>() == null)
        {
            Instantiate(globalState, Vector3.zero, Quaternion.identity);
        }
    }
    private void Start()
    {
        resumeButton.SetActive(GlobalState.Instance.CurrentProgress.modelIndex >= 0);

        graphics.value = QualitySettings.GetQualityLevel();

        // fullscreen
        bool fullscreen = Screen.fullScreen;
        fullScreenToggle[0].SetActive(fullscreen);
        fullScreenToggle[1].SetActive(!fullscreen);

        // resolutions
        List<string> options = new List<string>();
        Resolution current = Screen.currentResolution;
        int currentIndex = 0;
        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            var res = Screen.resolutions[i];
            options.Add($"{res.width}x{res.height}");

            if (res.width == current.width && res.height == current.height)
            {
                currentIndex = i;
            }
        }
        resolutions.AddOptions(new List<string>(options));
        resolutions.value = currentIndex;
    }
    public void StartGame()
    {
        GlobalState.Instance.CurrentProgress = SavedData.Default();
        SceneManager.LoadScene("ShopScene");
    }
    public void ResumeGame()
    {
        SceneManager.LoadScene("ShopScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenMenu(int index)
    {
        for (int i = 0; i < menus.Count; i++)
        {
            menus[i].SetActive(false);
        }
        menus[index].SetActive(true);
    }

    public void SetVolumn(float value)
    {
        audioMixer.SetFloat("VolumnValue", value);
    }
    public void SetGraphic(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
    public void SetFullScreen(bool value)
    {
        Screen.fullScreen = value;
        fullScreenToggle[0].SetActive(value);
        fullScreenToggle[1].SetActive(!value);
    }

    public void SetResolution(int index)
    {
        Resolution selected = Screen.resolutions[index];
        Screen.SetResolution(selected.width, selected.height, Screen.fullScreen);
    }
}
