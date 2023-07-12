using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GeneralTextUI : MonoBehaviour
{
    public Text generalText;

    private float invokeTime;

    private AudioSource warning;
    private void Start()
    {
        warning = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(Time.time - invokeTime > 3)
        {
            generalText.enabled = false;
        }
    }
    public void enableGeneralText(string text)
    {
        generalText.enabled = true;
        generalText.text = text;
        warning.Play();
        invokeTime = Time.time;
    }
}
