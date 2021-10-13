using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StartBattle : MonoBehaviour
{
    bool interact = true;
    bool startBattle;

    [SerializeField]
    private GameObject target;

    [SerializeField] 
    private Animator fadeAnimator;

    [SerializeField]
    private GameObject battleBoard;

    // Update is called once per frame
    void Update()
    {
        BattleStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && interact == true)
        {
            startBattle = true;
        }
    }

    void BattleStart()
    {
        if (startBattle == true && interact == true)
        {
            interact = false;
            fadeAnimator.Play("Fade_Combat_Start");
            battleBoard.GetComponent<Battle_System>().StartBattle(target, gameObject);
        }
    }
}
