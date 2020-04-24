using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue()
    {
        gameObject.GetComponent<Animator>().enabled= false;
        gameObject.tag="talker";
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
