using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class RendererSettingsHelper : MonoBehaviour
{
    [SerializeField] UniversalRendererData rendererData;
    public UniversalRendererData rd;

    [SerializeField] private GameObject postprocess;

    public TMP_Text labelNettete;

    // si besoin de simuler du flou => ce sont des assets à download en plus dans le projet (blur)


    public void Awake()
    {
        var blurFeature = rendererData.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();

        if (blurFeature == null) return;

        blurFeature.settings.Sharpness = 0;

        postprocess = FindObjectOfType<Volume>().gameObject;

        rendererData.SetDirty();
    }

    public void reinitialiserNettete()
    {
        changeSharpen(0);
        GameObject.Find("SliderNetteté").GetComponent<Slider>().value = 0;
    }

    public void changeSharpen(float value)
    {
        var blurFeature = rd.rendererFeatures.OfType<SharpenUrp>().FirstOrDefault();
        Debug.Log("Fonction netteté, elle passe par le rendererSettingsHelper");
        Debug.Log(blurFeature);

        if (blurFeature == null) return;

        blurFeature.settings.Sharpness = value;
        Debug.Log(value);
        int val = (int)(value * 100);
        string valeur = val.ToString();
        labelNettete.text = valeur + " %";

        rd.SetDirty();

    }

}
