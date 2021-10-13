using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_System : MonoBehaviour
{
    [SerializeField]
    private GameObject battleCamera;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartBattle(GameObject player, GameObject enemy)
    {
        //Set true to combat
        player.GetComponent<Unit_Info>().isInCombat = true;
        enemy.GetComponent<Unit_Info>().isInCombat = true;

        //Disable things
        player.GetComponent<Player_Movimentation>().enabled = false;


        battleCamera.SetActive(true);

    }
}
