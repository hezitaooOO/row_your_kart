using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapsCounter : MonoBehaviour
{
    private int elapsedLaps = 0;
    private Boolean isColliding = false;
    public bool backwardWallActivated = true;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isColliding = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Finish")
        {
            if (isColliding) return;
            isColliding = true;
            elapsedLaps++;
        }
        else if (other.tag == "FinishCheck")
        {
            backwardWallActivated = false;
        }
    }
    //public void OnCollisionExit(Collision collision) {
    //    if (collision.gameObject.CompareTag("Finish")) { 
    //        elapsedLaps++;
    //    }
    //}
    public void resetLaps()
    {
        elapsedLaps = 0;
    }
    public int getElapsedLaps() {
        return elapsedLaps;
    }
}
