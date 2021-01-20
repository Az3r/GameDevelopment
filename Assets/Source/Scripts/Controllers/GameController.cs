using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public GameObject pauseGUI;

    public GameObject[] hazards;
    public Vector3 hazardSpawnLocation;
    public Vector3 playerSpawnLocation;

    public int hazardCount; //Number of hazard
    public float spawnWait; //time between 2 hazard spawn
    public float startWait;
    public float waveWait; //time between 2 wave

    private Coroutine coroutine;

    [Header("Observed Fields")]
    [SerializeField]
    private SpaceshipController player;
    private PlayerInput inputs;
    private IncreaseScore increaseScore;
    private void Awake()
    {
        Application.targetFrameRate = 240;
        Instance = this;
    }
    private void Start()
    {
        var playerGameObject = Instantiate(GlobalState.Instance.SelectedSpaceCraft, playerSpawnLocation, Quaternion.Euler(-90f, 0f, 0f));
        player = playerGameObject.GetComponent<SpaceshipController>();
        inputs = GetComponent<PlayerInput>();
        increaseScore = GetComponent<IncreaseScore>();
        coroutine = StartCoroutine(SpawnWaves());
    }

    IEnumerator<WaitForSeconds> SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
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
    public void QuitToShopScene()
    {
        SceneManager.LoadScene("ShopScene");
    }
    public void AddScore(int value)
    {
        increaseScore.SetScore(increaseScore.maxScore + value);
    }
}
