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

    /**
     * Checks to see if the sentences Queue is empty and ends the dialogue if
     * there are no more sentences. This will print the sentence letter by
     * letter and stop animations that are already in progress.
     */
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

    /**
     * Prints the sentence letter by letter and leaves a single
     * frame in-between letters.
     */
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
     * Sets the isOpen animation attribute to false in order to close the
     * dialogue box.
     */
    public void EndDialogue()
    {
        //Debug.Log("End of conversation.");
        animator.SetBool("isOpen", false);
    }
}
