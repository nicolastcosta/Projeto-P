using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exp_Bank : MonoBehaviour
{
    [SerializeField]
    private GameObject playerUnit;
    
    // Start is called before the first frame update
    void Start()
    {
        playerUnit.GetComponent<Unit_Info>().curExp += Scene_Variables.instance.exp;
    }
}
