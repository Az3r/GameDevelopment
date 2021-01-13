using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModelItem : MonoBehaviour
{
    private MeshFilter filter;
    private void Start()
    {
        filter = GetComponent<MeshFilter>();
    }
    private void OnMouseUp()
    {
        SpaceCraftStore.Instance.SetSpaceCraft(filter.mesh);
    }
}
