using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalState : MonoBehaviour
{
    public GameObject[] spacecrafts;
    public GameObject SelectedSpaceCraft => spacecrafts[selectedModelIndex];
    public int selectedModelIndex = 0;
    public static GlobalState Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }
}
