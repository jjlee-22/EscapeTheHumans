using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public Animator animator;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;

    private Queue<string> sentences;
    private Queue<string> yesSentences;
    private Queue<string> noSentences;

    void Start()
    {
        sentences = new Queue<string>();
        yesSentences = new Queue<string>();
        noSentences = new Queue<string>();
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
        yesSentences.Clear();
        noSentences.Clear();
       
        nameText.text = d.name;
        if (d.isDialogueTree)
        { 
            continueButton.SetActive(false);
            yesButton.SetActive(true);
            noButton.SetActive(true);

            foreach (string sentence in d.yesSentences)
            {
                yesSentences.Enqueue(sentence);
            }
            foreach (string sentence in d.noSentences)
            {
                noSentences.Enqueue(sentence);
            }
        }
        else
        {
            continueButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
        }

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
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /**
     * This will print the next sentence if answered yes to previous question.
     * This will stop all animations that are already in progress.
     */
    public void DisplayNextYesSentence()
    {
        CheckLastYesSentence();
        sentences.Enqueue(yesSentences.Dequeue());
        noSentences.Dequeue();
        DisplayNextSentence();
    }

    /**
     * This will print the next sentence if answered no to previous question.
     * This will stop all animations that are already in progress.
     */
    public void DisplayNextNoSentence()
    {
        CheckLastNoSentence();
        sentences.Enqueue(noSentences.Dequeue());
        yesSentences.Dequeue();
        DisplayNextSentence();
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
        animator.SetBool("isOpen", false);
    }

    /**
     * Checks to see if the current sentence is the last in the Dialogue tree.
     * If it is, the continue button returns to close the dialogue since it
     * should not be a 'yes' or 'no' question.
     */
    public void CheckLastYesSentence()
    {
        if (yesSentences.Count == 1)
        {
            continueButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
            return;
        }
        return;
    }

    /**
     * Checks to see if the current sentence is the last in the Dialogue tree.
     * If it is, the continue button returns to close the dialogue since it
     * should not be a 'yes' or 'no' question.
     */
    public void CheckLastNoSentence()
    {
        if (noSentences.Count == 1)
        {
            continueButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
            return;
        }
        return;
    }
}
