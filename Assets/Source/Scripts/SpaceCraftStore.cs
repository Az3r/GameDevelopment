using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceCraftStore : MonoBehaviour
{
    public static SpaceCraftStore Instance;
    public List<Mesh> models;
    public List<Material> colors;

    public GameObject target;
    public GameObject firstColor;
    public GameObject firstModel;

    [Header("Spacing")]
    public float colorSpacing;
    public float modelSpacing;

    private MeshRenderer targetRenderer;
    private MeshFilter targetFilter;

    public Mesh GetShape(int i) => models[i];
    public Material GetColor(int i) => colors[i];
    public void SetSpaceCraft(Mesh model, Material color)
    {
        SetSpaceCraft(model);
        SetSpaceCraft(color);
    }
    public void SetSpaceCraft(Mesh model) => targetFilter.mesh = model;

    public void SetSpaceCraft(Material color) => targetRenderer.material = color;

    private void Awake()
    {
        // make framerate fixed
        Application.targetFrameRate = 60;
        Instance = this;
    }
    private void Start()
    {
        targetRenderer = target.GetComponent<MeshRenderer>();
        targetFilter = target.GetComponent<MeshFilter>();
        SpawnColors();
        SpawnModels();
    }

    private void SpawnColors()
    {
        var position = firstColor.transform.position;
        var scale = firstColor.transform.localScale;
        for (int i = 0; i < colors.Count; i++)
        {
            var color = colors[i];
            var x = position.x;
            var obj = Instantiate(
                firstColor,
                new Vector3(x + (scale.x + colorSpacing) * i, position.y, position.z),
                Quaternion.identity
             );
            obj.GetComponent<MeshRenderer>().material = color;
        }
    }
    private void SpawnModels()
    {
        var position = firstModel.transform.position;
        var scale = firstModel.transform.localScale;
        for (int i = 0; i < models.Count; i++)
        {
            var model = models[i];
            var x = position.x;
            var obj = Instantiate(
                firstModel,
                new Vector3(x + modelSpacing * i, position.y, position.z),
                firstModel.transform.rotation
             );
            obj.GetComponent<MeshFilter>().mesh = model;
        }
    }
}
