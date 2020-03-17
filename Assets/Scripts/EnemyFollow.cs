﻿using System.Collections;
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
            if (!hasTriggeredDialogue)
            {
                dialogueTrigger.TriggerDialogue();
                hasTriggeredDialogue = true;
            }
        }
        //else
        //{
        //    dialogueTrigger.TriggerDialogue();
        //}
        //Maybe make an else statement here to trigger the dialogue and maybe a boolean so
        //after the dialogue, the enemy will not chase anymore
    }
}