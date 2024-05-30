using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseOverFonction : MonoBehaviour
{
    public bool menu = false;

    void OnMouseOver()
        {
            menu = true;
        }

        void OnMouseExit()
        {
        menu = false;
             }
    }

