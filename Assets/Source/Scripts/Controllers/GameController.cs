using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 hazardSpawnLocation;
    public Vector3 playerSpawnLocation;

    public int hazardCount; //Number of hazard
    public float spawnWait; //time between 2 hazard spawn
    public float startWait;
    public float waveWait; //time between 2 wave

    private Coroutine coroutine;
    private void Start()
    {
        Instantiate(GlobalState.Instance.SelectedSpaceCraft, playerSpawnLocation, Quaternion.Euler(-90f, 0f, 0f));
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
}
