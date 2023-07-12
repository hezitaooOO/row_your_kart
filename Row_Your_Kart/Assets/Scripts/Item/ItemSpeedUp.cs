using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedUp : MonoBehaviour, ItemInterface
{

    public ItemUIController itemUIController;
    public PlayerController player;

    public Sprite sprite;
    private int itemID = 0;

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

    public void OnUse() {
        KartController kartController = player.GetComponent<KartController>();

        player.inSpeedUp = true;
        itemUIController.StartBlinking();
        Invoke("CleanUI", 3);
    }

    void CleanUI()
    {
        player.inSpeedUp = false;
        itemUIController.StopBlinking();
        itemUIController.RemoveItem();
    }
}
