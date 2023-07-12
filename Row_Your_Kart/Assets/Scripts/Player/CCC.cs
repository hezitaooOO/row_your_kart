using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCC : MonoBehaviour
{   private Material mm;
    private Color zzz;
    

    // Start is called before the first frame update
    void Start()
    {
        mm = GetComponent<SkinnedMeshRenderer>().material;
        mm.color= KartColor.QQQ;
    }

    // Update is called once per frame
    void Update()
    {
        //zzz =  KartColor.sss;
        mm = GetComponent<SkinnedMeshRenderer>().material;
        mm.color = KartColor.QQQ;
    }
}
