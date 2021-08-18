using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask_Occlusion : MonoBehaviour
{
    public GameObject[] maskObj;
    void Start()
    {
        for(int i = 0; i < maskObj.Length; i++)
        {
            maskObj[i].GetComponent<MeshRenderer>().material.renderQueue = 3002;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
