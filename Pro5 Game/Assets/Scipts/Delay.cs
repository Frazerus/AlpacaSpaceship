using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Delay : MonoBehaviour
{
    public Text text;
    public Slider slider;
    public void SetDelay()
    {
        StaticData.delay = slider.value / 100;
        text.text = (slider.value / 100).ToString();
    }

    public void Start()
    {
        text.text = (slider.value / 100).ToString();
    }
}
