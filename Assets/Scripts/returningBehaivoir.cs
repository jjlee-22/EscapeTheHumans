using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class returningBehaivoir : StateMachineBehaviour
{
    private Vector2 start;
    private Transform target;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        start= new Vector2(animator.GetFloat("originalX"), animator.GetFloat("originalY"));
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector2.Distance(animator.transform.position,start)!=0.0)
        {
            float oldX=animator.transform.position.x;
            float oldY=animator.transform.position.y;
            animator.transform.position = Vector3.MoveTowards(animator.transform.position,start,animator.GetFloat("Speed") * Time.deltaTime);
            Vector3 movement = new Vector3(animator.transform.position.x, animator.transform.position.y, 0.0f);
                animator.SetFloat("Horizontal", movement.x-oldX);
                animator.SetFloat("Vertical", movement.y-oldY);
            
             if(Vector2.Distance(animator.transform.position, target.position) > 2){
                if(Vector2.Distance(animator.transform.position, target.position) <animator.GetFloat("Range"))
                {
                    animator.SetBool("isFollowing",true);
                    animator.SetBool("isReturning", false);
                }
            }
        }
        else
        {
            animator.SetBool("isReturning", false);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
