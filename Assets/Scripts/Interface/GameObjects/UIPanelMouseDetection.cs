using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// S'attache à racine des interfaces. Permet de detecter si la souris interagit actuellement avec l'interface.
/// </summary>
[RequireComponent(typeof(Image))]
public class UIPanelMouseDetection : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector]
    //private CinemachineHelper cs;
    public Camera cam;
    public bool menu = false;

    // Start is called before the first frame update
    void Start()
    {
        //cam = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnPointerEnter(PointerEventData eventData)
    {

         menu = true;
        
        //cs.inContext = true;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        menu = false;
        //cs.inContext = false;
    }
}
