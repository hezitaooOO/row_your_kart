using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    int historyHighScore;
    int score;

    public Text scoreText;
    //public GameObject panel;
    public RankingUIController rankingController;

    //public Text playScoreText;
    //public Text rankScoreText;

    //public Text historyHighScoreText;
   // public Text finalScoreText;

    //public Text highScoreText;


    // Start is called before the first frame update
    void Start()
    {
        // Load history high score from local file

        historyHighScore = SaveLoad.LoadHighScore();
        score = 0;
        scoreText.text = "Score: 0";
        //panel.SetActive(false);
        //highScoreText.enabled = false;

    }


    public void UpdateCurrentScore(int score)
    {
        this.score += score;
        scoreText.text = "Score: " + this.score.ToString();
    }

    public void ResolveScore()
    {
        int rankingScore = 0;
        switch (rankingController.rank) 
        {
            case 1:
                rankingScore = 2000;
                break;
            case 2:
                rankingScore = 1000;
                break;
            case 3:
                rankingScore = 500;
                break;
            case 4:
                rankingScore = 0;
                break;
            default:
                rankingScore = 0;
                break;
        }


        string rankingString = "Rank score: " + rankingScore.ToString();
        GameResultsManager.rankScore = rankingScore;
        string playingString = "Play score: " + score.ToString();
        GameResultsManager.playScore = score;
        string finalString = "Final score: " + (score + rankingScore).ToString();
        GameResultsManager.finalScore = (score + rankingScore);
        GameResultsManager.historyHighScore = historyHighScore;

        if (score + rankingScore > historyHighScore)
        {
            //highScoreText.enabled = true;
            historyHighScore = score + rankingScore;
            SaveLoad.SaveHighScore(historyHighScore);
        }
        string highString = "High score: " + historyHighScore.ToString();

        //playScoreText.text = playingString;
        //rankScoreText.text = rankingString;
        //finalScoreText.text = finalString;
        //historyHighScoreText.text = highString;


        //panel.SetActive(true);

    }


    // Update is called once per frame
    void Update()
    {
       if( Input.GetKeyDown(KeyCode.V))
        {
            ResolveScore();
        }
    }
}
