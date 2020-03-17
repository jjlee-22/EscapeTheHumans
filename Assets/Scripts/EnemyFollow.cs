using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public float speed;
    public DialogueTrigger dialogueTrigger;
    private Transform target;
    private bool hasTriggeredDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(transform.position, target.position) > 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
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
                hasTriggeredDialogue = true;
            }
        }
        //after the dialogue, the enemy will not chase anymore
    }
}
