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
    // Start is called before the first frame update
    void Start()
    {
        randomspot = Random.Range(0, movespots.Length);
        waitTime = startwaittime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, movespots[randomspot].position, speed * Time.deltaTime);

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
