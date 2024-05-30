using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderValue : MonoBehaviour
{
    [Tooltip("Slider cible")]
    private Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = gameObject.GetComponent<Slider>();
    }


    public void ChangeSliderValue(float value)
    {
        if(slider.value < slider.maxValue)
        {
            slider.value += value;
        }
    }
}
