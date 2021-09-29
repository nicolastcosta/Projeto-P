using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Interact_Enemies : MonoBehaviour
{
    bool interact = true;
    bool startBattle;

    [SerializeField] Animator fadeAnimator;

    // Update is called once per frame
    void Update()
    {
        BattleStart();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" && interact == true)
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
        }
    }
}
