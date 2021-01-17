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

    public static SavedData Load(string name)
    {
        try
        {
            var path = Path.Combine("saves", name);
            var json = File.ReadAllText(path);
            return JsonUtility.FromJson<SavedData>(json);
        }
        catch (System.Exception)
        {
            return null;
        }
    }
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
            modelIndex = 0
        };
    }
    public bool Save(int slot)
    {
        try
        {
            this.slot = slot;
            var path = Path.Combine("saves", slot.ToString("###"));
            var json = JsonUtility.ToJson(this);
            File.WriteAllText(path, json);
            return true;
        }
        catch (System.Exception)
        {
            return false;
        }
    }
}
