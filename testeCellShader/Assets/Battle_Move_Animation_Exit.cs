using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle_Move_Animation_Exit : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {   
        GameObject modelHost = animator.gameObject.GetComponent<Select_Unit>().host;
        GameObject battleSystem = modelHost.GetComponent<Unit_Info>().battleSystem;
        Debug.Log("Animation current action: " +battleSystem.GetComponent<Battle>().curAction);

        if(battleSystem.GetComponent<Battle>().curAction < battleSystem.GetComponent<Battle>().maxActions)
        {
            if (modelHost == battleSystem.GetComponent<Battle>().playerSelected[battleSystem.GetComponent<Battle>().curAction])
            {
                battleSystem.GetComponent<Battle>().Actions();
            }
        }
        else
        {
            animator.SetBool("move", false);
        }
        
    }

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
