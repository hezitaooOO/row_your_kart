using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvoidOtherKart : MonoBehaviour
{

    public float avoidPath = 0;
    public float avoidTime = 0;
    public float avoidDistance = 4;
    public float avoidLength = 1;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Player"))
        {
            return;
        }
        avoidTime = 0;

    }

    void OnCollisionStay(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        Rigidbody otherKart = collision.rigidbody;

        avoidTime = Time.time + avoidLength;

        Vector3 otherKartLocalTarget = transform.InverseTransformPoint(otherKart.gameObject.transform.position);

        float otherKartAngle = Mathf.Atan2(otherKartLocalTarget.x, otherKartLocalTarget.z);
        avoidPath = avoidDistance * -Mathf.Sign(otherKartAngle);
    }

}
