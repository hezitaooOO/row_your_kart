using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetStuckAI : MonoBehaviour
{

    Rigidbody rb;
    float lastTimeChecked;
    private TrackerAIController aiController;
    private int lastReset;
    public GameObject kartCollider;

    void ResetKart()
    {
        rb.transform.position = aiController.resetPoint.transform.position + Vector3.up;
        rb.transform.rotation = aiController.resetPoint.transform.rotation;

        kartCollider.gameObject.layer = 6; // ReSpawn layer
        if (lastReset == aiController.currentResetWaypoint)
        {
            aiController.currentResetWaypoint += 1;
            aiController.currentTrackerWaypoint += 1;
            aiController.UpdatesResetPoint(8);
        }
        lastReset = aiController.currentResetWaypoint;
        Invoke("ResetLayer", 3); // delay reset layer
    }

    void ResetLayer()
    {
        kartCollider.gameObject.layer = 0; // Reset to default layer
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        aiController = GetComponent<TrackerAIController>();
    }

    void Update()
    {
        if (aiController.state == TrackerAIController.AIState.Run)
        {
            Vector3 localVelocity = transform.InverseTransformVector(rb.velocity);
            bool localVelocityForward = localVelocity.z >= 0;

            if (rb.velocity.magnitude > 1 && localVelocityForward)
            {
                lastTimeChecked = Time.time;
            }

            if (Time.time > lastTimeChecked + 3)
            {
                ResetKart();
                lastTimeChecked = Time.time;
            }
        }
        else
        {
            lastTimeChecked = Time.time;
        }

    }
}
