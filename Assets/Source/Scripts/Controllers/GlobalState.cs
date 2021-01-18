using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance;
    public GameData gameData;
    public GameObject[] spacecrafts;
    public GameObject SelectedSpaceCraft => spacecrafts[selectedModelIndex];
    public int selectedModelIndex = 0;
    public SavedData CurrentProgress
    {
        get => saveds[0];
        set => saveds[0] = value;
    }

    public List<SavedData> saveds = new List<SavedData>(5);

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
        LoadSaveFiles();
    }

    private void Start()
    {
    }

    private void LoadSaveFiles()
    {
        for (int i = 0; i < 5; i++)
        {
            saveds.Add(null);
        }

        var path = Path.Combine(Application.dataPath, "saves");
        if (!Directory.Exists(path))
            Directory.CreateDirectory(path);
        foreach (var file in Directory.EnumerateFiles(path))
        {
            var data = SavedData.Load(file);
            saveds[data.slot] = data;
        }
        if (CurrentProgress is null) CurrentProgress = SavedData.Default();
    }
}
