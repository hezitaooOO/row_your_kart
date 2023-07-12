using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartController : MonoBehaviour
{

    public Rigidbody Rigidbody;
    public float gearLength = 3f;
    public float currentSpeed;
    public float slipAllowanceSpeed = 45f;
    public float slipAllowance = 0.5f;
    public bool sliping;


    [Header("Vehicle movement")]
    public float maxTorque = 200;
    public float maxSteerAngle = 20;
    public float maxBrakeTorque = 4000;
    public float maxSpeed = 200;
    

    [Header("Movement Inputs")]
    public float accel;
    public float steer;
    public float brake;

    // 0 FrontLeft, 1 FrontRight, 2 RearLeft, 3 RearRight
    public WheelCollider[] wheelColliders;
    public GameObject[] wheels;
    
    [Header("Engine sound")]
    public float lowPitch = 1f;
    public float highPitch = 6f;
    public int numGears = 5;
    public AudioSource engineSound;

    private float rpm;
    private int currentGear = 1;
    private float currentGearPerc;

    [Header("Screeching sound")]
    public AudioSource screechingSound;

    [Header("Trail effect")]
    public Transform trailPrefab;
    Transform[] trails = new Transform[4];
    public ParticleSystem smokePrefab;
    ParticleSystem[] smoke = new ParticleSystem[4];

    [Header("Animation")]
    public Animator playerAnimationController;

    private float lastTimeChecked;
    private float lastGroundedPercent;
    public int screechingCount { get; set; }

    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            smoke[i] = Instantiate(smokePrefab);
            smoke[i].Stop();
        }

        if (this.gameObject.CompareTag("Player"))
        {
            maxSpeed = KartConfig.maxSpeed;
            maxSteerAngle = KartConfig.steer;
            maxTorque = KartConfig.acceleration;
            Rigidbody.mass = KartConfig.weight;
            for (int i = 0; i < 4; i++)
            {

                /*WheelFrictionCurve fFriction = wheelColliders[i].forwardFriction;
                fFriction.stiffness = KartConfig.friction;
                wheelColliders[i].forwardFriction = fFriction;*/

                WheelFrictionCurve sFriction = wheelColliders[i].sidewaysFriction;
                sFriction.stiffness = KartConfig.friction;
                wheelColliders[i].sidewaysFriction = sFriction;
            }

            Debug.LogFormat("maxSpeed: {0} maxSteerAngle: {1} maxTorque: {2} mass: {3} friction: {4}", maxSpeed, maxSteerAngle, maxTorque, Rigidbody.mass, KartConfig.friction);
        }

    }

     public void Move(float accel, float steer, float brake)
    {
        currentSpeed = Rigidbody.velocity.magnitude* gearLength;
        this.accel = accel;
        this.steer = steer;
        this.brake = brake;
        applyMotor();
        applySteering();
        applyBrake();
        updateWheelPositions();
    }

    void applyMotor()
    {
        float torque = 0;
        if (currentSpeed < maxSpeed)
        {
            torque = Mathf.Clamp(accel, -1, 1) * maxTorque;
        }
        // trying rear wheel drive
        for(int i =0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = torque;
        }
    }
    void applyBrake()
    {
        float brakeTorque = Mathf.Clamp(brake, 0, 1) * maxBrakeTorque;
        for(int i = 0; i < 4; i++)
        {
            wheelColliders[i].brakeTorque = brakeTorque;
        }
    }
    void applySteering()
    {
        // only front wheels shall rotate
        float streerAngle = Mathf.Clamp(steer, -2, 2) * 0.5f * maxSteerAngle;
        for (int i = 0; i < 2; i++)
        {
            wheelColliders[i].steerAngle = streerAngle;
        }
        // Animation parameters
        playerAnimationController.SetFloat("Steering", Mathf.Clamp(steer, -1, 1));
    }

    void updateWheelPositions()
    {
        for (int i = 0; i < 4; i++)
        {
            Quaternion quat;
            Vector3 pos;
            wheelColliders[i].GetWorldPose(out pos, out quat);

            wheels[i].transform.position = pos;
            wheels[i].transform.rotation = quat;
        }
    }

    public void StartTrail(int i)
    {
        if (trails[i] == null)
        {
            trails[i] = Instantiate(trailPrefab);
        }
        trails[i].parent = wheelColliders[i].transform;
        trails[i].localRotation = Quaternion.Euler(90, 0, 0);
        trails[i].position = wheelColliders[i].transform.position - wheelColliders[i].transform.up * wheelColliders[i].radius;
    }

    public void EndTrail(int i)
    {
        if (trails[i] == null)
        {
            return;
        }
        Transform holder = trails[i];
        trails[i] = null;
        holder.parent = null;
        holder.rotation = Quaternion.Euler(90, 0, 0);
        Destroy(holder.gameObject, 30);
    }

    public void CalculateEngineSound()
    {
        float gearPercentage = (1 / (float) numGears);
        float targetGearFactor = Mathf.InverseLerp(gearPercentage * currentGear, gearPercentage * (currentGear + 1),
            Mathf.Abs(currentSpeed / maxSpeed));
        currentGearPerc = Mathf.Lerp(currentGearPerc, targetGearFactor, Time.deltaTime * 5f);
        var gearNumFactor = currentGear / (float) numGears;
        rpm = Mathf.Lerp(gearNumFactor, 1, currentGearPerc);

        float speedPercentage = Mathf.Abs(currentSpeed / maxSpeed);
        float upperGearMax = (1 / (float)numGears) * (currentGear + 1);
        float downGearMax = (1 / (float)numGears) * currentGear;
        if (currentGear > 0 && speedPercentage < downGearMax)
        {
            currentGear--;
        }

        if (speedPercentage > upperGearMax && (currentGear < (numGears - 1)))
        {
            currentGear++;
        }
        float pitch = Mathf.Lerp(lowPitch, highPitch, rpm);
        engineSound.pitch = Mathf.Min(highPitch, pitch) * 0.25f;

    }

    public void CheckScreeching(SpeedupBar speedupBar)
    {
        int count = 0;
        for (int i = 0; i < 4; i++)
        {
            WheelHit hit;
            wheelColliders[i].GetGroundHit(out hit);
            if (currentSpeed> slipAllowanceSpeed && Mathf.Abs(hit.forwardSlip) >= slipAllowance || Mathf.Abs(hit.sidewaysSlip) >= slipAllowance)
            {
                count++;
                if (!screechingSound.isPlaying)
                {
                    screechingSound.Play();
                }
                StartTrail(i);
                smoke[i].transform.position = wheelColliders[i].transform.position - wheelColliders[i].transform.up * wheelColliders[i].radius;
                smoke[i].Emit(10);
                if (speedupBar != null)
                {
                    speedupBar.driftingCharge();
                }
            }
            else
            {
                EndTrail(i);
            }
        }

        if (count == 0 && screechingSound.isPlaying)
        {
            screechingSound.Stop();
        }
        screechingCount = count;
    }

    public float GetGroundedPercent()
    {
        int groundedCount = 0;
        for (int i = 0; i < 4; i++)
        {
            if (wheelColliders[i].isGrounded && wheelColliders[i].GetGroundHit(out WheelHit hit))
            {
                groundedCount++;

            }
        }
        return groundedCount / 4.0f;
    }

    private void ResetFlipKart()
    {
        transform.position += Vector3.up;
        transform.rotation = Quaternion.LookRotation(transform.forward);
    }


    public void CheckFlipKart()
    {
        float currentGroundedPercent = GetGroundedPercent();
        if (transform.up.y > 0.5f || currentGroundedPercent != lastGroundedPercent || Rigidbody.velocity.magnitude > 1)
        {
            lastTimeChecked = Time.time;
        }
        if (Time.time > lastTimeChecked + 3)
        {
            ResetFlipKart();
        }
        lastGroundedPercent = currentGroundedPercent;
    }

    public void ApplySpeedUp()
    {
        float torqueMultiplier = 2f;
        if (currentSpeed < 40)
        {
            torqueMultiplier = 5f;
        } else if(currentSpeed < 60)
        {
            torqueMultiplier = 3f;
        }

        for (int i = 0; i < 4; i++)
        {
            wheelColliders[i].motorTorque = torqueMultiplier * maxTorque;
        }
    }
   
}
