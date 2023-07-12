using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerAIController : MonoBehaviour
{
    public Circuit circuit;
    private KartController kartController;

    [Header("Movement Input")]
    public float steer;
    public float accel;
    public float brake;

    [Header("Items")]
    public ItemUIController itemUIController;
    public GameObject items;

    private float steeringSensitivity = 0.01f;
    private float brakeSensitivity = 1.1f;
    private float accelSensitivity = 0.4f;

    public GameObject resetPoint { get; set; }
    [Header("Next decision")]
    public Vector3 nextTrackerTarget;
    public float totalDistanceToTarget;
    public float nextTargetAngle;

    public GameObject tracker;
    public int currentTrackerWaypoint = 0;
    public int currentResetWaypoint = 0;

    [Header("Tracking")]
    public Vector3 nextLocalTarget;
    public float distanceToTaget;
    public float distanceFactor;
    public float speedFactor;
    public float currentSpeed;

    private int invincibleStartWaypoint;

    private float lookahead = 20f;


    private Rigidbody rb;

    public enum AIState
    {
        Idle,
        Run,
        Invincible,
    };

    public AIState state;
    public float IdleSteer = 0;
    private float lastSteerOffset = 0;

    void Start()
    {
        kartController = GetComponent<KartController>();
        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);

        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = kartController.Rigidbody.gameObject.transform.position;
        tracker.transform.rotation = kartController.Rigidbody.gameObject.transform.rotation;
        tracker.GetComponent<MeshRenderer>().enabled = false;

        nextTrackerTarget = circuit.waypoints[(currentTrackerWaypoint + 1) % circuit.waypoints.Length].transform.position;
        totalDistanceToTarget = Vector3.Distance(tracker.transform.position, kartController.Rigidbody.gameObject.transform.position);

        resetPoint = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(resetPoint.GetComponent<Collider>());
        resetPoint.GetComponent<MeshRenderer>().enabled = false;

        resetPoint.transform.position = kartController.Rigidbody.gameObject.transform.position;
        resetPoint.transform.rotation = kartController.Rigidbody.gameObject.transform.rotation;

        rb = GetComponent<Rigidbody>();

        state = AIState.Idle;
    }


    public void UpdatesResetPoint(float threshold)
    {
        resetPoint.transform.position = circuit.waypoints[(currentResetWaypoint -1 + circuit.waypoints.Length) % circuit.waypoints.Length].transform.position;
        resetPoint.transform.LookAt(circuit.waypoints[currentResetWaypoint].transform.position);

        if (Vector3.Distance(kartController.Rigidbody.gameObject.transform.position, circuit.waypoints[currentResetWaypoint].transform.position) < threshold)
        {
            currentResetWaypoint = (currentResetWaypoint + 1) % circuit.waypoints.Length;
        }
    }

    void ProgressTracker()
    {
        if (Vector3.Distance(kartController.Rigidbody.gameObject.transform.position, tracker.transform.position) > lookahead)
        {
            return;
        }

        tracker.transform.LookAt(circuit.waypoints[currentTrackerWaypoint].transform.position);
        tracker.transform.Translate(0, 0, 1f);
        if (Vector3.Distance(tracker.transform.position, circuit.waypoints[currentTrackerWaypoint].transform.position) < 1)
        {
            currentTrackerWaypoint = (currentTrackerWaypoint + 1) % circuit.waypoints.Length;
            nextTrackerTarget = circuit.waypoints[(currentTrackerWaypoint + 1) % circuit.waypoints.Length].transform.position;
            totalDistanceToTarget = Vector3.Distance(tracker.transform.position, kartController.Rigidbody.gameObject.transform.position);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
        switch (state)
        {
            case AIState.Idle:
                Random.InitState(System.DateTime.Now.Millisecond);

                float randomSteer = Random.Range(0, 0.05f);
                
                if (lastSteerOffset == 0)
                {
                    if(Random.Range(0, 2) == 0)
                    {
                        lastSteerOffset = -0.02f;
                    }
                    else
                    {
                        lastSteerOffset = 0.02f;
                    }
                }

                if (IdleSteer > 0.8f && lastSteerOffset >= 0)
                {
                    IdleSteer -= randomSteer;
                    lastSteerOffset = -randomSteer;
                }
                else if (IdleSteer < -0.8f && lastSteerOffset <= 0)
                {
                    IdleSteer += randomSteer;
                    lastSteerOffset = randomSteer;
                }
                else if (lastSteerOffset > 0)
                {
                    IdleSteer += randomSteer;
                }
                else
                {
                    IdleSteer -= randomSteer;
                }

                kartController.Move(0, IdleSteer, 0);
                break;
            case AIState.Run:
                ProgressTracker();
                UpdatesResetPoint(8);
                Vector3 localTarget;
                nextLocalTarget = kartController.Rigidbody.gameObject.transform.InverseTransformPoint(nextTrackerTarget);

                // Avoid other karts
                if (Time.time < kartController.Rigidbody.GetComponent<AvoidOtherKart>().avoidTime)
                {
                    Vector3 randomOffset = new Vector3(Random.Range(-0.5f, 0.5f), 0, Random.Range(-0.5f, 0.5f));
                    localTarget = tracker.transform.right * kartController.Rigidbody.GetComponent<AvoidOtherKart>().avoidPath + randomOffset;
                }
                else
                {
                    localTarget = kartController.Rigidbody.gameObject.transform.InverseTransformPoint(tracker.transform.position);
                }
                float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;


                nextTargetAngle = Mathf.Atan2(nextLocalTarget.x, nextLocalTarget.z) * Mathf.Rad2Deg;

                distanceToTaget = Vector3.Distance(nextTrackerTarget, kartController.Rigidbody.gameObject.transform.position);
                distanceFactor = distanceToTaget / totalDistanceToTarget;
                speedFactor = kartController.currentSpeed / kartController.maxSpeed;


                steer = Mathf.Clamp(targetAngle * steeringSensitivity, -1, 1) * Mathf.Sign(kartController.currentSpeed);
                accel = Mathf.Lerp(accelSensitivity, 1, distanceFactor);
                brake = Mathf.Lerp((-1 - Mathf.Abs(nextTargetAngle)) * brakeSensitivity, 1 + speedFactor, 1 - distanceFactor);

                if (accel > 0.8f && Mathf.Abs(nextTargetAngle) > 20)
                {
                    brake += 0.8f;
                    accel -= 0.8f;
                }

                if (kartController.currentSpeed < 0.1f && accel < 0.2f)
                {
                    accel = 2f;
                }

                kartController.Move(accel, steer, brake);
                kartController.CheckScreeching(null);
                kartController.CalculateEngineSound();
                break;
            case AIState.Invincible:
                ProgressTracker();
                UpdatesResetPoint(8);
                MoveInvincibly();
                if (currentResetWaypoint - invincibleStartWaypoint >= 3)
                {
                    disableInInvincible();
                }

                break;
        }
    }

    void MoveInvincibly()
    {
        transform.LookAt(circuit.waypoints[(currentResetWaypoint + 1) % circuit.waypoints.Length].transform.position);
        transform.Translate(0, 0, 0.8f);
    }

    public void enableInInvincible()
    {
        rb.mass = 100000;
        state = AIState.Invincible;
        invincibleStartWaypoint = currentResetWaypoint;
    }

    public void disableInInvincible()
    {
        rb.mass = 500;
        state = AIState.Run;
        kartController.Move(1, 0, 0);
    }

}
