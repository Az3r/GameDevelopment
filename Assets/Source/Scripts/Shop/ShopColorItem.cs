using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ShopColorItem : MonoBehaviour, IPointerDownHandler
{
    private new MeshRenderer renderer;
    private void Start()
    {
        renderer = GetComponent<MeshRenderer>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("hello");
        SpaceCraftStore.Instance.SetSpaceCraft(renderer.material);
    }
}
