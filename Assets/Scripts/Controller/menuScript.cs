using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class menuScript : MonoBehaviour
{
    public GameObject MenuTraitements;
    public GameObject MenuIHM;

    public GameObject MenuObjets;
    public GameObject MenuHandicap;

    public GameObject menuPolice;
    public GameObject menuMenu;
    public GameObject menuTheme;
    public GameObject menuDeplacement;
    public GameObject boutonFermerMenu;

    public TMP_Dropdown dropdown;

    public GameObject menuCadrage;
    public GameObject menuApparence;
    public GameObject menuContours;
    public GameObject menuLumiere;
    public GameObject menuJL;

    // penser à charger les menus avant l'ouverture de l'appli sinon les modifs ne sont pas prises en compte (component disabled)


    void Start()
    {
        if(menuPolice.activeSelf) menuPolice.SetActive(false);
        if(menuMenu.activeSelf) menuMenu.SetActive(false);
        if(menuTheme.activeSelf) menuTheme.SetActive(false);

        if (boutonFermerMenu.activeSelf) boutonFermerMenu.SetActive(false);
        
        if (menuCadrage.activeSelf) menuCadrage.SetActive(false);
        if (menuApparence.activeSelf) menuApparence.SetActive(false);
        if (menuContours.activeSelf) menuContours.SetActive(false);
        if (menuLumiere.activeSelf) menuLumiere.SetActive(false);
        if (menuJL.activeSelf) menuJL.SetActive(false);

        if (MenuObjets.activeSelf) MenuObjets.SetActive(false);
        if (MenuHandicap.activeSelf) MenuHandicap.SetActive(false);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F2))
        {
            if(MenuTraitements.activeSelf)
            {
                MenuTraitements.SetActive(false);
            }
            else
            {
                MenuTraitements.SetActive(true);
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(MenuIHM.activeSelf)
            {
                MenuIHM.SetActive(false);
            }
            else
            {
                MenuIHM.SetActive(true);
            }
        }
        if(Input.GetKeyDown(KeyCode.F1))
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
        if (Input.GetKeyDown(KeyCode.F3))
        {
            if (MenuHandicap.activeSelf)
            {
                MenuHandicap.SetActive(false);
            }
            else
            {
                MenuHandicap.SetActive(true);
            }
        }


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

    public void deplaceMenu1Gauche()
    {
        var newMenu = menuDeplacement.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(-1541, 0);
    }

    public void deplaceMenu1Droite()
    {
        var newMenu = menuDeplacement.GetComponent<RectTransform>();
        newMenu.anchoredPosition = new Vector2(0.001220703f, 0);
    }

    // quand on avait un bouton
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
        pauseMenu.isOn = false;
        dropdown.value = 3;
    }

    //actuellement on a le menu sous forme de dropdown
    //quand ferme le dropdown, soucis au relancement si l'option est déjà cliquée
    public void openMenuDropdown(int value)
    {
        boutonFermerMenu.SetActive(true);
        //0 = polices, 1 = menu, 2 = fond et 3 = fermer le menu
        if (value == 0)
        {
            menuPolice.SetActive(true);
            menuMenu.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 1)
        {
            menuMenu.SetActive(true);
            menuPolice.SetActive(false);
            menuTheme.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 2)
        {
            menuTheme.SetActive(true);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = true;
        }
        else if(value == 3)
        {
            menuTheme.SetActive(false);
            menuMenu.SetActive(false);
            menuPolice.SetActive(false);
            pauseMenu.isOn = false;
            boutonFermerMenu.SetActive(false);
        }

       
    }

}
