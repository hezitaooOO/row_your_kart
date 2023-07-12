using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class modeactive : MonoBehaviour
{
    public static GameObject aaa;
     public static  bool bb;
          public static  bool cc;

    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    public void Update()
    {
       cc= bb;
    }
    public void active()
        {
        bb=true;
        }
    public void negative()
    {
        bb=false;

    }
}
