using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
  public List<GameObject> menus;
  public void StartGame()
  {
    SceneManager.LoadScene("MainScene");
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
}
