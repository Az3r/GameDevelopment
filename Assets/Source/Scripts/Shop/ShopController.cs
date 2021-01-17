using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ShopController : MonoBehaviour
{
    public GameObject shopSession;
    public GameObject selectSession;
    public Button backButton;
    public Button nextButton;
    public TextMeshProUGUI reloadCost;
    public TextMeshProUGUI attackCost;
    public TextMeshProUGUI healthCost;
    public TextMeshProUGUI currency;
    public TextMeshProUGUI reloadLevel;
    public TextMeshProUGUI attackLevel;
    public TextMeshProUGUI healthLevel;
    public Button reloadButton;
    public Button attackButton;
    public Button healthButton;
    public List<Sprite> stageImages;


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

        // setup shop session
        var progress = GlobalState.Instance.currentProgress;


    }
    private void Update()
    {
        backButton.interactable = index > 0;
        nextButton.interactable = index < models.Count - 1;
    }

    public void NextModel() => GetModel(index + 1);
    public void PreviousModel() => GetModel(index - 1);

    private void GetModel(int i)
    {
        // No more out of range exception
        Mathf.Clamp(0, i, models.Count);
        index = i % models.Count;

        // display new model and continue to rotate with an angle from old model
        var rotation = current.transform.rotation;
        current.SetActive(false);
        current = models[index];
        current.SetActive(true);
        current.transform.rotation = rotation;
    }

    public void ToShopSession()
    {
        shopSession.SetActive(true);
        selectSession.SetActive(false);
    }

    public void StartGame(int stage)
    {
        // save selected index to global state
        GlobalState.Instance.selectedModelIndex = index;
        SceneManager.LoadScene("MainScene");
    }
}
