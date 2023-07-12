using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomItem : MonoBehaviour
{

    public Sprite Icon;
    private int itemIndex;
    private GeneralTextUI generalTextUI;
    private RankingUIController rankingController;

    void Start()
    {
        generalTextUI = GameObject.Find("GeneralTextUI").GetComponent<GeneralTextUI>();
        rankingController = GameObject.Find("RankingUI").GetComponent<RankingUIController>();
        var renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = Icon;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetStatus(bool s)
    {
        this.GetComponent<SphereCollider>().enabled = s;
        this.GetComponentInChildren<MeshRenderer>().enabled = s;
        this.GetComponentInChildren<SpriteRenderer>().enabled = s;
    }


    void Respawn()
    {
        SetStatus(true);
    }

    bool CheckItemRankValid(int i)
    {
        float rankPoint = ((float) rankingController.rank)  / rankingController.totalKartCount;
        Debug.Log("rank");

        Debug.Log(rankPoint);
        if (rankPoint > 0.75)
        {
            if (i == 4)
            {
                return true;
            }
            if (i == 1)
            {
                return true;
            }
        }

        if (rankPoint > 0.25)
        {
            if (i == 1)
            {
                return true;
            }
            if (i == 2)
            {
                return true;
            }
            if (i == 3)
            {
                return true;
            }
            if (i == 0)
            {
                return true;
            }
        }

        if (rankPoint >= 0)
        {
            if (i == 1)
            {
                return true;
            }
            if (i == 0)
            {
                return true;
            }
        }

        return false;
    }


    void OnTriggerEnter(Collider c)
    {
        // TODO optimize random logic with ranking
        if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Player"))
        {

            var playerController = c.gameObject.GetComponentInParent<PlayerController>();

            ItemInterface primary = playerController.itemUIController.primaryItem;
            ItemInterface secondary = playerController.itemUIController.secondaryItem;
            if (primary == null || secondary == null)
            {
                Random.InitState(System.DateTime.Now.Millisecond);
                itemIndex = Random.Range(0, 5);
                while((primary != null && itemIndex == primary.GetID()) || !CheckItemRankValid(itemIndex))
                {
                    Random.InitState(System.DateTime.Now.Millisecond);
                    itemIndex = Random.Range(0, 5);
                }
                //itemIndex = 4;

                switch (itemIndex)
                {
                    case 0:
                        var itemSpeedUp = playerController.items.GetComponentInChildren<ItemSpeedUp>();
                        playerController.AddItem(itemSpeedUp);
                        break;
                    case 1:
                        var itemFloating = playerController.items.GetComponentInChildren<ItemFloating>();
                        playerController.AddItem(itemFloating);
                        break;
                    case 2:
                        var itemBarrier = playerController.items.GetComponentInChildren<ItemBarrier>();
                        playerController.AddItem(itemBarrier);
                        break;
                    case 3:
                        var itemTires = playerController.items.GetComponentInChildren<ItemTires>();
                        playerController.AddItem(itemTires);
                        break;
                    case 4:
                        var itemRocket = playerController.items.GetComponentInChildren<ItemRocket>();
                        playerController.AddItem(itemRocket);
                        break;
                    default:
                        break;

                }
            }
            SetStatus(false);
            Invoke("Respawn", 3);
        }
        else if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Enemy"))
        {
            // TODO: need more enemy items.. and theres vunlneribilty in invincible for enemy
            var aiController = c.gameObject.GetComponentInParent<TrackerAIController>();
            var player = GameObject.FindGameObjectWithTag("Player");
            Random.InitState(System.DateTime.Now.Millisecond);
            itemIndex = Random.Range(0, 5);
            //itemIndex = 4;
            switch (itemIndex)
            {
                case 0:
                    break;
                case 1:
                    break;
                case 2:
                    // 1/3 chance to make player sliping
                    if (Random.Range(0, 3) == 0)
                    {
                        KartController playerKart = player.GetComponent<KartController>();
                        if (!playerKart.sliping)
                        {
                            float originalSlipAllowance = playerKart.slipAllowanceSpeed;
                            float originalSlipAllowanceSpeed = playerKart.slipAllowance;
                            playerKart.slipAllowanceSpeed = 5;
                            playerKart.slipAllowance = 0.1f;
                            playerKart.sliping = true;
                            StartCoroutine(resetSlip(playerKart, originalSlipAllowance, originalSlipAllowanceSpeed));
                            generalTextUI.enableGeneralText("Enemy using slippy item on player!");
                        }
                    }
                    break;
                case 3:
                    // 1/3 chance to make player floating
                    if(Random.Range(0, 3) == 0)
                    {
                        KartFloater floater = player.GetComponent<KartFloater>();
                        floater.isFloating = true;
                        floater.setFloatingStartTime();
                        generalTextUI.enableGeneralText("Enemy using Floating item on player!");
                    }
                    break;
                case 4:
                    //aiController.enableInInvincible();
                    break;
                default:
                    break;

            }
            SetStatus(false);
            Invoke("Respawn", 3);
        }
    }

    IEnumerator resetSlip(KartController playerKart, float originalSlipAllowance, float originalSlipAllowanceSpeed)
    {
        yield return new WaitForSeconds(3);
        playerKart.slipAllowanceSpeed = originalSlipAllowance;
        playerKart.slipAllowance = originalSlipAllowanceSpeed;
        playerKart.sliping = false;
    }
}
