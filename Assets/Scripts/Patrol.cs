using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startwaittime;

    public Transform[] movespots;
    private int randomspot;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        randomspot = Random.Range(0, movespots.Length);
        waitTime = startwaittime;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movespots[randomspot].position, speed * Time.deltaTime);
        Vector3 movement = new Vector3(transform.position.x, transform.position.y, 0.0f);
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Magnitude", movement.magnitude);

        if (Vector2.Distance(transform.position, movespots[randomspot].position) < 0.2f)
        {
            if(waitTime<=0)
            {
                randomspot = Random.Range(0, movespots.Length);
                waitTime = startwaittime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
    }
}
