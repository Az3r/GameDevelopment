using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level1 : MonoBehaviour
{
    
    public Vector3 hazardSpawnLocation = new Vector3(29, 15, 0);
    public LevelData level;
    

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
        yield return new WaitForSeconds(level.startWait);
        int current = 0;
        while (current< level.waves)
        {
            for (int i = 0; i < level.hazardCount; i++)
            {
                GameObject hazard = level.enermies[Random.Range(0, level.enermies.Length)];
                Vector3 spawnPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
                Quaternion spawnRotation = Quaternion.identity;
                Instantiate(hazard, spawnPosition, spawnRotation);
                yield return new WaitForSeconds(level.spawnWait);
            }
            current++;
            yield return new WaitForSeconds(level.waveWait);
        }
        //End of level

    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

    void InitObject(GameObject gameObject, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Instantiate(gameObject, spawnPosition, spawnRotation);
    }
}
