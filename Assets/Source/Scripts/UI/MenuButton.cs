using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
  public void OnPointerEnter(PointerEventData eventData)
  {
    transform.localScale = new Vector3(1.02f, 1.02f, 1.02f);
  }

  public void OnPointerExit(PointerEventData eventData)
  {
    transform.localScale = new Vector3(1f, 1f, 1f);
  }
}
