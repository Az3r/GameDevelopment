using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
  public GameObject StartMenu;
  public GameObject SettingMenu;
  public void StartGame()
  {
    SceneManager.LoadScene("MainScene");
  }

  public void ExitGame()
  {
    Application.Quit();
  }

  public void OpenSettingMenu()
  {
    StartMenu.SetActive(false);
    SettingMenu.SetActive(true);
  }
  public void OpenStartMenu()
  {
    StartMenu.SetActive(true);
    SettingMenu.SetActive(false);
  }
}
