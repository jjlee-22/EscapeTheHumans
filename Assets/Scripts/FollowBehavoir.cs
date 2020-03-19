using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBehavoir : StateMachineBehaviour
{
    private Transform playerPos;
    public float speed;
    public DialogueTrigger dialogueTrigger;
    private bool hasTriggeredDialogue = false;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").transform;
        dialogueTrigger=GameObject.Find("Enemy2").GetComponent<DialogueTrigger>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("isFollowing")){
            if(Vector2.Distance(animator.transform.position, playerPos.position) > 2){
                animator.transform.position = Vector3.MoveTowards(animator.transform.position, playerPos.position,speed * Time.deltaTime);
                if(Vector2.Distance(animator.transform.position, playerPos.position) >5){
                    animator.SetBool("isFollowing",false);
                }
            }
            else
            {
                /**
                * Triggers the dialogue if it has not been triggered yet and stops
                * the enemy from following.
                */
                if (!hasTriggeredDialogue)
                {
                    dialogueTrigger.TriggerDialogue();
                   
                    hasTriggeredDialogue = true;
                    animator.SetBool("hadDialog",true);
                    animator.SetBool("isFollowing", false);
                }
            }
        //after the dialogue, the enemy will not chase anymore
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
