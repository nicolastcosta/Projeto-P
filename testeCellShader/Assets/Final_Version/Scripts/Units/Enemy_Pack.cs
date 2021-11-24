using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pack : MonoBehaviour
{
    public int packLevel;
    public int isDead;

    public int packIndex;

    public GameObject[] units;

    private void Awake()
    {
        isDead = PlayerPrefs.GetInt("isDead" + packIndex.ToString());
        PlayerPrefs.SetInt("isDead" + packIndex.ToString(), isDead);
        
        for (int u = 0; u < units.Length; u++)
        {
            units[u].GetComponent<Unit_Info>().unitLevel = packLevel;

            if (isDead == 1)
                units[u].GetComponent<Unit_Info>().isDead = true;
            else
                units[u].GetComponent<Unit_Info>().isDead = false;
        }
    }
}
