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
        GameObject modelHost = animator.gameObject.transform.parent.gameObject;
        GameObject battleSystem = modelHost.GetComponent<Unit_Info>().battleSystem;

        int curAction = battleSystem.GetComponent<New_Battle_System>().currentAction;



        if (modelHost.GetComponent<Unit_Info>().unitPos == battleSystem.GetComponent<New_Battle_System>().targets[curAction -1])
            battleSystem.GetComponent<New_Battle_System>().Actions(curAction);

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
