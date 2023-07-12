using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class ResetUI : MonoBehaviour
{
    public Text ResetText;
    public Text ResetRemindText;

    private GameObject kart;
    private float stillCheckThreshold = 0.01f;
    private float lastHoldTime;
    private float lastKartMovingTime;
    private Color blinkColor;

    // Start is called before the first frame update
    void Start()
    {
        kart = GameObject.Find("Kart");
        blinkColor = ResetRemindText.color;
        ResetRemindText.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        float kartSpeed = kart.GetComponent<Rigidbody>().velocity.magnitude;
        if (kartSpeed > stillCheckThreshold) {
            lastKartMovingTime = Time.time;
            ResetRemindText.enabled = false;
        }
        if (Time.time - lastKartMovingTime > 6) {
            ResetRemindText.enabled = true;
            blinkColor.a = Mathf.Round(Mathf.PingPong(Time.unscaledTime * 2.0f, 1));
            //Assign the new alpha to the text
            ResetRemindText.color = blinkColor;
        }
        if (!Input.GetKey(KeyCode.R))
        {
            lastHoldTime = Time.time;
            ResetText.enabled = false;
            return;
        }

        int pressSecond = 3 - (int)(Time.time - lastHoldTime);
        if (Time.time > lastHoldTime + 3)
        {
            lastHoldTime = Time.time;
        }
        ResetText.enabled = true;
        ResetText.text = "Resetting in " + pressSecond.ToString() + " sec";

    }
}
