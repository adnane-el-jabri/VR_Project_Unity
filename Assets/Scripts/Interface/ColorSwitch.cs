using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ColorSwitch : MonoBehaviour
{
    [SerializeField]
    private bool isOn;
    public bool ColorSwitchOn = true;
    [Header("Objets supplementaires")]
    public TextMeshProUGUI text;
    public String isOnText;
    public String isOffText;
    [Header("Objets cible")]
    public Light lumiereCible;
    public GameObject obj;
    private Color colorOn = Color.green, colorOff = Color.red;

    private void Start()
    {
        if (ColorSwitchOn == true){
            if (isOn == true)
            {
                gameObject.GetComponent<Image>().color = Color.green;
            }
        } else
        {
            gameObject.GetComponent<Image>().color = Color.white;

        }

    }
    // Update is called once per frame
    void Update()
    {
        if(obj != null || lumiereCible != null)
        {
            SwitchObj();
        }
    }

    private void SwitchObj()
    {
        if(obj != null)
        {
            if(obj.activeInHierarchy == true)
            {
                gameObject.GetComponent<Image>().color = colorOn;
            }
            else
            {
                gameObject.GetComponent<Image>().color = colorOff;
            }

            if (text != null)
            {
                SwitchText(obj.activeInHierarchy);
            }
        } 
        else if (lumiereCible != null)
        {
            if(lumiereCible.enabled == true)
            {
                gameObject.GetComponent<Image>().color = colorOn;
            } else
            {
                gameObject.GetComponent<Image>().color = colorOff;
            }

            if (text != null)
            {
                SwitchText(lumiereCible.enabled);
            }
        }
    }

    public void Switch()
    {
        if(ColorSwitchOn == true)
        {
            if (isOn != true)
            {
                gameObject.GetComponent<Image>().color = colorOn;
            }
            else
            {
                gameObject.GetComponent<Image>().color = colorOff;
            }
        }
        if(text != null)
        {
            SwitchText(isOn);
        }
        isOn = !isOn;
    }

    public void SwitchText(bool on)
    {
        if (on)
        {
            text.text = isOnText;
        } else
        {
            text.text = isOffText;
        }
    }
}
