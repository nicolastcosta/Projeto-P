using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search_for_Battle : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPack, fadeEffect;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.GetComponent<Unit_Info>() && other.GetComponent<Unit_Companions>())
            {
                Unit_Info playerUnitInfo = other.GetComponent<Unit_Info>();
                Unit_Companions playerCompenions = other.GetComponent<Unit_Companions>();

                if (playerCompenions.companion.Length > 0 && playerUnitInfo.isInCombat == false)
                {
                    playerUnitInfo.isInCombat = true;

                    int numberOfCompanions = 0;

                    for (int n = 0; n < playerCompenions.companion.Length; n++)
                    {
                        if (playerCompenions.companion[n] != null)
                            numberOfCompanions++;
                    }

                    if (fadeEffect != null)
                    {
                        Animator fadeAnimator = fadeEffect.GetComponent<Animator>();

                        if (numberOfCompanions == 2)
                        {
                            Scene_Variables.instance.playerLevel = playerUnitInfo.unitLevel;
                            Scene_Variables.instance.enemyLevel = GetComponent<Unit_Info>().unitLevel;

                            fadeAnimator.Play("Combat");
                        }

                        else
                            fadeAnimator.Play("Lose");
                    }
                    else
                        Debug.Log("Error to start battle, need a fade effect");
                }
            }
        }
    }
}
