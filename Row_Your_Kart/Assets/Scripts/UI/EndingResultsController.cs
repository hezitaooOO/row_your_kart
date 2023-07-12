using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;

public class EndingResultsController : MonoBehaviour
{
    public Camera cam;
    public GameObject camTarget;
    public GameObject scoreBoardPanel;
    //public GameObject itemUI;

    public Text speedUpCountText;
    public Text floatingCountText;
    public Text barrierCountText;
    public Text tiresCountText;
    public Text rocketCountText;

    public Text playScoreText;
    public Text rankScoreText;
    public Text historyHighScoreText;
    public Text finalScoreText;
    public Text highScoreText;

    public GameObject winImage;
    public GameObject lostImage;

    public GameObject[] kartsToDisplayWhenWin;
    public GameObject[] kartsToDisplayWhenLose;

    private int playScore;
    private int rankScore;
    private int historyHighScore;
    private int finalScore;
    private int highScore;
    private int[] itemUsed;


    // Start is called before the first frame update
    void Start()
    {
        itemUsed = GameResultsManager.itemUsed;
        playScore = GameResultsManager.playScore;
        rankScore = GameResultsManager.rankScore;
        historyHighScore = GameResultsManager.historyHighScore;
        finalScore = GameResultsManager.finalScore;
        highScore = GameResultsManager.highScore;

        string rankingString = "Rank score: " + rankScore.ToString();
        string playingString = "Play score: " + playScore.ToString();
        string finalString = "Final score: " + finalScore.ToString();
        if (finalScore > historyHighScore)
        {
            highScoreText.enabled = true;
            historyHighScore = finalScore;
        }
        string highString = "High score: " + historyHighScore.ToString();

        playScoreText.text = playingString;
        rankScoreText.text = rankingString;
        finalScoreText.text = finalString;
        historyHighScoreText.text = highString;

        speedUpCountText.text = itemUsed[0].ToString();
        floatingCountText.text = itemUsed[1].ToString();
        barrierCountText.text = itemUsed[2].ToString();
        tiresCountText.text = itemUsed[3].ToString();
        rocketCountText.text = itemUsed[4].ToString();

        winImage.SetActive(GameResultsManager.playerWon);
        lostImage.SetActive(!GameResultsManager.playerWon);

        foreach (GameObject kart in kartsToDisplayWhenWin) {
            kart.SetActive(GameResultsManager.playerWon);
        }
        foreach (GameObject kart in kartsToDisplayWhenLose)
        {
            kart.SetActive(!GameResultsManager.playerWon);
        }
    }
    void Update()
    {
        if (cam.transform.position.Equals(camTarget.transform.position)) { 
            scoreBoardPanel.SetActive(true);
        }
    }
}
