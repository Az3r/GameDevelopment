using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalState : MonoBehaviour
{
    private static int maxSlots = 13;
    public static GlobalState Instance;
    public GameData gameData;
    public GameObject[] spacecrafts;
    public GameObject SelectedSpaceCraft => spacecrafts[CurrentProgress.modelIndex];
    public SavedData CurrentProgress
    {
        get => saveds[0];
        set => saveds[0] = value;
    }

    public List<SavedData> saveds = new List<SavedData>(maxSlots);

    private void Awake()
    {
        Instance = this;
        LoadSaveFiles();
        // use SingleTon so this object can be safely destroy
        Destroy(this);
    }
    private void LoadSaveFiles()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            saveds.Add(null);
        }

        var path = Path.Combine(Application.dataPath, "saves");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        foreach (var file in Directory.EnumerateFiles(path))
        {
            var data = SavedData.LoadFromFile(file);
            if (data != null)
            {
                saveds[data.slot] = data;
            }
        }
        if (CurrentProgress is null) CurrentProgress = SavedData.Default();
    }
}
