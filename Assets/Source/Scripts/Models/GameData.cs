using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game Data", fileName = "GameData")]
[System.Serializable]
public class GameData : ScriptableObject
{
    public int maxLevel;
    public List<int> attackCost;
    public List<int> reloadCost;
    public List<int> healthCost;
}
