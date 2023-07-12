//using System.Collections;
//using System.Collections.Generic;
//using Packages.Rider.Editor.UnitTesting;
using UnityEngine;
using UnityEngine.UI;

public class LapsUIController : MonoBehaviour
{
    private GameObject player;
    public Text lapsText;
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0]; ;
    }

    // Update is called once per frame
    void Update()
    {
        int maxLaps = LapsConfig.laps;
        int curLaps = player.transform.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps();
        string text = "Lap: " +  curLaps + "/" + maxLaps;
        lapsText.text = text;
    }
}
