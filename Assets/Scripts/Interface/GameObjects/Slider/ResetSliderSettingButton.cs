using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetSliderSettingButton : MonoBehaviour
{
    [Tooltip("Slider cible")]
    [SerializeField]
    private Slider slider;
    private float defaultValue;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = defaultValue;
    }


    public void ResetSlider()
    {
        slider.value = defaultValue;
    }
}
