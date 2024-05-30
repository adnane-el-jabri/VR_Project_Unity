using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;
using TMPro;

public class mainScene : MonoBehaviour
{
    [Header("Le modèle 3D de la scène")]
    public GameObject gameObjectOfModel;
    public GameObject gameObjectActif;
    public GameObject gameObjectActifPivot;
    /*Possible de mettre plusieurs GameObjects dans la scene*/
    public GameObject[] listeModeles;
    /*Pivot = pour faire le point fixé par la caméra*/
    public GameObject[] listeModelesPivot;
    public int boucleModel;

    [SerializeField]
    public List<MeshRenderer> mesh;
    public Light scenelight;
    public Volume volume;

    private bool InterfaceUtilisateurOn = true;
    private CinemachineHelper cinehelp; //le JL
    private LightController lighthandler;

    public bool switchModeles = false;
    List<string> gameObjectNamesDropDown;
    public TMP_Dropdown objet;

    public int boucleIndices = 0;
    //tests et si besoin de matériaux
    public Material matVide;
    public Material matTempModele;
    public Material matTempPivot;
    public bool textureRayures;
    public bool navigationOk = false;
    public lookCam camScript;
    public URPController shaderModif;
    public bool contoursMode = false;
    public bool saturationMode = false;

    private static mainScene _instance;

    public static mainScene Instance { get { return _instance; } }


    private void Awake()
    {
        objet = GameObject.Find("DropdownObjets").GetComponent<TMP_Dropdown>();
        gameObjectOfModel = GameObject.FindWithTag("Modele3D");
        listeModeles = GameObject.FindGameObjectsWithTag("Objet");

        listeModeles = listeModeles.OrderBy(go => go.name).ToArray();

        /* **** DROPDOWN OBJETS  **** si plusieurs objets dans la scene */
        List<string> gameObjectNamesDropDown = new List<string>();
        foreach (GameObject go in listeModeles)
        {
            gameObjectNamesDropDown.Add(go.name);
        }


        if (objet != null)
        {
            if (gameObjectNamesDropDown.Count > 0)
            {
                objet.AddOptions(gameObjectNamesDropDown);
            }
            else
            {
                Debug.LogWarning("Aucun GameObject trouvé dans la scène.");
            }
        }
        else
        {
            Debug.LogError("Veuillez assigner un TextMeshProDropdown dans l'inspecteur Unity.");
        }

        // ************************************

        listeModelesPivot = GameObject.FindGameObjectsWithTag("Pivot");

        listeModelesPivot = listeModelesPivot.OrderBy(go => go.name).ToArray();


        scenelight = FindObjectOfType<Light>();
        lighthandler = FindObjectOfType<LightController>();
        gameObjectActif = listeModeles[0];
        gameObjectActifPivot = listeModeles[0];
        for (int i = 1; i < listeModeles.Length; i++)
        {
            listeModeles[i].SetActive(false);
        }
        boucleModel = 0;
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
        mesh = gameObjectOfModel.transform.GetComponentsInChildren<MeshRenderer>(true).ToList();
        volume = FindObjectOfType<Volume>();
        initialiseMat();
        textureRayures = false;
    }

    private void Update()
    {
        //pour passer à l'obj suivant, barre espace
        if (Input.GetKeyDown(KeyCode.Space))
        {
            reinitialiseMat();
            SwitchModele();
        }

    }

    public LightController getLight()
    {
        return lighthandler;
    }

    public void SwitchModele()
    {
        boucleModel = boucleModel + 1;
        listeModeles[boucleModel - 1].SetActive(false);
        if (boucleModel == listeModeles.Length)
        {
            boucleModel = 0;
        }
        listeModeles[boucleModel].SetActive(true);
        gameObjectActif = listeModeles[boucleModel];
        gameObjectActifPivot = listeModelesPivot[boucleModel];
        initialiseMat();
        textureRayures = false;
        reinitialiserTout();
    }

    public void SwitchModeleParam(int value)
    {
        switchModeles = true;
        for (int i = 0; i < listeModeles.Length; i++)
        {
            listeModeles[i].SetActive(false);
        }
        gameObjectActif = listeModeles[value];
        gameObjectActifPivot = listeModelesPivot[boucleModel];
        listeModeles[value].SetActive(true);
        boucleModel = value;
        initialiseMat();
        textureRayures = false;
        
        reinitialiserTout();
    }

    public void reinitialiserTout()
    {
        camScript.reinitialiserPosition();
        camScript.reinitialiserZoom();
        shaderModif.reinitialiserApparenceTT();
        shaderModif.choix.value = 3;
        shaderModif.changeMaterialContour();
        lighthandler.Reinitialiser();
        lighthandler.ReinitialiserDollyLightModel();
    }


    public void initialiseMat()
    {
        matTempModele = gameObjectActif.GetComponentInChildren<MeshRenderer>().material;
    }

    public void reinitialiseMat()
    {
        gameObjectActif.GetComponentInChildren<MeshRenderer>().material = matTempModele;
    }


    public void activeTextureplaquee()
    {
        if(textureRayures == false)
        {
            gameObjectActif.GetComponentInChildren<MeshRenderer>().material = matVide;
            String boutonLabel = "Disable\nTexture";
            GameObject.Find("ButtonActifTexture").GetComponentInChildren<TMP_Text>().text = boutonLabel;
            textureRayures = true;
        }
        else
        {
            gameObjectActif.GetComponentInChildren<MeshRenderer>().material = matTempModele;
            String boutonLabel = "Active\nTexture";
            GameObject.Find("ButtonActifTexture").GetComponentInChildren<TMP_Text>().text = boutonLabel;
            textureRayures = false;
        }
    }

}
