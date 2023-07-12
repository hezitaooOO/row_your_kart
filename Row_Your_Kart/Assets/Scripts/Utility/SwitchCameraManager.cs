using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCameraManager : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject[] targets;
    private SmoothFollow smoothFollow;
    private int currentTarget = 0;

    // Start is called before the first frame update
    void Start()
    {
        smoothFollow = mainCamera.GetComponent<SmoothFollow>();
        if (targets.Length == 0)
        {
            Debug.LogError("No camera targets");
        }
        smoothFollow.SetTarget(targets[currentTarget].transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            currentTarget = (currentTarget + 1) % targets.Length;
            smoothFollow.SetTarget(targets[currentTarget].transform);
        }
    }
}
