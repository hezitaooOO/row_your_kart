using UnityEngine;
using System.Collections;
using System;

//Reference: http://www.donovankeith.com/2016/05/making-objects-float-up-down-in-unity/
// Makes objects float up & down while gently spinning.
public class KartFloater : MonoBehaviour
{
    // User Inputs
    public float degreesPerSecond = 5.0f;
    public float amplitude = 0.3f;
    public float frequency = 1f;
    private double floatingDuration = 3;

    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    //floating controlling variable
    public Boolean isFloating = false;
    private Boolean floatingTrigger = true;
    System.DateTime floatingStartTime;

    // Use this for initialization
    void Start()
    {
        // Store the starting position & rotation of the object
        //posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloating)
        {
            // Spin object around Y-Axis
            //transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            // Float up/down with a Sin()
            if (floatingTrigger)
            {
                posOffset = transform.position;
                floatingTrigger = false;
            }
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude + 1f;

            transform.position = tempPos;

            System.DateTime currentTime = System.DateTime.Now;
            double floatingElapsedTime = (currentTime - floatingStartTime).TotalSeconds;
            //Debug.Log("Floating elapsed time is: " + floatingElapsedTime);
            if (floatingElapsedTime >= floatingDuration)
            {
          
                isFloating = false;
            }


        }
        else {
            floatingTrigger = true;
        }
    }

    public void setFloatingStartTime() {
        floatingStartTime = System.DateTime.Now;
    }
}