using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaterBar : MonoBehaviour
{

    public Slider slider;

    public void SetMaxWaterLevel(float maxWater)
    {
        slider.maxValue = maxWater;
    }

    public void SetWaterLevel(float water)
    {
        slider.value = water;
    }
}