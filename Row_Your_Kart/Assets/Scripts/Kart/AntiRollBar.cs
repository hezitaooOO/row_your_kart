using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiRollBar : MonoBehaviour
{
    public float antiRoll = 5000.0f;
    public WheelCollider wheelLeftFront;
    public WheelCollider wheelRightFront;
    public WheelCollider wheelLeftBack;
    public WheelCollider wheelRightBack;
    public Transform CenterOfMass;
    Rigidbody rb;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = transform.InverseTransformPoint(CenterOfMass.position);
    }


    void GroundWheels(WheelCollider wl, WheelCollider wr)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool groundedL = wl.GetGroundHit(out hit);
        if (groundedL)
        {
            travelL = (-wl.transform.InverseTransformPoint(hit.point).y - wl.radius) / wl.suspensionDistance;
        }

        bool groundedR = wr.GetGroundHit(out hit);

        if (groundedR)
        {
            travelR = (-wr.transform.InverseTransformPoint(hit.point).y - wr.radius) / wr.suspensionDistance;
        }

        float antiRollForce = (travelL - travelR) * antiRoll;
        if (groundedL)
        {
            rb.AddForceAtPosition(wl.transform.up * -antiRollForce, wl.transform.position);
        }
        if (groundedR)
        {
            rb.AddForceAtPosition(wr.transform.up * antiRollForce, wr.transform.position);
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.centerOfMass = transform.InverseTransformPoint(CenterOfMass.position);
        GroundWheels(wheelLeftFront, wheelRightFront);
        GroundWheels(wheelLeftBack, wheelRightBack);
    }
}
