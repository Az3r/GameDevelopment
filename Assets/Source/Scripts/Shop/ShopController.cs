using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    public List<GameObject> models;

    [Header("Observed Fields")]
    [SerializeField]
    private int index;
    [SerializeField]
    private GameObject current;


    private void Awake()
    {
        // make framerate fixed
        Application.targetFrameRate = 60;
        Instance = this;
    }
    private void Start()
    {
        // Hide all models but current selected one
        foreach (var model in models)
        {
            model.SetActive(false);
        }
        models[index].SetActive(true);
        current = models[index];
    }

    public void NextModel() => GetModel(index + 1);
    public void PreviousModel() => GetModel(index - 1);

    private void GetModel(int i)
    {
        // display new model and continue to rotate with an angle from old model
        var rotation = current.transform.rotation;
        index = i % models.Count;
        current.SetActive(false);
        current = models[index];
        current.SetActive(true);
        current.transform.rotation = rotation;
    }

    public void StartGame(int stage)
    {
        // save selected index to global state
        GlobalState.Instance.selectedModelIndex = index;
        SceneManager.LoadScene("MainScene");
    }
}
