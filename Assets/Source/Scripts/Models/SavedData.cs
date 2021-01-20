using UnityEngine;
using System.IO;

[System.Serializable]
public class SavedData : ISerializationCallbackReceiver
{
    public int slot;
    public int currentStage;
    public int modelIndex;
    public int money;
    public int attackLevel;
    public int healthLevel;
    public int reloadLevel;
    public string savedTimeStr;
    public System.DateTime savedTime;

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
            modelIndex = -1,
        };
    }
    public bool SaveToFile()
    {
        try
        {
            // set the time this file is saved
            savedTime = System.DateTime.Now;
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
    public static SavedData LoadFromFile(string name)
    {
        try
        {
            var path = Path.Combine(Application.dataPath, "saves", name);
            var json = File.ReadAllText(path);
            var savedData = JsonUtility.FromJson<SavedData>(json);
            Debug.Log($"Load file {name} successfully");
            return savedData;
        }
        catch (System.Exception)
        {
            // Debug.LogError(e.Message);
            return null;
        }
    }

    public void OnBeforeSerialize()
    {
        savedTimeStr = System.DateTime.Now.ToString();
    }

    public void OnAfterDeserialize()
    {
        if (savedTimeStr is null)
            savedTime = System.DateTime.Now;
        else savedTime = System.DateTime.Parse(savedTimeStr);
    }
    public SavedData Clone()
    {
        return new SavedData()
        {
            attackLevel = this.attackLevel,
            healthLevel = this.healthLevel,
            reloadLevel = this.reloadLevel,
            money = this.money,
            currentStage = this.currentStage,
            slot = this.slot,
            modelIndex = this.modelIndex,
            savedTime = this.savedTime,
            savedTimeStr = this.savedTimeStr
        };
    }
}
