using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class KartInvisibleMaker : MonoBehaviour
{
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void makeSemiTransparent()
    {
        //Material material = gameObject.GetComponent<Renderer>().material;
        //Color oldColor = material.color;
        //Color newColor = new Color(oldColor.r, oldColor.b, oldColor.b, newAlpha);
        //material.SetColor("_Color", newColor);
        float oldAlpha = gameObject.GetComponent<Renderer>().material.color.a;
        var trans = 1;
        var col = gameObject.GetComponent<Renderer>().material.color;
        col.a = trans;
        //gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        //Debug.Log("Old alpha value is " + oldAlpha + ". New alpha value is " + gameObject.GetComponent<Renderer>().material.color.a);
        //Debug.Log(col.r + " " + col.g + " " + col.b + " " + col.a);
    }

    public void makeOpaque()
    {
        //Material material = gameObject.GetComponent<Renderer>().material;
        //Color oldColor = material.color;
        //Color newColor = new Color(oldColor.r, oldColor.b, oldColor.b, 1f);
        //material.SetColor("_Color", newColor);
        var trans = 1.0f;
        var col = gameObject.GetComponent<Renderer>().material.color;
        col.a = trans;
    }
}
