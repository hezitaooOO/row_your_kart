using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectableSpeedupController : MonoBehaviour
{
    public Image speedupBarImage;
    private SpeedupBar speedupBar;
    void Start()
    {
        speedupBar = speedupBarImage.GetComponent<SpeedupBar>();
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hittingCar)
    {
        if (hittingCar.attachedRigidbody != null && hittingCar.CompareTag("Player"))
        {
            speedupBar.chargeSpeedup();
            Destroy(this.gameObject);
        }

    }
}
