using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTires : MonoBehaviour, ItemInterface
{

    public ItemUIController itemUIController;
    public PlayerController player;

    public Sprite[] sprites;

    private int itemID = 3;


    public GameObject tirePrefab;

    public int count;


    public void Init()
    {
        count = 3;
    }

    public Sprite GetSprite()
    {
        return sprites[3-count];
    }

    public int GetID()
    {
        return itemID;
    }

    public void OnUse()
    {

        Vector3 position = player.transform.position + player.transform.forward + player.transform.up;

        var tire = Instantiate(tirePrefab, position, Quaternion.identity);

        tire.GetComponent<Rigidbody>().velocity = player.GetComponent<Rigidbody>().velocity;

        tire.GetComponent<Rigidbody>().AddForce(player.transform.forward * 30, ForceMode.VelocityChange);

        count--;

        if (count == 0)
        {
            itemUIController.RemoveItem();
        } else
        {
            itemUIController.SetQImage(GetSprite());
        }

    }
}
