using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRocket : MonoBehaviour, ItemInterface
{
    public ItemUIController itemUIController;
    public PlayerController player;

    public Sprite sprite;
    public AudioSource flying;
    public AudioSource landing;


    private int itemID = 4;


    public void Init()
    {
    }

    public int GetID()
    {
        return itemID;
    }

    public Sprite GetSprite()
    {
        return sprite;
    }

    public void OnUse()
    {
        KartController kartController = player.GetComponent<KartController>();

        player.enterInvinvincible();
        itemUIController.StartBlinking();
        flying.Play();
        Invoke("CleanUI", 3);
    }

    void CleanUI()
    {
        flying.Stop();
        landing.Play();
        player.leaveInvinvincible();
        itemUIController.StopBlinking();
        itemUIController.RemoveItem();
    }
}
