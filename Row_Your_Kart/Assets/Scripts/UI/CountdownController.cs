using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public int countdownTime;
    public Text countdownDisplay;

    public GameObject[] AIKarts;
    public GameObject player;
    public GameObject timer;
    public GameObject ResetUI;


    public AudioSource TTO;
    public AudioSource GO;

    private void Start()
    {

        for (int i = 0; i < AIKarts.Length; i++)
        {
            AIKarts[i].GetComponent<TrackerAIController>().state = TrackerAIController.AIState.Idle;
        }

        player.GetComponent<PlayerController>().SetInCountDown(true);
        timer.GetComponent<TimerController>().enabled = false;
        ResetUI.GetComponent<ResetUI>().enabled = false;


        StartCoroutine(CountdownToStart());
    }

    IEnumerator CountdownToStart()
    {
        while (countdownTime > 0)
        {
            if (countdownTime <= 3)
            {
                TTO.Play();
                countdownDisplay.text = countdownTime.ToString();
            }
            yield return new WaitForSeconds(1f);
            countdownTime--;
        }
        GO.Play();

        countdownDisplay.text = "GO!";
        /* Call the code to "begin" your game here.
		 * For example, mine allows the player to start
		 * moving and starts the in game timer.
         */
        // GameController.instance.BeginGame();

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < AIKarts.Length; i++)
        {
            AIKarts[i].GetComponent<TrackerAIController>().state = TrackerAIController.AIState.Run;
        }
        player.GetComponent<PlayerController>().SetInCountDown(false);
        timer.GetComponent<TimerController>().enabled = true;
        ResetUI.GetComponent<ResetUI>().enabled = true;


        countdownDisplay.gameObject.SetActive(false);
    }
}
