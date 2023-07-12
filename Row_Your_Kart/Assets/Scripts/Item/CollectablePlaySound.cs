using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePlaySound : MonoBehaviour
{
    public AudioClip collisionAudio;


    void OnTriggerEnter(Collider hittingCar)
    {
        if (hittingCar.attachedRigidbody != null && hittingCar.CompareTag("Player"))
        {
            AudioSource.PlayClipAtPoint(collisionAudio, transform.position);
        }

    }
}
