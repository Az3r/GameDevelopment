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
    public SavedData currentProgress => saveds[0];

    public List<SavedData> saveds = new List<SavedData>(5);

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        LoadSaveFiles();
    }

    private void LoadSaveFiles()
    {
        for (int i = 0; i < 4; i++)
        {
            saveds.Add(null);
        }
        if (Directory.Exists("saves"))
        {
            foreach (var file in Directory.EnumerateFiles("saves"))
            {
                var data = SavedData.Load(file);
                saveds[data.slot] = data;
            }
        }
    }
}
