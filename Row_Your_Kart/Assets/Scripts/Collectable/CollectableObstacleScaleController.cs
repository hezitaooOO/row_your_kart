using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableObstacleScaleController : MonoBehaviour
{
    private GameObject[] obstacles;

    void Start()
    {
        if (obstacles == null)
        {
            obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        }



    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator scaleUpObstacles()
    {
        //Print the time of when the function is first called.
        Debug.Log("Started Coroutine at timestamp : " + Time.time);
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.localScale += new Vector3(1, 1, 1);
        }
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(3);

        //After we have waited 5 seconds print the time again.
        //Debug.Log("Finished Coroutine at timestamp : " + Time.time);
        //foreach (GameObject obstacle in obstacles)
        //{
        //    obstacle.transform.localScale += new Vector3(2, 2, 2);
        //}
    }

    void OnTriggerEnter(Collider hittingCar)
    {
        if (hittingCar.attachedRigidbody != null && hittingCar.CompareTag("Player"))
        {
            StartCoroutine(scaleUpObstacles());
            Destroy(this.gameObject);
        }

    }
}