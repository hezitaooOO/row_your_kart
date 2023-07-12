using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
//using UnityEngine.Vector3;


public class AIchasing : MonoBehaviour
{
    //public int currWaypoint = -1;
    public Transform[] waypoints;
    private  NavMeshAgent UAN;
   // private Animator MAC;

    //private  NavMeshAgent CUBE;
    public Transform[] waypointsmoving;
    public float Dist;
    public Vector3 FutureTarget;
    public float lookAheadT;
    //public VelocityReporter Cubemovingaim;
    public Vector3 aa;
    public GameObject bb;
    public GameObject hh;


    //public float  aaa;

    // Start is called before the first frame update
    void Start()
    {
        

        UAN = GetComponent <NavMeshAgent>();
       // MAC = GetComponent <Animator>();
        //Cubemovingaim = Cubemoving.GetComponent <VelocityReporter>();
        GameObject bb = GameObject.Find("cube4 (1)");
       // GameObject hh = GameObject.Find("cube4 (1)");

       // aiState = AIState.staticcc;
        //setNextWaypoint();
        // hh.SetActive (modeactive.cc);
         hh.SetActive(modeactive.cc) ;


    }

    // Update is called once per frame
    void Update()
    {        
        movingball();
        if (Dist <=1)
        {  // currWaypoint =0;
        movingball();}
    }
    
    void movingball()
    {     
                aa = bb.GetComponent<VelocityReporter>().velocity;
                Dist =  (waypointsmoving[0].transform.position -UAN.transform.position).sqrMagnitude;
                lookAheadT = Dist/UAN.speed;
                FutureTarget = (waypointsmoving[0].transform.position)+(lookAheadT * aa);
                UAN.SetDestination (FutureTarget);
                //currWaypoint++;
    }
    
}