using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Player"))
        {
            Debug.Log("enter");
        }
    }
}
