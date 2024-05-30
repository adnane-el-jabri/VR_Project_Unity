using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class lookCam : MonoBehaviour
{
    /*on récupère la position de la liste des gameobjects (et notamment du premier)*/
    public mainScene cs;
    /*l'objet a afficher - mis à jour avec la fonction switch*/
    public Transform objetTransform;
    /*var pour calculer les décalages*/
    public Vector3 _cameraOffset;
    public Quaternion cameraRotation;
    /*la position initiale de la cam*/
    public Vector3 positionInitiale;
    /*vitesse de navigation*/
    public float rotationSpeed = 2.0f;
    //public float zoomSpeed = 2.0f;
    /*pour mettre des limites vertical*/
    public float minVerticalAngle = -89.0f;
    public float maxVerticalAngle = 89.0f;

    //navigation
    public float x = 0.0f;
    public float y = 0.0f;
    //MAJ position
    Quaternion rotation;
    Vector3 position;

    //verif si on est dans l'espace de navigation ou le menu
    public GameObject menuUser;
    public Vector3 initialMousePosition;

    //pour la collision
    public int _count;
    public bool reinXY = false;

    void Awake()
    {
        objetTransform = cs.listeModelesPivot[0].transform;
        _cameraOffset = transform.position - objetTransform.position;
        positionInitiale = _cameraOffset;
        cameraRotation = transform.rotation;
       _count = 0;
    }

    private void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        initialMousePosition = Input.mousePosition;

    }

    public void reinitialiserPosition()
    {
        rotation = cameraRotation;
        reinXY = true;
    }

    public void reinitialiserZoom()
    {
        _cameraOffset = positionInitiale;
        transform.position = objetTransform.position + _cameraOffset;
    }

    void LateUpdate()
    {
        if (pauseMenu.isOn == false && menuUser.GetComponent<OnMouseOverScript>().menu == false)
        {
            Navigate();
            Zoom();
        }

        /*commun navigation et zoom*/
        position = rotation * _cameraOffset + objetTransform.position;
        transform.rotation = rotation;
        transform.position = position;
        transform.LookAt(objetTransform);

        /*changement modeles*/
        if (Input.GetKeyDown(KeyCode.Space))
        {
            objetTransform = cs.gameObjectActifPivot.transform;
        }
        if (cs.switchModeles == true)
        {
            objetTransform = cs.gameObjectActifPivot.transform;
            cs.switchModeles = false;
        }
    }

    void Navigate()
    {
        if(reinXY == true)
        {
            x = 0;
            y = 0;
            reinXY = false;
        }
        if (Input.GetMouseButton(0))
        {
            x += Input.GetAxis("Mouse X") * rotationSpeed;
            y -= Input.GetAxis("Mouse Y") * rotationSpeed;

            //Debug.Log("y1 = " + y);
            //pour mettre des limites vertical
            y = Mathf.Clamp(y, minVerticalAngle, maxVerticalAngle);

            rotation = Quaternion.Euler(y, x, 0);
        }

    }

    void Zoom()
    {
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        if (Input.GetKey(KeyCode.KeypadPlus) && isCollision())
        {
            _cameraOffset.z = _cameraOffset.z + 0.01f;
        }
        if(Input.GetKey(KeyCode.KeypadMinus))
        {
            _cameraOffset.z = _cameraOffset.z - 0.01f;
        }
        if (scrollInput > 0 && isCollision())
        {
            _cameraOffset.z = _cameraOffset.z + 0.1f;
        }
        if (scrollInput < 0)
        {
            _cameraOffset.z = _cameraOffset.z - 0.1f;
        }        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("ontrigg enter");
        ++_count;
    }

    void OnTriggerExit(Collider other)
    {
        --_count;
    }

    bool isCollision()
    {
        return _count == 0;
    }

}
