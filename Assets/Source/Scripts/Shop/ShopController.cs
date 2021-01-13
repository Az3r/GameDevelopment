using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    public List<GameObject> models;

    private GameObject current;
    [Header("Observed Fields")]
    [SerializeField]
    private int index;


    private void Awake()
    {
        // make framerate fixed
        Application.targetFrameRate = 60;
        Instance = this;
    }
    private void Start()
    {
        // Hide all models but current selected one
        foreach (var model in models)
        {
            model.SetActive(false);
        }
        models[index].SetActive(true);
        current = models[index];
    }

    public void NextModel() => GetModel(index + 1);
    public void PreviousModel() => GetModel(index - 1);

    private void GetModel(int i)
    {
        var rotation = current.transform.rotation;
        index = i % models.Count;
        current.SetActive(false);
        current = models[index];
        current.SetActive(true);
        current.transform.rotation = rotation;
    }
}
