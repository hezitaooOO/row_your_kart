using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LapsConfig : MonoBehaviour
{
    public Slider lapsSlider;

    public static int laps = 2;

    Text lapText;

    public void Start()
    {
        lapText = GetComponentInChildren<UnityEngine.UI.Text>();
    }
    public void ResetValue()
    {
        lapsSlider.value = 2;
    }
    public void SaveValue()
    {
        laps =(int)lapsSlider.value;
    }
    // Update is called once per frame
    void Update()
    {
        laps = (int)lapsSlider.value;
        lapText.text = lapsSlider.value.ToString();
    }
}
