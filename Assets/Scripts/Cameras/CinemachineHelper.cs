using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static Cinemachine.AxisState;

/// <summary>
/// Fonctions pour gerer les cameras Cinemachine : Activation/desactivation etc...
/// </summary>
public class CinemachineHelper : MonoBehaviour
{
    CinemachineVirtualCamera[] allCams;
    //Pour detecter quand la souris quitte l'ui panel, et donc permettre la rotation
    //[HideInInspector]
    public bool inContext = false;
    private float starttime;
    //private float holdtime = 1.0f;
    public bool Recentrage = true;
    public float stockSourisAxis;
    public float stockSourisOrdonne;
    [Header("Default Camera")]
    [Tooltip("Toutes les caméras seront desactives a part celle-la.")]
    public CinemachineVirtualCamera DefaultCamera;

    /// <summary>
    /// On recupere chaque camera existante, et initialise la scene avec seulement la camera par defaut. 
    /// </summary>
    void Start()
    {
        allCams = FindObjectsOfType<CinemachineVirtualCamera>();
        DisableRotationAll();
        SetInactiveAll();
        DefaultCamera.gameObject.SetActive(true);
        stockSourisAxis = Input.GetAxis("Mouse Y");
        stockSourisOrdonne = Input.GetAxis("Mouse X");

        DisableRotationAll();
    }
    public void setRecentrage()
    {
        Recentrage = !Recentrage;
    }
    private void Update()
    {

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera currentcam = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        //stockSourisAxis = Input.GetAxis("Mouse Y");
        //stockSourisOrdonne = Input.GetAxis("Mouse X");


        /*if ((stockSourisAxis!=Input.GetAxis("Mouse Y") || stockSourisOrdonne != Input.GetAxis("Mouse X")) && DefaultCamera.gameObject.activeSelf == false)
        {
            DefaultCamera.gameObject.SetActive(true);
        }*/



        if (currentcam.GetCinemachineComponent<CinemachinePOV>() != null)
        {
            //Est ce qu'on active le recentrage automatique de la camera ou non 
            if (Recentrage == true && currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering.m_enabled != true)
            {
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering = new Recentering(true, 1, 2);
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalRecentering = new Recentering(true, 1, 2);
            }
            else if (Recentrage == false)
            {
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalRecentering = new Recentering(false, 1, 2);
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalRecentering = new Recentering(false, 1, 2);

            }
        }
        //Si l'utilisateur n'est pas actuellement pas dans les menus, le zoom et la rotation sont accessibles
        /*if (inContext == false)
        {

            currentcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance -= Input.GetAxis("Mouse ScrollWheel") * 2.0f;

            //Si un clic est détecté, on commence le compte à rebours
            if (Input.GetMouseButtonDown(0))
            {
                starttime = Time.time;
            }
            //Tant que le bouton gauche de la souris est enfonce, on accepte la rotation sinon on retire les axes pour arreter la rotation
            if (Input.GetMouseButton(0) && currentcam.GetCinemachineComponent<CinemachinePOV>() != null)
            {
                if (starttime + holdtime >= Time.time)
                {
                    currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
                    currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
                }
            }
            else if(currentcam.GetCinemachineComponent<CinemachinePOV>() != null)
            {
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisValue = 0;
                currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisValue = 0;

            }
        }*/
    }


    //Cree une fonctionnalite de zoom sur la caméra actuellement contrôlée
    void LateUpdate()
    {
        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera currentcam = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;

    }


    public void SetInactiveAll()
    {

        allCams = FindObjectsOfType<CinemachineVirtualCamera>();
        foreach (CinemachineVirtualCamera cvc in allCams)
        {
            cvc.gameObject.SetActive(false);
        }
    }
    public void SetInactiveAllExcept(CinemachineVirtualCamera cam)
    {

        allCams = FindObjectsOfType<CinemachineVirtualCamera>();

        cam.PreviousStateIsValid = false;
        foreach (CinemachineVirtualCamera cvc in allCams)
        {
            cvc.PreviousStateIsValid = false;
            cvc.m_Priority = 0;
            cvc.gameObject.SetActive(false);
        }
        cam.m_Priority = 11;

        cam.gameObject.SetActive(true);
    }

    public void enableRotation(CinemachineVirtualCamera cam)
    {
        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera currentcam = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
        currentcam.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";


    }

    public void ControlRotation()
    {
        allCams = FindObjectsOfType<CinemachineVirtualCamera>();

        CinemachineBrain brain = FindObjectOfType<CinemachineBrain>();
        CinemachineVirtualCamera currentcam = (CinemachineVirtualCamera)brain.ActiveVirtualCamera;
        if (currentcam.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName == "")
        {
            foreach (CinemachineVirtualCamera cvc in allCams)
            {
                cvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "Mouse X";
                cvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "Mouse Y";
            }

        }

        else
        {
            foreach (CinemachineVirtualCamera cvc in allCams)
            {
                cvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                cvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
            }
        }
    }

    public void DisableRotationAll()
    {
        foreach (CinemachineVirtualCamera cvc in allCams)
        { 
            if (cvc.GetCinemachineComponent<CinemachinePOV>() != null)
            {
                cvc.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_InputAxisName = "";
                cvc.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_InputAxisName = "";
            }
        }

    }

}
