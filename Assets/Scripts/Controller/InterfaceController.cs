using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceController : MonoBehaviour
{
    public TMP_Dropdown choix;

    //les polices
    public TMP_FontAsset Luciole;
    public TMP_FontAsset OpenDys;
    public TMP_FontAsset Tiresias;
    public TMP_FontAsset Arial;
    public TMP_FontAsset Liberation;

    public TMP_Text m_TextComponent;

    public TMP_Text label_tc;

    public TMP_Text labelTaillePolice;
    public TMP_Text labelTailleBordures;
    public TMP_Text labelTailleBorduresBoutons;

    public GameObject[] listeGameObjectUI;
    public GameObject[] listeGameObjectMenu;
    public GameObject[] listeGameObjectBoutons;
    public GameObject[] dropdownObject;
    public GameObject[] listeGameObjectSlider;

    public float oldTaille;

    public Color couleurGrisClair;
    public Color couleurGrisFonce;

    public bool verifGras = false;


    public void Awake()
    {

        listeGameObjectUI = GameObject.FindGameObjectsWithTag("Texte");
        dropdownObject = GameObject.FindGameObjectsWithTag("Dropdown");
        listeGameObjectMenu = GameObject.FindGameObjectsWithTag("Menu");
        listeGameObjectBoutons = GameObject.FindGameObjectsWithTag("Bouton");
        listeGameObjectSlider = GameObject.FindGameObjectsWithTag("Slider");
    }

    public void reintialiserIHM()
    {
        choix.value = 0;
        changementPoliceNumber();
        modifTailleFont(1);
        //gras
        if (verifGras == true)
        {
            modifGras(false);
        }
        changeColorMenu1();
        changeBorderMenu(1);
        changeBorderBoutons(1);


    }
    
    #region Police
    public void changementPolice(TMP_FontAsset newfont)
    {
        foreach (GameObject goUI in listeGameObjectUI)
        {
            m_TextComponent = goUI.GetComponent<TMP_Text>();
            m_TextComponent.font = newfont;
        }
        foreach (GameObject goDD in dropdownObject)
        {
            label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
            label_tc.font = newfont;
        }

    }

    public void changementPoliceNumber()
    {
        if (choix.value == 0)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.font = Luciole;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Luciole;
            }
        }
        else if (choix.value == 1)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.font = OpenDys;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = OpenDys;
            }
        }
        else if (choix.value == 2)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.font = Tiresias;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Tiresias;
            }
        }
        else if (choix.value == 3)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.font = Arial;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Arial;
            }
        }
        else if (choix.value == 4)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.font = Liberation;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.font = Liberation;
            }
        }

    }

    public void modifTailleFont(float value)
    {
        foreach (GameObject goUI in listeGameObjectUI)
        {
            m_TextComponent = goUI.GetComponent<TMP_Text>();
            m_TextComponent.fontSize = m_TextComponent.fontSize - oldTaille + value;
        }
        foreach (GameObject goDD in dropdownObject)
        {
            label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
            label_tc.fontSize = label_tc.fontSize - oldTaille + value;
        }
        oldTaille = value;
        string valeur = value.ToString();

        labelTaillePolice.text = valeur;
    }

    public void modifGras(bool value)
    {
        if (value == true)
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.fontStyle = FontStyles.Bold;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.fontStyle = FontStyles.Bold;
            }
           verifGras = true;
        }
        else
        {
            foreach (GameObject goUI in listeGameObjectUI)
            {
                m_TextComponent = goUI.GetComponent<TMP_Text>();
                m_TextComponent.fontStyle = FontStyles.Normal;
            }
            foreach (GameObject goDD in dropdownObject)
            {
                label_tc = goDD.GetComponent<TMP_Dropdown>().itemText;
                label_tc.fontStyle = FontStyles.Normal;
            }
            verifGras = false;
        }
    }
    #endregion

    #region Menu
    public void changeColorMenu(int value)
    {
        if (value == 0)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = Color.white;
            }
        }
        else if (value == 1)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = couleurGrisClair;
            }
        }
        else if (value == 2)
        {
            foreach (GameObject goUI in listeGameObjectMenu)
            {
                goUI.GetComponent<Image>().color = couleurGrisFonce;
            }
        }
    }

    //BLANC
    public void changeColorMenu1()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = Color.white;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectUI)
        {
            goUI.GetComponent<TMP_Text>().color = Color.black;
            
        }

        foreach (GameObject goUI in listeGameObjectBoutons)
        {
            goUI.GetComponent<Image>().color = Color.white;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in dropdownObject)
        {
            goUI.GetComponent<Image>().color = Color.white;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectSlider)
        {
            Slider slider = goUI.GetComponent<Slider>();
            ColorBlock colorBlock = slider.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = Color.black;
            colorBlock.pressedColor = Color.black;
            colorBlock.selectedColor = Color.black;
            slider.colors = colorBlock;
        }
    }

    //GRIS CLAIR
    public void changeColorMenu2()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = couleurGrisClair;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectUI)
        {
            goUI.GetComponent<TMP_Text>().color = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectBoutons)
        {
            goUI.GetComponent<Image>().color = Color.white;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in dropdownObject)
        {
            goUI.GetComponent<Image>().color = Color.white;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectSlider)
        {
            Slider slider = goUI.GetComponent<Slider>();
            ColorBlock colorBlock = slider.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = Color.black;
            colorBlock.pressedColor = Color.black;
            colorBlock.selectedColor = Color.black;
            slider.colors = colorBlock;
        }

    }

    //GRIS FONCE
    public void changeColorMenu3()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = couleurGrisFonce;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in listeGameObjectUI)
        {
            goUI.GetComponent<TMP_Text>().color = Color.white;
        }

        foreach (GameObject goUI in listeGameObjectBoutons)
        {
            goUI.GetComponent<Image>().color = Color.black;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.black;
        }

        foreach (GameObject goUI in dropdownObject)
        {
            goUI.GetComponent<Image>().color = Color.black;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.white;
        }

        foreach (GameObject goUI in listeGameObjectSlider)
        {
            Slider slider = goUI.GetComponent<Slider>();
            ColorBlock colorBlock = slider.colors;
            colorBlock.normalColor = Color.white;
            colorBlock.highlightedColor = Color.black;
            colorBlock.pressedColor = Color.black;
            colorBlock.selectedColor = Color.black;
            slider.colors = colorBlock;
        }
    }

    //NOIR
    public void changeColorMenu4()
    {

        foreach (GameObject goUI in listeGameObjectMenu)
        {
            goUI.GetComponent<Image>().color = Color.black;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.white;
        }

        foreach (GameObject goUI in listeGameObjectUI)
        {
            goUI.GetComponent<TMP_Text>().color = Color.white;
        }

        foreach (GameObject goUI in listeGameObjectBoutons)
        {
            goUI.GetComponent<Image>().color = Color.black;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.white;
        }

        foreach (GameObject goUI in dropdownObject)
        {
            goUI.GetComponent<Image>().color = Color.black;
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectColor = Color.white;
        }

        foreach (GameObject goUI in listeGameObjectSlider)
        {
            Slider slider = goUI.GetComponent<Slider>();
            ColorBlock colorBlock = slider.colors;
            colorBlock.normalColor = couleurGrisFonce;
            colorBlock.highlightedColor = Color.white;
            colorBlock.pressedColor = Color.white;
            colorBlock.selectedColor = Color.white;
            slider.colors = colorBlock;
        }
    }

    public void changeBorderMenu(float value)
    {
        foreach (GameObject goUI in listeGameObjectMenu)
        {
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectDistance = new Vector2(value, value);
            string valeur = value.ToString();
            labelTailleBordures.text = valeur;
        }
    }

    public void changeBorderBoutons(float value)
    {
        foreach (GameObject goUI in listeGameObjectBoutons)
        {
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectDistance = new Vector2(value, value);
            string valeur = value.ToString();
            labelTailleBorduresBoutons.text = valeur;
        }

        foreach (GameObject goUI in dropdownObject)
        {
            Outline outline = goUI.GetComponent<Outline>();
            outline.effectDistance = new Vector2(value, value);
            string valeur = value.ToString();
            labelTailleBorduresBoutons.text = valeur;
        }
    }
    #endregion
}
