using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolBehaivoir : StateMachineBehaviour
{
    public float speed;
    private float waitTime;
    public float startwaittime;

    public Transform[] movespots;
    private int randomspot;
     //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Patrol enemy=GameObject.Find(animator.transform.name).GetComponent<Patrol>();
        movespots=enemy.movespots;
        randomspot = Random.Range(0, movespots.Length);
        waitTime = startwaittime;   
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float oldX=animator.transform.position.x;
        float oldY=animator.transform.position.y;
        if(Vector2.Distance(animator.transform.position, movespots[randomspot].position) > 0.2f)
        {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, movespots[randomspot].position, speed * Time.deltaTime);
        }
        else
        {
             randomspot = Random.Range(0, movespots.Length);
        }
        Vector3 movement = new Vector3(animator.transform.position.x, animator.transform.position.y, 0.0f);
        animator.SetFloat("Horizontal", movement.x-oldX);
        animator.SetFloat("Vertical", movement.y-oldY);
        animator.SetFloat("Magnitude", movement.magnitude);
        
        Transform target=GameObject.FindGameObjectWithTag("Player").transform;
         if(Vector2.Distance(animator.transform.position, target.position) > 2){
                if(Vector2.Distance(animator.transform.position, target.position) <5)
                {
                    animator.SetBool("isFollowing",true);
                    animator.SetBool("isPatrolling",false);
                }
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
