using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FinishLineWinController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject youWinCanvas;
    //public GameObject youLostCanvas;
    public GameObject inGameCanvas;
    public Image winImage;
    public Image loseImage;
    public AudioSource winAudio;
    public AudioSource lostAudio;
    public AudioSource lastLapAudio;
    public TextMeshProUGUI lastLapText;

    public ScoreManager scoreManager;

    private GameObject player;
    private GameObject[] enemies;
    private bool lastLapSoundPlayed = false;
    
    public void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        
    }

    public void Update()
    {
        //Debug.Log("Player laps : " + player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps() + ". Total laps: " + LapsConfig.laps);
        if (lastLapSoundPlayed == false && player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps() == LapsConfig.laps - 1) {
            lastLapAudio.Play();
            lastLapSoundPlayed = true;
            if (LapsConfig.laps > 1)
            {
                StartCoroutine(showLastLapMessage());
            }
        }
        if (player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps() >= LapsConfig.laps)
        {
            //Debug.Log("should hit finish line");
            //youWinCanvas.SetActive(true);
            //loseImage.enabled = false;
            //winImage.enabled = true;
            //inGameCanvas.SetActive(false);
            //winAudio.Play();
            
            //player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().resetLaps();
            //Time.timeScale = 0f;
            
            //Debug.Log("Player laps : " + player.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps() + ". Total laps: " + LapsConfig.laps);
            scoreManager.ResolveScore();
            GameResultsManager.playerWon = true;
            SceneManager.LoadScene("Ending");

        }
        foreach (GameObject enemy in enemies)
        {
            if (enemy.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().getElapsedLaps() >= LapsConfig.laps)
            {
                //youLostCanvas.SetActive(true);
                //youWinCanvas.SetActive(true);
                //loseImage.enabled = true;
                //winImage.enabled = false;
                //inGameCanvas.SetActive(false);
                //lostAudio.Play();
                foreach (GameObject e in enemies) { 
                    e.transform.Find("TriggeredCollider").GetComponent<LapsCounter>().resetLaps();
                }
                //Time.timeScale = 0f;
                GameResultsManager.playerWon = false;
                scoreManager.ResolveScore();
                SceneManager.LoadScene("Ending");
            }
        }
    }
    IEnumerator showLastLapMessage()
    {
        lastLapText.text = "Last Lap!";
        yield return new WaitForSeconds(0.5f);
        lastLapText.text = "";
        yield return new WaitForSeconds(0.5f);
        lastLapText.text = "Last Lap!";
        yield return new WaitForSeconds(0.5f);
        lastLapText.text = "";
        yield return new WaitForSeconds(0.5f);
        lastLapText.text = "Last Lap!";
        yield return new WaitForSeconds(0.5f);
        lastLapText.text = "";
        yield return new WaitForSeconds(0.5f);
    }
}
