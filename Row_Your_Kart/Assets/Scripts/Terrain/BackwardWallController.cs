using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackwardWallController : MonoBehaviour
{
    public AudioSource barrierCollision;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<BoxCollider>().enabled = player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().backwardWallActivated;

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

