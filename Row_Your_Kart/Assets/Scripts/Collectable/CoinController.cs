using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{

    public AudioSource coinSound;

    public ScoreManager scoreManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void SetStatus(bool s)
    {
        this.GetComponent<CapsuleCollider>().enabled = s;
        this.GetComponentInChildren<MeshRenderer>().enabled = s;
    }


    void Respawn()
    {
        SetStatus(true);
    }


    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Player"))
        {
            scoreManager.UpdateCurrentScore(100);

            coinSound.Play();
            SetStatus(false);
            Invoke("Respawn", 3);
        }
        else if (c.attachedRigidbody != null && c.transform.parent != null && c.transform.parent.CompareTag("Enemy"))
        {
            SetStatus(false);
            Invoke("Respawn", 3);
        }

    }
}
