using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{
    public AudioSource barrierCollision;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (!barrierCollision.isPlaying)
            {
                //trim audio
                barrierCollision.time = 0.145f;
                barrierCollision.Play();
            }
        }
    }
}
