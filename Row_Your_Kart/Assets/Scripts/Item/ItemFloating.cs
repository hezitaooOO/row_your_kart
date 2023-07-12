using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemFloating : MonoBehaviour, ItemInterface
{

    public ItemUIController itemUIController;
    public PlayerController player;
    public Sprite sprite;


    private GameObject floatedEnemy;

    private int itemID = 1;


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

        var enemies = GameObject.FindGameObjectsWithTag("Enemy");

        var index = 0;
        var minDistance = Vector3.Distance(enemies[0].transform.position, player.transform.position);

        for (int i = 1; i < enemies.Length; i++)
        {
            var d = Vector3.Distance(enemies[i].transform.position, player.transform.position);
            if ( d < minDistance) {
                index = i;
                minDistance = d;
            }
        }

        floatedEnemy = enemies[index];
        KartFloater floater = floatedEnemy.GetComponent<KartFloater>();
        floater.isFloating = true;
        floater.setFloatingStartTime();
        itemUIController.RemoveItem();

    }
}
