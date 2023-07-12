using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBarrier : MonoBehaviour, ItemInterface
{

    public ItemUIController itemUIController;
    public PlayerController player;

    public Sprite sprite;

    private int itemID = 2;

    public void Init()
    {
    }

    public Sprite GetSprite()
    {
        return sprite;
    }


    public int GetID()
    {
        return itemID;
    }

    public void OnUse()
    {
        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.localScale += new Vector3(1, 1, 1);
        }

        itemUIController.RemoveItem();

        Invoke("ResetBarrier", 3);
    }


    void ResetBarrier()
    {
        var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
        foreach (GameObject obstacle in obstacles)
        {
            obstacle.transform.localScale -= new Vector3(1, 1, 1);
        }
    }

}
