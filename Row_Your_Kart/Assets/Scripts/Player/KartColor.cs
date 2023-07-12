using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class KartColor : MonoBehaviour
{


    public Material hpymaterial;
    public static Color sss;
    public static Color QQQ;


    // Start is called before the first frame update
    void Start()
    {
        //hpymaterial = GetComponent<SkinnedMeshRenderer>().material;
        sss = new Color(0f / 255f, 50f / 255f, 150f / 255f);
    }

    // Update is called once per frame
    void Update()
    {
        QQQ=sss;
        //print( "KartColor.QQQ");
    }
    public void redcolor ()
    { 
        //hpymaterial.color =  Color.red;
         //sss =hpymaterial.color;
         sss=Color.red;
        // SceneManager.LoadScene("Terrain");
         
    }
    public void yellowcolor()
    { 
        sss =  Color.yellow;
    }
    public void bluecolor()
    {
        //sss =  Color.blue;
        sss = new Color(0f / 255f, 50f / 255f, 150f / 255f);
    }
    public void greencolor()
    {
        //sss =  Color.green;
        sss = new Color(104f / 255f, 195f / 255f, 79f / 255f);
        //SceneManager.LoadScene("Terrain");
    }
    public void whitecolor()
    { 
        sss =  Color.white;
    }
    public void pinkcolor()
    { 
        sss =  Color.black;
    }

}
