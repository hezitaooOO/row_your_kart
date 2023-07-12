using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CollectableInvisibleController : MonoBehaviour
{
    private float invisibleStartTime;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider hittingCar)
    {
        if (hittingCar.attachedRigidbody != null && hittingCar.CompareTag("Player"))
        {
            KartInvisibleMaker invisibleMaker = hittingCar.GetComponent<KartInvisibleMaker>();
            invisibleMaker.makeSemiTransparent();
            Debug.Log("Made transparent");
            //invisibleStartTime = System.DateTime.now.Second

            Rigidbody hittingCarRb  = hittingCar.GetComponent<Rigidbody>();
            hittingCarRb.isKinematic = false;


            Destroy(this.gameObject);
        }

    }
}
