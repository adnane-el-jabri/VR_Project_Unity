using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnMouseOverScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public bool menu = false;


    public void OnPointerEnter(PointerEventData eventData)
    {
        menu = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        menu = false;
    }
}

