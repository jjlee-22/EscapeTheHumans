using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class idlePatrolNPCBehaviour : StateMachineBehaviour
{
    private Transform target;
    private Vector2 start;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if(animator.GetFloat("originalX")==0.0)
        {
            animator.SetFloat("originalX", animator.transform.position.x);
            animator.SetFloat("originalY", animator.transform.position.y);
            start= new Vector2(animator.transform.position.x, animator.transform.position.y);
        }
        else{
            start= new Vector2(animator.GetFloat("originalX"), animator.GetFloat("originalY"));
        }
         animator.SetBool("isPatrolling",true);
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
                else{
                    animator.SetBool("isPatrolling",true);
                }
            }
        }
        //Makes some kind of waiting code if dialogue is still open
        else if (Vector2.Distance(animator.transform.position,start)>1.0)
        {
            animator.SetBool("isReturning", true);
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
