using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Search_for_Battle : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPack, fadeEffect;

    private float timer;
    private bool canSearch;

    private void Update()
    {
        if (canSearch == false)
        {
            if (timer < 3)
                timer += Time.deltaTime;
            else
                canSearch = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<Unit_Info>().isDead == false && canSearch == true)
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
                                Scene_Variables.instance.enemyIndex = transform.parent.gameObject.GetComponent<Enemy_Pack>().packIndex;

                                Scene_Variables.instance.playerCurrentPosition = other.transform.position;

                                Scene_Variables.instance.companion1CurrentPosition = playerCompenions.companion[0].transform.position;
                                Scene_Variables.instance.companion2CurrentPosition = playerCompenions.companion[1].transform.position;

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
}
