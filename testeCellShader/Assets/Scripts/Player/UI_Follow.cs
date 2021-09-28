using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Follow : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private Vector3 offset;

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(target.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }
}
