using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Load_Variables : MonoBehaviour
{
    [SerializeField]
    private GameObject playerUnit;
    
    // Start is called before the first frame update
    void Start()
    {
        playerUnit.GetComponent<Unit_Info>().curExp += Scene_Variables.instance.exp;
        PlayerPrefs.SetInt("playerExp", playerUnit.GetComponent<Unit_Info>().curExp);
    }
}
