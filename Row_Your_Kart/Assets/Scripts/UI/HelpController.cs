using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpController : MonoBehaviour
{
    public GameObject helpPanel;
    //public GameObject startGameCanvas;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void popupHelp() {
        helpPanel.SetActive(true);
        
    }

    public void hideHelp() {
        helpPanel.SetActive(false);
        
    }
}
