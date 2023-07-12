using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableSoundPlayer : MonoBehaviour
{
    public AudioClip collisionAudio;


    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Player"))
        {

            AudioSource.PlayClipAtPoint(collisionAudio, transform.position);

        }

    }
}
