using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject MenuTraitements;
    public GameObject MenuIHM;

    public GameObject MenuObjets;

    public GameObject menuPolice;
    public GameObject menuMenu;
    public GameObject menuTheme;
    public GameObject menuDeplacement;
    public GameObject boutonFermerMenu;
    public GameObject boutonReinitialiser;

    public int menuActif = 3;

    public TMP_Dropdown dropdown;

    public GameObject menuCadrage;
    public GameObject menuApparence;
    public GameObject menuContours;
    public GameObject menuLumiere;
    public GameObject menuJL;

    // pour la partie 2D
    public GameObject DDContours;
    public GameObject boutonMenuLumiere;
    public GameObject boutonMenuJL;

    //AJOUT MODE STIM
    public mainScene ms;
    public GameObject hideNavigationMenu;
    public GameObject hideSaturation;
    public GameObject hideContoursMenu;
    public GameObject hideContoursChoix;

    // charger les menus avant l'ouverture de l'appli

    void Start()
    {
        if (menuPolice.activeSelf) menuPolice.SetActive(false);
        if (menuMenu.activeSelf) menuMenu.SetActive(false);
        if (menuTheme.activeSelf) menuTheme.SetActive(false);

        if (boutonFermerMenu.activeSelf) boutonFermerMenu.SetActive(false);
        if (boutonReinitialiser.activeSelf) boutonReinitialiser.SetActive(false);

        if (menuCadrage.activeSelf) menuCadrage.SetActive(false);
        if (menuApparence.activeSelf) menuApparence.SetActive(false);
        if (menuContours.activeSelf) menuContours.SetActive(false);
        if (menuLumiere.activeSelf) menuLumiere.SetActive(false);
        if (menuJL.activeSelf) menuJL.SetActive(false);

        if (MenuObjets.activeSelf) MenuObjets.SetActive(false);

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F12))
        {
            if (menuDeplacement.activeSelf)
            {
                menuDeplacement.SetActive(false);
            }
            else
            {
                menuDeplacement.SetActive(true);
            }

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (MenuIHM.activeSelf)
            {
                MenuIHM.SetActive(false);
            }
            else
            {
                MenuIHM.SetActive(true);
            }
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            if (MenuObjets.activeSelf)
            {
                MenuObjets.SetActive(false);
            }
            else
            {
                MenuObjets.SetActive(true);
            }
        }

    }

    public void reinitialiserIHMMenu()
    {
        deplaceMenu1Droite();

        menuPolice.SetActive(true);
        GameObject.Find("SliderTaillePolice").GetComponent<Slider>().value = 1;
        GameObject.Find("ToggleGras").GetComponent<Toggle>().isOn = false;
        menuPolice.SetActive(false);

        menuMenu.SetActive(true);
        GameObject.Find("SliderTailleBordures").GetComponent<Slider>().value = 1;
        GameObject.Find("SliderTailleBorduresBtn").GetComponent<Slider>().value = 1;
        menuMenu.SetActive(false);

        menuTheme.SetActive(true);
        GameObject.Find("SliderColorFond").GetComponent<Slider>().value = 0;
        menuTheme.SetActive(false);

        openMenuDropdown(menuActif);
    }

    public void retourMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        if (menuCadrage.activeSelf) menuCadrage.SetActive(false);
        if (menuApparence.activeSelf) menuApparence.SetActive(false);
        if (menuContours.activeSelf) menuContours.SetActive(false);
        if (menuLumiere.activeSelf) menuLumiere.SetActive(false);
        if (menuJL.activeSelf) menuJL.SetActive(false);

    }

    public void cadrageMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        menuCadrage.SetActive(true);
    }

    public void apparenceMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        menuApparence.SetActive(true);
    }

    public void contoursMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        menuContours.SetActive(true);
    }

    public void lumiereMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        menuLumiere.SetActive(true);
    }

    public void JLMenu()
    {
        if (MenuTraitements.activeSelf)
        {
            MenuTraitements.SetActive(false);
        }
        else
        {
            MenuTraitements.SetActive(true);
        }

        menuJL.SetActive(true);
    }

    //depend de la taille de l'ecran
    public void deplaceMenu1Gauche()
    {
        var newMenu = menuDeplacement.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(-1541, 540);
    }

    public void deplaceMenu1Droite()
    {
        var newMenu = menuDeplacement.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(1, 540);
    }

    // avec bouton
    public void closeMenuParam()
    {
        if (menuPolice.activeSelf)
        {
            menuPolice.SetActive(false);
        }

        if (menuMenu.activeSelf)
        {
            menuMenu.SetActive(false);
        }

        if (menuTheme.activeSelf)
        {
            menuTheme.SetActive(false);
        }

        if (boutonFermerMenu.activeSelf)
        {
            boutonFermerMenu.SetActive(false);
        }
        if (boutonReinitialiser.activeSelf)
        {
            boutonReinitialiser.SetActive(false);
        }
        pauseMenu.isOn = false;
        dropdown.value = 3;
    }

    //on a le menu sous forme de dropdown
    public void openMenuDropdown(int value)
    {
        boutonFermerMenu.SetActive(true);
        boutonReinitialiser.SetActive(true);
        //0 = polices, 1 = menu, 2 = fond et 3 = fermer le menu
        if (value == 0)
        {
            menuActif = 0;
            menuPolice.SetActive(true);
            menuMenu.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 1)
        {
            menuActif = 1;
            menuMenu.SetActive(true);
            menuPolice.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 2)
        {
            menuActif = 2;
            menuTheme.SetActive(true);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if (value == 3)
        {
            menuActif = 3;
            menuTheme.SetActive(false);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = false;
            boutonFermerMenu.SetActive(false);
            boutonReinitialiser.SetActive(false);
        }


    }
}
