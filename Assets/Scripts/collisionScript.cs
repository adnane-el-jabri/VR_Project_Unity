using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collisionScript : MonoBehaviour
{

    public Vector3 previousPosition;

    void Start()
    {
        previousPosition = transform.position;
        Debug.Log("start " + previousPosition);
    }

    void OnTriggerEnter(Collider other)
    {
        transform.position = previousPosition;
        Debug.Log("fonction " + transform.position);
    }
}


