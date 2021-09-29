using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Name_Tag : MonoBehaviour
{
    private Camera cam;
    private Transform target;
    private Vector3 offset;

    [SerializeField]
    private TextMeshProUGUI text;

    private void Awake()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = cam.WorldToScreenPoint(target.position + offset);

        if (transform.position != pos)
            transform.position = pos;
    }

    public void SetAttributes(string name, Transform targetTemp, Vector3 nameTagOffset)
    {
        text.text = name;
        target = targetTemp;
        offset = nameTagOffset;
    }
}
