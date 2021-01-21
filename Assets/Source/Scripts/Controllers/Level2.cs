using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2 : MonoBehaviour
{
    public GameObject[] hazards;
    public Vector3 hazardSpawnLocation = new Vector3(29, 15, 0);

    public int waves = 4; //Number of wave
    public int hazardCount; //Number of hazard
    public float spawnWait; //time between 2 hazard spawn
    public float startWait;
    public float waveWait; //time between 2 wave

    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(SpawnWaves());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator<WaitForSeconds> SpawnWaves()
    {
        yield return new WaitForSeconds(startWait);
        int current = 0;
        while (current < waves)
        {
            for (int i = 0; i < hazardCount; i++)
            {
                GameObject hazard = hazards[Random.Range(0, hazards.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(spawnWait);
            }
            current++;
            yield return new WaitForSeconds(waveWait);
        }
        //End of level

        //SceneManager.LoadScene("MainScene2");
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }
}
