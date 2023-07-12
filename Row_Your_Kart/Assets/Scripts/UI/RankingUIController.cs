using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using static System.Net.Mime.MediaTypeNames;

public class RankingUIController : MonoBehaviour
{
    GameObject[] enemies;
    GameObject player;
    public Text rankingText;
    public int rank;
    public int totalKartCount;


    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        rank = 1;
        var playerController = player.GetComponent<PlayerController>();
        int playerLaps = player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps();
        for (int i = 0; i < enemies.Length; i++)
        {
            var enemyController = enemies[i].GetComponent<TrackerAIController>();
            int enemyLaps = enemies[i].transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps();
            if (playerLaps > enemyLaps || (playerLaps == enemyLaps && enemyController.currentResetWaypoint >= playerController.currentResetWaypoint))
            {
                rank++;
            }
        }

        string playerRank = getRankString(rank);
        totalKartCount = GameObject.FindGameObjectsWithTag("Enemy").Length + 1;
        string text = "Rank: " + playerRank + "/" + totalKartCount;
        rankingText.text = text;
    }

    public string getRankString(int rank) {
        string text = "1st";
        switch (rank)
        {
            case 1:
                text = "1st";
                break;
            case 2:
                text = "2nd";
                break;
            case 3:
                text = "3rd";
                break;
            case 4:
                text = "4th";
                break;
            case 5:
                text = "5th";
                break;
            case 6:
                text = "6th";
                break;
            case 7:
                text = "7th";
                break;
            case 8:
                text = "8th";
                break;
            default:
                text = "1th";
                break;
        }
        return text;
    }
}
