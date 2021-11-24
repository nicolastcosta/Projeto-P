using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pack : MonoBehaviour
{
    public int packLevel;

    public GameObject[] units;

    private void Awake()
    {
        for (int u = 0; u < units.Length; u++)
        {
            units[u].GetComponent<Unit_Info>().unitLevel = packLevel;
        }
    }
}
