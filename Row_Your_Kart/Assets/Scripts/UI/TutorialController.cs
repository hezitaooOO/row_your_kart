using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialController : MonoBehaviour
{
    public GameObject[] tutorialPanels;
    int curPanel;
    //public GameObject startGameCanvas;
    void Start()
    {
        curPanel = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void startTutorial()
    {
        tutorialPanels[curPanel].SetActive(true);

    }
    public void moveToNextTutorial()
    {
        tutorialPanels[curPanel].SetActive(false);
        curPanel++;
        tutorialPanels[curPanel].SetActive(true);

    }

    public void moveToPreviousTutorial()
    {
        tutorialPanels[curPanel].SetActive(false);
        curPanel--;
        tutorialPanels[curPanel].SetActive(true);

    }

    public void closeTutorial()
    {
        tutorialPanels[curPanel].SetActive(false);
        curPanel = 0;

    }
}
