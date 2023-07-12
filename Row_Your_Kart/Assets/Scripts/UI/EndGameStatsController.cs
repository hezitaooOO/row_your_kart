using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EndGameStatsController : MonoBehaviour
{
    public GameObject itemUI;
    public Text speedUpCountText;
    public Text floatingCountText;
    public Text barrierCountText;
    public Text tiresCountText;
    public Text rocketCountText;
    private int[] itemUsed;

    // Start is called before the first frame update
    void Start()
    {
        itemUsed = itemUI.GetComponent<ItemUIController>().getItemUsedStats();
    }

    // Update is called once per frame
    void Update()
    {
        speedUpCountText.text = itemUsed[0].ToString();
        floatingCountText.text= itemUsed[1].ToString();
        barrierCountText.text= itemUsed[2].ToString();
        tiresCountText.text= itemUsed[3].ToString();
        rocketCountText.text= itemUsed[4].ToString();
    }
}
