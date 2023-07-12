using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{
    private KartController kartController;
    private bool inCountDown;
    public bool inSpeedUp { get; set; }
    private float originalMass;
    public bool inInvincible { get; set; }

    public GameObject resetPoint { get; set; }
    public GameObject kartCollider;
    [HideInInspector] public int currentResetWaypoint = 0;
    private float lastHoldTime;
    public Circuit circuit;

    [Header("Items")]
    public ItemUIController itemUIController;
    public GameObject items;

    [Header("Speedup bar")]
    public Image speedupBarImage;
    [HideInInspector] public SpeedupBar speedupBar;

    public GameObject visualKart;
    public GameObject visualPlayer;
    public GameObject visualRocket;
    public GameObject[] visualWheels;

    public ScoreManager scoreManager;

    private Boolean isOnRoad;
    private Boolean isOnTerrain;
    private float lastOfftrackTime;
    private Boolean offtrackTriggered;

    void Start()
    {
        kartController = GetComponent<KartController>();
        speedupBar = speedupBarImage.GetComponent<SpeedupBar>();

        resetPoint = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(resetPoint.GetComponent<Collider>());
        resetPoint.GetComponent<MeshRenderer>().enabled = false;
        resetPoint.transform.position = kartController.Rigidbody.gameObject.transform.position;
        resetPoint.transform.rotation = kartController.Rigidbody.gameObject.transform.rotation;
    }

    void Update()
    {
        float accel = Input.GetAxis("Vertical");
        float steer = Input.GetAxis("Horizontal");
        float brake = Input.GetAxis("Jump");

        if (inCountDown)
        {
            accel = 0;
            kartController.Move(accel, steer, brake);
            return;
        }
        CheckReset();
        UpdateItem();
        
        UpdatesResetPoint(20);
        
        MovePlayer(accel, steer, brake);
        kartController.Move(accel, steer, brake);
        applyBooster();

        kartController.CheckScreeching(speedupBar);
        kartController.CalculateEngineSound();

        if (!isOnRoad && isOnTerrain)
        {
            if (offtrackTriggered == false)
            {
                lastOfftrackTime = Time.time;
                offtrackTriggered = true;
            }
            if (offtrackTriggered == true && Time.time - lastOfftrackTime > 1f)
            {
                OffTrackReset();
                offtrackTriggered = false;
            }
        }
        else
        {
            offtrackTriggered = false;
        }
    }

    public void SetInCountDown(bool inCountDown)
    {
        this.inCountDown = inCountDown;
    }

    public bool ApplyNitron()
    {
        if (speedupBar.getSpeedupCharge() > 0.0f && kartController.currentSpeed < kartController.maxSpeed)
        {
            speedupBar.consumeSpeedup();
            return true;
        }
        return false;
    }

    void MovePlayer(float accel, float steer, float brake)
    {
        Vector3 localVelocity = transform.InverseTransformVector(kartController.Rigidbody.velocity);
        bool accelDirectionIsFwd = accel - brake > 0;
        bool localVelDirectionIsFwd = localVelocity.z >= 0;

        // Rotate in place
        if (kartController.GetGroundedPercent() > 0.0f && kartController.screechingCount == 0)
        {

            float angularVelocitySteering = 0.4f;
            float angularVelocitySmoothSpeed = 40f;

            if (!localVelDirectionIsFwd && !accelDirectionIsFwd)
                angularVelocitySteering *= -1.0f;

            var angularVel = kartController.Rigidbody.angularVelocity;
            angularVel.y = Mathf.MoveTowards(angularVel.y, 2f*steer * angularVelocitySteering, Time.fixedDeltaTime * angularVelocitySmoothSpeed);

            kartController.Rigidbody.angularVelocity = angularVel;

        }
    }



    void UpdatesResetPoint(float threshold)
    {

        resetPoint.transform.position = circuit.waypoints[(currentResetWaypoint - 1 + circuit.waypoints.Length) % circuit.waypoints.Length].transform.position;
        resetPoint.transform.LookAt(circuit.waypoints[currentResetWaypoint].transform.position);

        if (Vector3.Distance(kartController.Rigidbody.gameObject.transform.position, circuit.waypoints[(currentResetWaypoint + 1) % circuit.waypoints.Length].transform.position) < threshold)
        {
            currentResetWaypoint = (currentResetWaypoint + 1) % circuit.waypoints.Length;
        }
        
    }

    void CheckReset()
    {
        if (!Input.GetKey(KeyCode.R))
        {
            lastHoldTime = Time.time;
            return;
        }

        if (Time.time > lastHoldTime + 3) {
            transform.position = resetPoint.transform.position + Vector3.up;
            transform.rotation = resetPoint.transform.rotation;
            kartController.Rigidbody.velocity = Vector3.zero;
            kartCollider.gameObject.layer = 6;
            lastHoldTime = Time.time;
            Invoke("ResetLayer", 3);
        }
    }


    public void enterInvinvincible()
    {
        inInvincible = true;
        originalMass = kartController.Rigidbody.mass;
        kartController.Rigidbody.mass = 100000;
        kartController.slipAllowanceSpeed = 5000f;
        visualKart.SetActive(false);
        visualPlayer.SetActive(false);
        visualRocket.SetActive(true);
        for (int i = 0; i < visualWheels.Length; i++)
        {
            visualWheels[i].SetActive(false);
        }

    }
    public void leaveInvinvincible()
    {
        inInvincible = false;
        kartController.Rigidbody.mass = originalMass;
        kartController.slipAllowanceSpeed = 45f;
        visualKart.SetActive(true);
        visualPlayer.SetActive(true);
        visualRocket.SetActive(false);
        for (int i = 0; i < visualWheels.Length; i++)
        {
            visualWheels[i].SetActive(true);
        }
        kartController.Move(1, 0, 0);
    }


    public void AddItem(ItemInterface item)
    {
        int success = itemUIController.AddItem(item);

        scoreManager.UpdateCurrentScore(success * 100);
    }

    void UpdateItem()
    {
        // rocket item effect
        if (inInvincible)
        {
            UpdatesResetPoint(12);
            MoveInvincibly();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {

            int success = itemUIController.UseItem();

            scoreManager.UpdateCurrentScore(success * 100);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            itemUIController.SwitchItems();
        }
    }

    void MoveInvincibly()
    {
        transform.LookAt(circuit.waypoints[(currentResetWaypoint + 1) % circuit.waypoints.Length].transform.position);
        transform.Translate(0, 0, 0.8f);
    }

    void applyBooster()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (ApplyNitron())
            {
                kartController.ApplySpeedUp();
            }
        }
        if (inSpeedUp)
        {
            kartController.ApplySpeedUp();
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            isOnTerrain = true;
        }
        else if (other.CompareTag("Road"))
        {
            isOnRoad = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Terrain"))
        {
            isOnTerrain = false;
        }
        else if (other.CompareTag("Road"))
        {
            isOnRoad = false;
        }
    }
    public void OffTrackReset() {
    
        StartCoroutine("ForceReset");
        
    }
    IEnumerator ForceReset()
    {

        yield return new WaitForSeconds(0);
        transform.position = resetPoint.transform.position + Vector3.up;
        transform.rotation = resetPoint.transform.rotation;
        kartController.Rigidbody.velocity = Vector3.zero;
        kartCollider.gameObject.layer = 6;
        //lastHoldTime = Time.time;
        Invoke("ResetLayer", 3);
    }
    void ResetLayer()
    {
        kartCollider.gameObject.layer = 0; // Reset to default layer
    }
}
