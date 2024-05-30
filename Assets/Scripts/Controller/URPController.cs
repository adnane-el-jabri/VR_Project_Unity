using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using TMPro;
using UnityEngine.UI;
using System;

public class URPController : MonoBehaviour
{
    // il y a du tri à faire, bcp de fonctions de gabriel à supprimer + mettre au propre et charger correctement tous les shaders, etc
    // utilisé pour apparences (saturation, luminosité et contraste), et contours

    public RenderPipelineAsset DEPTHSOBELRenderPipelineAsset;
    public RenderPipelineAsset NORMALSSOBELRenderPipelineAsset;
    public RenderPipelineAsset COULEURSOBELRenderPipelineAsset;
    public RenderPipelineAsset NEUTRERenderPipelineAsset;

    public RendererSettingsHelper rendererSettingsHelper;

    public UniversalRendererData DEPTHrendererData;
    public UniversalRendererData NORMALrendererData;
    public UniversalRendererData COLORrendererData;
    public UniversalRendererData NEUTRErendererData;

    public Color color;
    public Color orange;
    public Color violet;
    public float border = 0.000594468f;
    public TMP_Text labelContraste;
    public TMP_Text labelLuminosite;
    public TMP_Text labelSaturation;
    public TMP_Text labelBordureContours;


    #region variables
    [Header("Debug Use")]
    [SerializeField] private List<MeshRenderer> target;
    [SerializeField] private List<Material> targetM;
    [Header("Debug Use")]
    [SerializeField] private GameObject postprocess;
    [Header("Materials")]
    public List<Material> materials;
    public Material _OutlineMaterial;
    private Material OutlineMaterial;


    public Material DEPTHMaterial;
    public Material NORMALSMaterial;
    public Material COLORMaterial;
    public Material ALLMaterial;

    public Material _DefaultMaterial;
    private Material DefaultMaterial;
    public Material _GradientMaterial;
    private Material GradientMaterial;

    public Color maColor;

    public TMP_Dropdown choix;

    public bool flou = false;

    //public Material contour2D;


    #endregion
    #region helper functions
    void Start()
    {
        //GraphicsSettings.renderPipelineAsset = NEUTRERenderPipelineAsset;
        //_OutlineMaterial = SOBELRenderPipelineAsset.defaultMaterial;
        // !! attention pour le moment il faut bien penser à changer le render pipeline asset renderer material + le outline contour de notre shader helper !!!
        Debug.Log("Fonction Start");
        DefaultMaterial = new Material(_DefaultMaterial);
        OutlineMaterial = new Material(_OutlineMaterial);
        GradientMaterial = new Material(_GradientMaterial);
        target = mainScene.Instance.mesh;
        postprocess = FindObjectOfType<Volume>().gameObject;
    }

    #endregion
    #region post-process
    //Post-processing URP, accès par profile du volume

    //on est sûr qu'elles servent :
    //Fonction Saturation
    public void ChangeVolumeSaturation(float val)
    {
        Debug.Log("Fonction ChangeVolumeSaturation");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        labelSaturation.text = valeur + " %";
    }

    //Fonction luminosité
    public void ChangeVolumeLuminosite(float val)
    {
        Debug.Log("Fonction ChangeVolumeLuminosite");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.postExposure.value = val;
        float valTemp = val * 100;
        int valEntier = (int)valTemp;
        string valeur = valEntier.ToString();
        labelLuminosite.text = valeur + " %";
    }

    //Fonction contraste
    public void ChangeVolumeContrast(float val)
    {
        Debug.Log("Fonction ChangeVolumeContraste");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.contrast.value = val;
        int valeurEntier = (int)val;
        string valeur = valeurEntier.ToString();
        labelContraste.text = valeur + " %";
    }

    //Fonction change fond scene
    public void ChangeFondScene(float val)
    {
        Debug.Log("Fonction ChangeFondScene");
        GameObject MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        Camera cam = MainCamera.GetComponent<Camera>();
        cam.backgroundColor = Color.Lerp(Color.white, Color.black, val);
    }

    public void reinitialiserApparence()
    {
        ChangeVolumeSaturation(0);
        ChangeVolumeContrast(0);
        ChangeVolumeLuminosite(0);

        GameObject.Find("SliderContraste").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderLuminosité").GetComponent<Slider>().value = 0;
        GameObject.Find("SliderSaturation").GetComponent<Slider>().value = 0;

    }

    public void reinitialiserApparenceTT()
    {
        ChangeVolumeSaturation(0);
        ChangeVolumeContrast(0);
        ChangeVolumeLuminosite(0);
    }

        public void reinitialiserIHMFond()
    {
        ChangeFondScene(0);
    }

    #endregion
    #region ChangeMaterials

    public void modifBorderShader(float value)
    {
        Debug.Log("Fonction modifBorderShader");
        border = value;
        float valTemp = value * 10000;
        int valArrondie = (int)valTemp;
        string valeur = valArrondie.ToString();
        labelBordureContours.text = valeur;
        changeMaterialContour();
    }

    public void modifColorShader1()
    {
        Debug.Log("Fonction modifColorShader noir");
        color = Color.black;
        changeMaterialContour();
    }

    public void modifColorShader2()
    {
        Debug.Log("Fonction modifColorShader blanc");
        color = Color.white;
        changeMaterialContour();
    }

    public void modifColorShader3()
    {
        Debug.Log("Fonction modifColorShader rouge");
        color = Color.red;
        changeMaterialContour();
    }

    public void modifColorShader4()
    {
        Debug.Log("Fonction modifColorShader vert");
        color = Color.green;
        changeMaterialContour();
    }

    public void modifColorShader5()
    {
        Debug.Log("Fonction modifColorShader jaune");
        color = Color.yellow;
        changeMaterialContour();
    }

    public void modifColorShader6()
    {
        Debug.Log("Fonction modifColorShader bleu");
        color = Color.blue;
        changeMaterialContour();
    }

    public void modifColorShader7()
    {
        Debug.Log("Fonction modifColorShader orange");
        color = orange;
        changeMaterialContour();
    }

    public void modifColorShader8()
    {
        Debug.Log("Fonction modifColorShader violet");
        color = violet;
        changeMaterialContour();
    }

    //Fonction Contours => début prog : val =0
    public void changeMaterialContour()
    {
        Debug.Log("Fonction changeMaterialContour");

        if (choix.value == 0)
        {
            Debug.Log("Fonction changeMaterialContour : valeur 0");
            GraphicsSettings.renderPipelineAsset = DEPTHSOBELRenderPipelineAsset;
            _OutlineMaterial = DEPTHMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = DEPTHrendererData;
        }
        else if (choix.value == 1)
        {
            Debug.Log("Fonction changeMaterialContour : valeur 1");
            GraphicsSettings.renderPipelineAsset = NORMALSSOBELRenderPipelineAsset;
            _OutlineMaterial = NORMALSMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = NORMALrendererData;
        }
        else if (choix.value == 2)
        {
            Debug.Log("Fonction changeMaterialContour : valeur 2");
            GraphicsSettings.renderPipelineAsset = COULEURSOBELRenderPipelineAsset;
            _OutlineMaterial = COLORMaterial;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = COLORrendererData;
        }
        else
        {
            Debug.Log("Fonction changeMaterialContour : valeur else");
            GraphicsSettings.renderPipelineAsset = NEUTRERenderPipelineAsset;
            _OutlineMaterial.SetColor("_Color", color);
            _OutlineMaterial.SetFloat("_Thickness", border);
            rendererSettingsHelper.rd = NEUTRErendererData;
        }
    }

    #endregion
    #region atester
    //à tester :
    /************************************************************************************************************************************************/
    public void AddToVolumeSaturation(float val)
    {
        Debug.Log("Fonction AddVolumeSaturation");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.saturation.value += val;
    }

    public void AddToVolumeContrast(float val)
    {
        Debug.Log("Fonction AddVolumeContraste");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.contrast.value += val;
    }

    public void ChangeVolumeHue(float val)
    {
        Debug.Log("Fonction ChangeVolumeHue");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.hueShift.value = val;
    }
    public void AddToVolumeHue(float val)
    {
        Debug.Log("Fonction AddVolumeHue");
        ColorAdjustments color;
        postprocess.GetComponent<Volume>().profile.TryGet<ColorAdjustments>(out color);
        color.hueShift.value += val;
    }

    public void changeMaterialGradient()
    {
        Debug.Log("Fonction ChangeMaterialGradient");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction ChangeMaterialGradient : changement des gradients dans la boucle");
            target.material = GradientMaterial;
        }
    }

    public void changeMaterialBetweenNormalAndContour()
    {
        Debug.Log("Fonction changeMaterialBetweenNormalAndContour");
        if (target[1].sharedMaterial.name == DefaultMaterial.name)
        {
            Debug.Log("Fonction changeMaterialBetweenNormalAndContour : si sharedMat = defaultMat");
            foreach (MeshRenderer target in target)
            {
                Debug.Log("Fonction changeMaterialBetweenNormalAndContour : boucle du si");
                target.material = OutlineMaterial;
            }

        }
        else
        {
            Debug.Log("Fonction changeMaterialBetweenNormalAndContour : si sharedMat != defaultMat");
            foreach (MeshRenderer target in target)
            {
                Debug.Log("Fonction changeMaterialBetweenNormalAndContour : boucle du else");
                target.material = DefaultMaterial;
            }
        }
    }

    public void changeMaterial(Material m)
    {
        Debug.Log("Fonction changeMaterial classique (material m)");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction changeMaterial classique : for mat m");
            target.material = m;
        }
    }

    public void changeMaterial(int m)
    {
        Debug.Log("Fonction changeMaterial classique (int m)");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction changeMaterial classique : for int m");
            target.material = materials[m];
        }
    }

    public void AccessColorShader(Color c)
    {
        Debug.Log("Fonction AccessColorShader");
        _OutlineMaterial.SetColor("_Color", c);
    }

    public void AccessDepthPowerShader(float value)
    {
        Debug.Log("Fonction AccessDepthPowerShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AccessDepthPowerShader : boucle for");
            target.material.SetFloat("_OutlineDepthStrength", value);
        }
    }

    public void AccessNormalsPowerShader(float value)
    {
        Debug.Log("Fonction AccessNormalPowerShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AccessNormalPowerShader : boucle for");
            target.material.SetFloat("_OutlineNormalStrength", value);
        }
    }

    public void AccessThicknessShader(float value)
    {
        Debug.Log("Fonction AccessThicknessShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AccessThicknessShader : boucle for");
            target.material.SetFloat("_OutlineThickness", value);
        }
    }

    public void AccessDepthTighteningShader(float value)
    {
        Debug.Log("Fonction AccessDepthTighteningShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AccessDepthTighteningShader : boucle for");
            target.material.SetFloat("_OutlineDepthTightening", value);
        }
    }
    public void AccessNormalsTighteningShader(float value)
    {
        Debug.Log("Fonction AccessNormalsTighteningShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AccessNormalsTighteningShader : boucle for");
            target.material.SetFloat("_OutlineNormalTightening", value);
        }
    }
    public void NormalOnShader()
    {
        Debug.Log("Fonction NormalOnShader");
        float v = target[0].material.GetFloat("_NormalsOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach (MeshRenderer target in target)
        {
            target.material.SetFloat("_NormalsOn", v);
        }
    }

    public bool getNormalsShader()
    {
        Debug.Log("Fonction getNormalsShader");
        float v = target[0].material.GetFloat("_NormalsOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        bool c = v == 1 ? false : true;
        return c;
    }
    public void DepthOnShader()
    {
        Debug.Log("Fonction DepthOnShader");
        float v = target[0].material.GetFloat("_DepthOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach (MeshRenderer target in target)
        {
            target.material.SetFloat("_DepthOn", v);
        }
    }

    public bool getDepthShader()
    {
        Debug.Log("Fonction getDepthShader");
        float v = target[0].material.GetFloat("_DepthOn");
        Debug.Log(v);
        v = resolveIntBool(v);
        bool c = v == 1 ? false : true;
        return c;

    }

    public void CoefDepthShader(float value)
    {
        Debug.Log("Fonction CoefDepthShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction CoefDepthShader : boucle for");
            target.material.SetFloat("_CoefficientDepth", value);
        }
    }

    public void CoefNormalsShader(float value)
    {
        Debug.Log("Fonction CoefNormalsShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction CoefNormalsShader : boucle for");
            target.material.SetFloat("_CoefficientNormals", value);
        }
    }

    public void ContourStrictShader()
    {
        Debug.Log("Fonction ContourStrictShader");
        float v = target[0].material.GetFloat("_ContourStrict");
        Debug.Log(v);
        v = resolveIntBool(v);
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction ContourStrictShader : boucle for");
            target.material.SetFloat("_ContourStrict", v);
        }
    }

    public string getCurrentMaterialShader()
    {
        Debug.Log("Fonction getCurrentMaterialShader");
        return target[0].sharedMaterial.name;
    }

    public int resolveIntBool(float v)
    {
        Debug.Log("Fonction resolveIntBool");
        if (v == 1)
        {
            v = 0;
        }
        else
        {
            v = 1;
        }
        return (int)v;
    }

    public void AnimTimeShader(float value)
    {
        Debug.Log("Fonction AnimTimeShader");
        foreach (MeshRenderer target in target)
        {
            Debug.Log("Fonction AnimTimeShader : boucle for");
            target.material.SetInt("_OutlineTimeAnim", (int)value);
        }
    }

    public void Reinitialiser()
    {
        Debug.Log("Fonction réinitialiser");
        DefaultMaterial = new Material(_DefaultMaterial);
        OutlineMaterial = new Material(_OutlineMaterial);
        GradientMaterial = new Material(_GradientMaterial);

        foreach (MeshRenderer a in target)
        {
            a.material.CopyPropertiesFromMaterial(DefaultMaterial);
        }
    }
    /************************************************************************************************************************************************/
    #endregion
}
