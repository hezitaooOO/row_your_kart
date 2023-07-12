using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemUIController : MonoBehaviour
{

    public ItemInterface primaryItem;
    public ItemInterface secondaryItem;

    public Image itemImageQ;
    public Image itemImageE;


    public GameObject q;
    public GameObject e;

    public AudioSource switchingItem;

    private bool itemInUsing = false;

    private int[] itemUsed = new int[5];

    void Start()
    {
        RemoveQImage();
        RemoveEImage();
    }


    private void setEImage(Sprite sprite)
    {
        itemImageE.sprite = sprite;
        var tempColor = itemImageE.color;
        tempColor.a = 1f;
        itemImageE.color = tempColor;
        e.SetActive(true);
    }


    public void SetQImage(Sprite sprite)
    {
        itemImageQ.sprite = sprite;
        var tempColor = itemImageQ.color;
        tempColor.a = 1f;
        itemImageQ.color = tempColor;
        q.SetActive(true);
    }

    public void SwitchItems()
    {
        if (itemInUsing)
        {
            return;
        }
        if (primaryItem == null || secondaryItem == null )
        {
            return;

        }
        var temp = itemImageE.sprite;
        itemImageE.sprite = itemImageQ.sprite;
        itemImageQ.sprite = temp;

        var tempItem = primaryItem;
        primaryItem = secondaryItem;
        secondaryItem = tempItem;
        switchingItem.Play();
    }

    public int UseItem()
    {
        if (primaryItem != null && !itemInUsing)
        {
            itemUsed[primaryItem.GetID()]++;
            //GameResultsManager.itemUsed[primaryItem.GetID()]++;
            primaryItem.OnUse();
            switchingItem.Play();
            return 1;
        }
        return 0;
    }



    public void RemoveItem()
    {
        RemoveQImage();
        primaryItem = secondaryItem;
        secondaryItem = null;
        if (primaryItem != null)
        {
            RemoveEImage();
            SetQImage(primaryItem.GetSprite());
        }
    }

    private void RemoveQImage()
    {
        var tempColor = itemImageQ.color;
        tempColor.a = 0f;
        itemImageQ.color = tempColor;
        itemImageQ.sprite = null;
        q.SetActive(false);
    }


    private void RemoveEImage()
    {
        var tempColor = itemImageE.color;
        tempColor.a = 0f;
        itemImageE.color = tempColor;
        itemImageE.sprite = null;
        e.SetActive(false);
    }

    public int AddItem(ItemInterface item)
    {
        if (primaryItem != null && secondaryItem != null)
        {
            return 0;
        }

        item.Init();

        if (primaryItem == null)
        {
            SetQImage(item.GetSprite());
            primaryItem = item;

        }
        else if (primaryItem.GetID() == item.GetID())
        {
            Debug.Log("same");
            return 0;
        }
        else if (secondaryItem == null)
        {
            setEImage(item.GetSprite());
            secondaryItem = item;
        }
        return 1;
    }


    IEnumerator Blink()
    {
        while (true)
        {
            switch (itemImageQ.color.a.ToString())
            {
                case "0":
                    itemImageQ.color = new Color(itemImageQ.color.r, itemImageQ.color.g, itemImageQ.color.b, 1);
                    yield return new WaitForSeconds(0.1f);
                    break;
                case "1":
                    itemImageQ.color = new Color(itemImageQ.color.r, itemImageQ.color.g, itemImageQ.color.b, 0);
                    yield return new WaitForSeconds(0.1f);
                    break;
            }
        }
    }

    public void StartBlinking()
    {
        StopAllCoroutines();
        StartCoroutine("Blink");
        itemInUsing = true;
    }

    public void StopBlinking()
    {
        StopAllCoroutines();
        itemInUsing = false;
    }


    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < itemUsed.Length; i++) {
            GameResultsManager.itemUsed[i] = itemUsed[i];
        }
    }

    public int[] getItemUsedStats() {
        return itemUsed;
    }
}
