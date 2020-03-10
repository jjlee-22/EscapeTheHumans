using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
	public Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
	    Vector3 movement= new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);

	    animator.SetFloat("Horizontal", movement.x);
	    animator.SetFloat("Vertical", movement.y);
	    animator.SetFloat("Magnitude", movement.magnitude);

	    transform.position= transform.position+movement* Time.deltaTime*4;
        
        if (movement.y > 0)
        {
            animator.SetFloat("Direction", 2);
        }
        else if (movement.y < 0)
        {
            animator.SetFloat("Direction", 0);
        }
        else if (movement.x < 0)
        {
            animator.SetFloat("Direction", 1);
        }
        else if (movement.x > 0)
        {
            animator.SetFloat("Direction", 3);
        }
    }
}
