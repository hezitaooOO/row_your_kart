using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KartConfig : MonoBehaviour
{

    public Slider maxSpeedSlider;
    public Slider accelerationSlider;
    public Slider weightSlider;
    public Slider steerSlider;
    public Slider frictionSlider;

    public static float maxSpeed = 120;
    public static float acceleration = 120;
    public static float weight = 700;
    public static float steer = 20;
    public static float friction = 2;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ResetValue()
    {
        maxSpeedSlider.value = 120;
        accelerationSlider.value = 120;
        weightSlider.value = 700;
        steerSlider.value = 20;
        frictionSlider.value = 2;
    }

    // Update is called once per frame
    void Update()
    {
        maxSpeed = maxSpeedSlider.value;
        acceleration = accelerationSlider.value;
        weight = weightSlider.value;
        steer = steerSlider.value;
        friction = frictionSlider.value;
    }
}
