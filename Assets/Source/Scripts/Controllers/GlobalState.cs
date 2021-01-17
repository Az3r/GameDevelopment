using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class GlobalState : MonoBehaviour
{
    public static GlobalState Instance;
    public GameObject[] spacecrafts;
    public GameObject SelectedSpaceCraft => spacecrafts[selectedModelIndex];
    public int selectedModelIndex = 0;

    public List<SavedData> saveds = new List<SavedData>();

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
        foreach (var file in Directory.EnumerateFiles("saves"))
        {
            saveds.Add(SavedData.Load(file));
        }
    }
}
