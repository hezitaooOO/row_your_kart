using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpeedupBar : MonoBehaviour
{
    private const float MAX_FILL_AMOUNT = 100f;
    private float speedupCharge = MAX_FILL_AMOUNT / 3;
    private Image speedupBar;
    private float consumeSpeed = 0.2f;

    void Start()
    {
        speedupBar = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        speedupBar.fillAmount = speedupCharge / MAX_FILL_AMOUNT;
    }

    public void consumeSpeedup() 
    {
        this.speedupCharge -= consumeSpeed;
    }

    public void chargeSpeedup() {

        this.speedupCharge = Mathf.Min(this.speedupCharge + 30, MAX_FILL_AMOUNT);
    }
    public float getSpeedupCharge() {
        return this.speedupCharge;
    }

    public void driftingCharge()
    {
        this.speedupCharge = Mathf.Min(this.speedupCharge + 0.3f, MAX_FILL_AMOUNT);
    }
}
