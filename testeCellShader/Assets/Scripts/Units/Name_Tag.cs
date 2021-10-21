using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Name_Tag : MonoBehaviour
{
    private Camera cam;

    private Transform faceTarget;
    private Vector3 offset;

    void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(cam.transform.position);
        /*Vector3 pos = target.position + offset;

        if (transform.position != pos)
            transform.position = pos;*/
    }

    public void SetAttributes(string nameTemp, Vector3 offsetTemp)
    {
        GetComponent<TextMeshPro>().text = nameTemp;
        offset = offsetTemp;
    }
}
