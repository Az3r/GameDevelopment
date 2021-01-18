using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.IO;
using TMPro;

public class ShopController : MonoBehaviour
{
    public GameObject shopSession;
    public GameObject selectSession;
    public GameObject saveLoadSession;
    public Button backButton;
    public Button nextButton;
    public Image currentStage;
    public TextMeshProUGUI saveLoadLabel;
    public TextMeshProUGUI reloadCost;
    public TextMeshProUGUI attackCost;
    public TextMeshProUGUI healthCost;
    public TextMeshProUGUI money;
    public TextMeshProUGUI reloadLevel;
    public TextMeshProUGUI attackLevel;
    public TextMeshProUGUI healthLevel;
    public Button reloadButton;
    public Button attackButton;
    public Button healthButton;
    public List<Sprite> stageImages;
    public List<Button> saveSlots;
    public List<GameObject> models;

    [Header("Observed Fields")]
    [SerializeField]
    private int selectableModelIndex;
    [SerializeField]
    private bool inSaveSession;
    [SerializeField]
    private GameObject current;
    [SerializeField]
    private SavedData progress => GlobalState.Instance.CurrentProgress;
    [SerializeField]
    private GameData data => GlobalState.Instance.gameData;


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
        models[selectableModelIndex].SetActive(true);
        current = models[selectableModelIndex];

        // setup shop session
        SetupShopGUI();

    }
    public void SetupShopGUI()
    {
        // Display proper session depending on player progress
        var isNewGame = progress.modelIndex < 0;
        selectSession.SetActive(isNewGame);
        shopSession.SetActive(!isNewGame);
        saveLoadSession.SetActive(false);

        reloadButton.interactable = progress.reloadLevel < data.reloadCost.Count;
        attackButton.interactable = progress.attackLevel < data.attackCost.Count;
        healthButton.interactable = progress.healthLevel < data.healthCost.Count;

        reloadLevel.text = progress.reloadLevel >= data.reloadCost.Count ? "Max Level" : $"Level {progress.reloadLevel + 1}";
        attackLevel.text = progress.attackLevel >= data.attackCost.Count ? "Max Level" : $"Level {progress.attackLevel + 1}";
        healthLevel.text = progress.healthLevel >= data.healthCost.Count ? "Max Level" : $"Level {progress.healthLevel + 1}";

        reloadCost.text = data.reloadCost[Mathf.Min(progress.reloadLevel, data.reloadCost.Count - 1)].ToString();
        attackCost.text = data.attackCost[Mathf.Min(progress.attackLevel, data.attackCost.Count - 1)].ToString();
        healthCost.text = data.healthCost[Mathf.Min(progress.healthLevel, data.healthCost.Count - 1)].ToString();

        money.text = progress.money.ToString();
        currentStage.sprite = stageImages[progress.currentStage - 1];
    }
    public void SetupSaveLoadGUI()
    {
        saveLoadSession.SetActive(true);
        shopSession.SetActive(false);

        // update GUI
        saveLoadLabel.text = inSaveSession ? "Select a file you wish to save" : "Select a file to load your game";
        for (int i = 0; i < saveSlots.Count; i++)
        {
            var button = saveSlots[i];
            var slot = GlobalState.Instance.saveds[i + 1];
            var slotIsUsed = slot != null;
            button.interactable = inSaveSession || slotIsUsed;

            // unsaved child, when the slot hasn't been used yet
            var saved = button.transform.GetChild(0).gameObject;
            saved.SetActive(!slotIsUsed);
            saved.GetComponent<TextMeshProUGUI>().text = $"Slot {i + 1} is empty";

            // if the slot has been used, display the saved stage and time
            var unsaved = button.transform.GetChild(1).gameObject;
            unsaved.SetActive(slotIsUsed);
            if (slot != null)
            {
                unsaved.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"Stage {slot.currentStage}";
                unsaved.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = slot.savedTimeStr;
            }
        }

    }
    public void NextModel() => GetModel(selectableModelIndex + 1);
    public void PreviousModel() => GetModel(selectableModelIndex - 1);

    private void GetModel(int i)
    {
        // No more out of range exception
        Mathf.Clamp(0, i, models.Count);
        selectableModelIndex = i % models.Count;

        // display new model and continue to rotate with an angle from old model
        var rotation = current.transform.rotation;
        current.SetActive(false);
        current = models[selectableModelIndex];
        current.SetActive(true);
        current.transform.rotation = rotation;

        // Make sure NextButton is disabled at last model,
        // and BackButton is disabled at first model
        backButton.interactable = selectableModelIndex > 0;
        nextButton.interactable = selectableModelIndex < models.Count - 1;
    }

    public void SelectSessionToShopSession()
    {
        // from model selection
        // save selected model index to global state
        GlobalState.Instance.CurrentProgress.modelIndex = selectableModelIndex;
        progress.SaveToFile();

        shopSession.SetActive(true);
        selectSession.SetActive(false);
    }
    public void SaveLoadSessionToShopSession()
    {
        shopSession.SetActive(true);
        saveLoadSession.SetActive(false);
        SetupShopGUI();
    }
    public void OpenSaveLoadSession(bool saving)
    {
        this.inSaveSession = saving;
        SetupSaveLoadGUI();
    }

    public void OnSlotSelected(int slot)
    {
        if (inSaveSession) SaveFile(slot);
        else LoadFile(slot);
    }

    private void SaveFile(int i)
    {
        var savedData = progress.Clone();
        savedData.slot = i;
        savedData.SaveToFile();

        // update global state
        GlobalState.Instance.saveds[i] = savedData;

        // Update UI
        var button = saveSlots[i];
        // unsaved child, when the slot hasn't been used yet
        var saved = button.transform.GetChild(0).gameObject;
        saved.SetActive(false);

        // if the slot has been used, display the saved stage and time
        var unsaved = button.transform.GetChild(1).gameObject;
        unsaved.SetActive(true);
        unsaved.transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = $"Stage {savedData.currentStage}";
        unsaved.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = savedData.savedTimeStr;

    }
    private void LoadFile(int i)
    {
        GlobalState.Instance.CurrentProgress = GlobalState.Instance.saveds[i];
        SaveLoadSessionToShopSession();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void QuitToStartMenu()
    {
        SceneManager.LoadScene("StartScene");
    }
}
