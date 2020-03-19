using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idleBehaviour : StateMachineBehaviour
{
    private Transform target;
    public DialogueTrigger dialogueTrigger;
    private bool hasTriggeredDialogue = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueTrigger=GameObject.Find("Enemy2").GetComponent<DialogueTrigger>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(!animator.GetBool("hadDialog")){
            if(Vector2.Distance(animator.transform.position, target.position) > 2){
                if(Vector2.Distance(animator.transform.position, target.position) <5)
                {
                    animator.SetBool("isFollowing",true);
                }
            }
        }
        else if(!hasTriggeredDialogue){
             dialogueTrigger.TriggerDialogue();
                    try
                    {
                        target = null;
                    }
                    catch (Exception e)
                    {
                        // This is just here to prevent null reference errors once the
                        // following NPC no longer needs a target.
                        Console.WriteLine("");
                    }
                hasTriggeredDialogue=true;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
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
