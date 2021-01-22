using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject pauseGUI;
    public GameObject failGUI;
    public GameObject winGUI;
    public List<GameObject> healthIcons;
    public List<GameObject> ultimateIcons;
    public List<Material> backgrounds;
    public MeshRenderer backgroundRenderer;


    public Vector3 playerSpawnLocation;



    private PlayerInput inputs;
    private IncreaseScore increaseScore;
    private SavedData progress => GlobalState.Instance.CurrentProgress;
    private GameData data => GlobalState.Instance.gameData;

    [Header("Observed Fields")]
    [SerializeField]
    private SpaceshipController player;
    private void Awake()
    {
        Application.targetFrameRate = 240;
        Instance = this;
    }
    private void Start()
    {
        if (player is null)
        {
            var playerGameObject = Instantiate(GlobalState.Instance.SelectedSpaceCraft, playerSpawnLocation, Quaternion.Euler(-90f, 0f, 0f));
            player = playerGameObject.GetComponent<SpaceshipController>();
        }
        // player stats
        player.meta.attack += progress.attackLevel;
        player.meta.health += progress.healthLevel;
        player.meta.reload -= 0.09f * progress.reloadLevel;


        inputs = GetComponent<PlayerInput>();
        increaseScore = GetComponent<IncreaseScore>();
        SetupGUI();
    }
    private void SetupGUI()
    {
        int maxHealth = player.meta.health;
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].SetActive(i < maxHealth);
        }
        backgroundRenderer.material = backgrounds[Mathf.Min(backgrounds.Count - 1, progress.currentStage)];

    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        player.OnShoot(context);
    }
    public void OnMoveHorizontal(InputAction.CallbackContext context)
    {
        player.MoveHorizontal(context);
    }
    public void OnMoveVertical(InputAction.CallbackContext context)
    {
        player.MoveVertical(context);
    }
    public void Pause(InputAction.CallbackContext context)
    {
        Time.timeScale = 0f;
        pauseGUI.SetActive(true);
        inputs.SwitchCurrentActionMap("Menu");

    }
    public void Resume()
    {
        Time.timeScale = 1f;
        pauseGUI.SetActive(false);
        inputs.SwitchCurrentActionMap("Player");
    }
    public void Restart()
    {
        SceneManager.LoadScene("MainScene");
        Resume();
    }
    public void ToShopScene(int stageIncrease = 0)
    {
        progress.currentStage += stageIncrease;
        if (stageIncrease > 0) progress.SaveToFile();
        SceneManager.LoadScene("ShopScene");
    }
    public void AddScore(int value)
    {
        increaseScore.SetScore(increaseScore.maxScore + value);
    }
    public void UpdateHealthUI(int currentHealth)
    {
        for (int i = 0; i < healthIcons.Count; i++)
        {
            healthIcons[i].SetActive(i < currentHealth);
        }
    }
    public void DisplayFailScreen()
    {
        failGUI.SetActive(true);
        inputs.SwitchCurrentActionMap("None");
    }

    public void DiplayWinningScreen()
    {
        winGUI.SetActive(true);
        inputs.SwitchCurrentActionMap("None");
    }

}
