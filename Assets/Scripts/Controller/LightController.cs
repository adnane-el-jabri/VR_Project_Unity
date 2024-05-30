using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LightController : MonoBehaviour
{
    // tout ce qui concerne la lumière (menu lumière et menu jeux de lumière)
    public TMP_Dropdown choixLum;

    public TMP_Text labelTemperature;
    public TMP_Text labelIntensite;
    public TMP_Text labelIntensityJL;

    public Light lumierePrincipal;
    public Light lumiereHaut;
    public Light lumiereBas;

    public Light lightTarget;
    public List<Light> dollyLightModel;

    public Tuple<Vector3, float, float> defaultValuePrincipal { get; set; }
    public Tuple<Vector3, float, float> defaultValueHaut { get; set; }
    public Tuple<Vector3, float, float> defaultValueBas { get; set; }
    private GameObject dollyLightModelParent;

    private bool dollyLightModelSpeed = false;

    public float intensity;
    public float temperature;

    public Slider slideLum;
    public Slider slideTemp;

    public Slider slidIntJL;

    void Start()
    {
        lightTarget = lumierePrincipal;
    }

    private void Update()
    {
        modifLightTarget();
    }

    public void Reinitialiser()
    {
        intensity = 3;
        temperature = 10603;

        lumierePrincipal.intensity = intensity;
        lumierePrincipal.colorTemperature = temperature;
        lumiereHaut.intensity = intensity;
        lumiereBas.intensity = intensity;
        lumiereHaut.colorTemperature = temperature;
        lumiereBas.colorTemperature = temperature;

        lightTarget = lumierePrincipal;

        /*PENSER A AJOUTER LAFFICHAGE GRAPHIQUE DE LA REINITIALISATION*/

        lumiereHaut.gameObject.SetActive(false);
        lumiereBas.gameObject.SetActive(false);
        lumierePrincipal.gameObject.SetActive(true);

        slideLum.value = intensity;
        labelIntensite.text = intensity + " %";

        slideTemp.value = temperature;
        labelTemperature.text = "Neutral";


    }


    public void SwitchDollyLightModel(int index)
    {
        dollyLightModel[index].enabled = !dollyLightModel[index].enabled;
    }

    public void SwitchDollyLightModelOnly(int index)
    {
        foreach (Light l in dollyLightModel)
        {
            if (l != dollyLightModel[index])
            {
                l.enabled = false;
            }
        }
        dollyLightModel[index].enabled = !dollyLightModel[index].enabled;

        if (dollyLightModel[index].intensity == 0)
        {
            intensityDollyLightModel(3);
            dollyLightModel[1].gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Speed = 1;
        }
    }

    public void intensityDollyLightModel(float value)
    {
        if (dollyLightModel[1].enabled)
        {
            dollyLightModel[1].intensity = value;
            int val = (int)value;
            string valeur = val.ToString();
            labelIntensityJL.text = valeur + " %";
        }
    }

    //Réinitialiser pour JEUX DE LUMIERE
    public void ReinitialiserDollyLightModel()
    {
        dollyLightModel[1].gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Position = 0;
        dollyLightModel[1].gameObject.transform.parent.GetComponent<CinemachineDollyCart>().m_Speed = 0;
        intensityDollyLightModel(0);

        foreach (Light i in dollyLightModel)
        {
            i.enabled = false;
        }

        slidIntJL.value = 0;
        labelIntensityJL.text = "0 %";
    }

    public void DesactiverDollyLightModel()
    {
        foreach (Light i in dollyLightModel)
        {
            i.enabled = false;
            
        }
    }

    public void StopDollyLightModel(int index)
    {
        dollyLightModelParent = dollyLightModel[index].gameObject.transform.parent.gameObject;

        if (dollyLightModelSpeed != false)
        {
            dollyLightModelParent.GetComponent<CinemachineDollyCart>().m_Speed = 1;
        }
        else
        {
            dollyLightModelParent.GetComponent<CinemachineDollyCart>().m_Speed = 0;
        }
        dollyLightModelSpeed = !dollyLightModelSpeed;
    }

    public void changeIntensity(float value)
    {
        intensity = value;
        int pourcen = (int)value * 10;
        string valeur = pourcen.ToString();
        labelIntensite.text = valeur + " %";
    }

    public void changeTemperature(float value)
    {
        temperature = value;
        if (temperature < 8608)
        {
            labelTemperature.text = "Warm";
        }
        else if (temperature > 12099)
        {
            labelTemperature.text = "Cool";
        }
        else
        {
            labelTemperature.text = "Neutral";
        }

    }



    public void HandleDropDownCMPT()
    {
        if (choixLum.value == 0)
        {
            lumierePrincipal.gameObject.SetActive(true);
            lumiereHaut.gameObject.SetActive(false);
            lumiereBas.gameObject.SetActive(false);
            lightTarget = lumierePrincipal;
        }
        else if (choixLum.value == 1)
        {
            lumiereHaut.gameObject.SetActive(true);
            lumierePrincipal.gameObject.SetActive(false);
            lumiereBas.gameObject.SetActive(false);
            lightTarget = lumiereHaut;
        }
        else if (choixLum.value == 2)
        {
            lumiereBas.gameObject.SetActive(true);
            lumiereHaut.gameObject.SetActive(false);
            lumierePrincipal.gameObject.SetActive(false);
            lightTarget = lumiereBas;
        }
    }

    public void SetPositionX(float val)
    {
        Vector3 pos = lumierePrincipal.gameObject.transform.position;
        pos.x = val;
        lumierePrincipal.gameObject.transform.position = pos;
    }
    public void SetPositionY(float val)
    {
        Vector3 pos = lumierePrincipal.gameObject.transform.position;
        pos.y = val;
        lumierePrincipal.gameObject.transform.position = pos;
    }

    public void SetRotationX(float val)
    {
        Quaternion pos = lumierePrincipal.gameObject.transform.rotation;
        pos.x = val;
        lumierePrincipal.gameObject.transform.rotation = pos;
    }
    public void SetRotationY(float val)
    {
        Quaternion pos = lumierePrincipal.gameObject.transform.rotation;
        pos.y = val;
        lumierePrincipal.gameObject.transform.rotation = pos;
    }

    public void modifLightTarget()
    {
        lightTarget.intensity = intensity;
        lightTarget.colorTemperature = temperature;
    }
}
