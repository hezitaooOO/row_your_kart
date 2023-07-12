using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedOmeterController : MonoBehaviour
{
    public GameObject player;
    public GameObject needle;
    private float startPosition = 224.16f;
    private float endPosition = 313.7f;
    private float desiredPosition;
    

    void Start()
    {
        //player = GameObject.Find("Kart");
    }

    // Update is called once per frame
    void Update()
    {
        updateNeedle();
    }

    public void updateNeedle() {
        desiredPosition = startPosition - endPosition;
        KartController controller = player.GetComponent<KartController>();
        float kartSpeed = controller.currentSpeed;
        //Debug.Log("kartSpeed: " + kartSpeed);
        float temp = 3 * kartSpeed / 280;
        needle.transform.eulerAngles = new Vector3(0, 0, (startPosition + temp * desiredPosition));
    }
}
