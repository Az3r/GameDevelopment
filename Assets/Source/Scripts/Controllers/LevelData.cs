using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Level Data", fileName = "LevelData")]
[System.Serializable]
public class LevelData : ScriptableObject
{
    // Start is called before the first frame update
    public GameObject[] enermies;
    public GameObject[] asteroids;
    public GameObject[] items;

    public int waves = 4; //Number of wave
    public int hazardCount; //Number of hazard
    public float spawnWait; //time between 2 hazard spawn
    public float startWait;
    public float waveWait; //time between 2 wave

    
}
