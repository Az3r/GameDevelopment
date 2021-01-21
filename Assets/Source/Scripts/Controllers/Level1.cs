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
        level.waves = 3;
        while (current< level.waves)
        {
            Debug.Log(current);
            for (int i = 0; i < level.hazardCount; i++)
            {

                GameObject obj = null;
                if (current == 0) {
                    obj = level.enermies[0];
                }
                if (current == 1)
                {
                    obj = level.enermies[1];
                }
                if (current == 2)
                {
                    obj = level.enermies[2];
                }
                if (current == 3)
                {
                    obj = level.enermies[Random.Range(0, level.enermies.Length)];
                }
                if (current == 4)
                {
                    obj = level.asteroids[Random.Range(0, level.asteroids.Length)];
                }
                if (current == 5)
                {
                    obj = level.asteroids[Random.Range(0, level.asteroids.Length)];
                }
                if (current == 6)
                {
                    obj = level.asteroids[Random.Range(0, level.asteroids.Length)];
                }
                if (current == 7)
                {
                    obj = level.enermies[Random.Range(0, level.enermies.Length)];
                }
                if (current == 8)
                {
                    obj = level.enermies[Random.Range(0, level.enermies.Length)];
                }
                if (current == 9)
                {
                    obj = level.enermies[Random.Range(0, level.enermies.Length)];
                }
                if (current == 10)
                {
                    obj = level.enermies[Random.Range(0, level.enermies.Length)];
                }

                Vector3 spawnPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (obj != null)
                {
                    Instantiate(obj, spawnPosition, spawnRotation);
                }
                yield return new WaitForSeconds(level.spawnWait);
            }
            current++;
            GameObject item = level.items[Random.Range(0, level.items.Length)];
            Vector3 itemPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
            Quaternion itemRotation = Quaternion.identity;
            if (item != null)
            {
                Instantiate(item, itemPosition, itemRotation);
            }
            yield return new WaitForSeconds(level.waveWait);
        }


        //Boss
        GameObject boss = level.boss;
        Vector3 bossPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
        Quaternion bossRotation = Quaternion.identity;
        Instantiate(boss, bossPosition, bossRotation);
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

}
