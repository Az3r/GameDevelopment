using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SavedData
{
    public int slot;
    public int currentStage;
    public int modelIndex;
    public int money;
    public int attackLevel;
    public int healthLevel;
    public int reloadLevel;

    public static SavedData Default()
    {
        return new SavedData()
        {
            attackLevel = 0,
            healthLevel = 0,
            reloadLevel = 0,
            money = 0,
            currentStage = 1,
            slot = 0,
            modelIndex = -1
        };
    }
    public bool Save()
    {
        try
        {
            var path = Path.Combine(Application.dataPath, "saves", $"{slot}.json");
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(path, json);

            Debug.Log($"Save slot {slot} suffessfully");
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError(e.Message);
            return false;
        }
    }
    public static SavedData Load(string name)
    {
        try
        {
            var path = Path.Combine(Application.dataPath, "saves", name);
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<SavedData>(json);
        }
        catch (System.Exception)
        {
            return null;
        }
    }
}
