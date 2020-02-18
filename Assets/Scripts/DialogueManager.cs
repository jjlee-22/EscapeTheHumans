using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Animator animator;

    private Queue<string> sentences;

    void Start()
    {
        sentences = new Queue<string>();
    }

    /**
     * Starts the given dialogue. Accesses the string array called 'sentences'
     * within each Dialogue object. Takes a Dialogue object as an input.
     */
    public void StartDialogue(Dialogue d)
    {
        //Debug.Log("Starting conversation with " + d.name);
        animator.SetBool("isOpen", true);
        sentences.Clear();

        nameText.text = d.name;

        foreach (string sentence in d.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        // If you have reached the end of the queue, end the dialogue.
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //dialogueText.text = sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    /**
     * This will enter 'End of conversation.' into the debug log. This will later
     * be adapted to close the dialogue window and allow the game to continue.
     */
    public void EndDialogue()
    {
        //Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
    }
}
