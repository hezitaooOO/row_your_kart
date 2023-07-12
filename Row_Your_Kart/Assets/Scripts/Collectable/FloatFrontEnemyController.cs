using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FloatFrontEnemyController : MonoBehaviour
{
    System.Random random = new System.Random();
    public Enemies enemies;


    //floating variables
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    //private int floatingDuration = 3;
   



    private GameObject floatedEnemy;
    // Position Storage Variables
    Vector3 posOffset = new Vector3();
    Vector3 tempPos = new Vector3();

    //variable to control floating
    Boolean isFloating = false;

    // Start is called before the first frame update
    void Start()
    {
        // Store the starting position & rotation of the object
        posOffset = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFloating) {

            // Spin object around Y-Axis
            transform.Rotate(new Vector3(0f, Time.deltaTime * degreesPerSecond, 0f), Space.World);

            // Float up/down with a Sin()
            tempPos = posOffset;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;

            transform.position = tempPos;
            
        }
    }


    void OnTriggerEnter(Collider hittingCar)
    {
        if (hittingCar.attachedRigidbody != null && hittingCar.CompareTag("Player"))
        {

            //Debug.Log("Hiting Car found");
            int randomIndex = random.Next(0, enemies.enemies.Length);

            floatedEnemy = enemies.enemies[randomIndex];
            KartFloater floater = floatedEnemy.GetComponent<KartFloater>();
            floater.isFloating = true;
            floater.setFloatingStartTime();

           

            Destroy(this.gameObject);
        }

    }

}
