using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public List<LevelData> levels;
    public Vector3 hazardSpawnLocation = new Vector3(29, 15, 0);

    private SavedData progress => GlobalState.Instance.CurrentProgress;
    private LevelData data => levels[Mathf.Min(levels.Count - 1, progress.currentStage)];
    private Coroutine coroutine;
    // Start is called before the first frame update
    void Start()
    {
        coroutine = StartCoroutine(SpawnWaves());
    }

    IEnumerator<WaitForSeconds> SpawnWaves()
    {
        yield return new WaitForSeconds(data.startWait);
        int current = 0;
        data.waves = 3;
        while (current < data.waves)
        {
            Debug.Log(current);
            for (int i = 0; i < data.hazardCount; i++)
            {

                GameObject obj = null;
                if (current == 0)
                {
                    obj = data.enermies[0];
                }
                if (current == 1)
                {
                    obj = data.enermies[1];
                }
                if (current == 2)
                {
                    obj = data.enermies[2];
                }
                if (current == 3)
                {
                    obj = data.enermies[Random.Range(0, data.enermies.Length)];
                }
                if (current == 4)
                {
                    obj = data.asteroids[Random.Range(0, data.asteroids.Length)];
                }
                if (current == 5)
                {
                    obj = data.asteroids[Random.Range(0, data.asteroids.Length)];
                }
                if (current == 6)
                {
                    obj = data.asteroids[Random.Range(0, data.asteroids.Length)];
                }
                if (current == 7)
                {
                    obj = data.enermies[Random.Range(0, data.enermies.Length)];
                }
                if (current == 8)
                {
                    obj = data.enermies[Random.Range(0, data.enermies.Length)];
                }
                if (current == 9)
                {
                    obj = data.enermies[Random.Range(0, data.enermies.Length)];
                }
                if (current == 10)
                {
                    obj = data.enermies[Random.Range(0, data.enermies.Length)];
                }

                Vector3 spawnPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
                Quaternion spawnRotation = Quaternion.identity;
                if (obj != null)
                {
                    Instantiate(obj, spawnPosition, spawnRotation);
                }
                yield return new WaitForSeconds(data.spawnWait);
            }
            current++;
            GameObject item = data.items[Random.Range(0, data.items.Length)];
            Vector3 itemPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
            Quaternion itemRotation = Quaternion.identity;
            if (item != null)
            {
                Instantiate(item, itemPosition, itemRotation);
            }
            yield return new WaitForSeconds(data.waveWait);
        }


        //Boss
        GameObject boss = data.boss;
        if (boss != null)
        {
            Vector3 bossPosition = new Vector3(Random.Range(-hazardSpawnLocation.x, hazardSpawnLocation.x), hazardSpawnLocation.y, hazardSpawnLocation.z);
            Quaternion bossRotation = Quaternion.identity;
            Instantiate(boss, bossPosition, bossRotation);
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(coroutine);
    }

}
