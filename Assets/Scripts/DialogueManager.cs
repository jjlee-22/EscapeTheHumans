using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{

    public Text nameText;
    public Text dialogueText;
    public StressBar stressBar;
    public Animator animator;
    public GameObject continueButton;
    public GameObject yesButton;
    public GameObject noButton;

    private Queue<Sentence> sentences;
    private Queue<Sentence> yesSentences;
    private Queue<Sentence> noSentences;

    void Start()
    {
        sentences = new Queue<Sentence>();
        yesSentences = new Queue<Sentence>();
        noSentences = new Queue<Sentence>();
    }

    /**
     * Starts the given dialogue. Accesses the string array called 'sentences'
     * within each Dialogue object. Takes a Dialogue object as an input. This
     * will also disable the player Movement script in order to freeze player
     * movement during the duration of the dialgue.
     */
    public void StartDialogue(Dialogue d)
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled
            = false; // Freezes player movement
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

            foreach (Sentence sentence in d.yesSentences)
            {
                yesSentences.Enqueue(sentence);
            }
            foreach (Sentence sentence in d.noSentences)
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

        foreach (Sentence sentence in d.sentences)
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

        AffectStress(sentences.Peek());

        string sentence = sentences.Dequeue().sentence;
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    /**
     * This will print the next sentence if answered yes to previous question.
     * This will stop all animations that are already in progress.
     */
    public void DisplayNextYesSentence()
    {
        CheckLastYesSentence(yesSentences.Peek());
        AffectStress(yesSentences.Peek());
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
        CheckLastNoSentence(noSentences.Peek());
        AffectStress(noSentences.Peek());
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
     * dialogue box. Enables the player Movement script again in order to allow
     * the player to move again.
     */
    public void EndDialogue()
    {
        animator.SetBool("isOpen", false);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled
            = true; // Unfreeze player movement
        GameObject.FindGameObjectWithTag("talker").GetComponent<Animator>().enabled
            = true;
        GameObject.FindGameObjectWithTag("talker").tag="Untagged";
    }

    /**
     * Checks to see if the current sentence is the last in the Dialogue tree.
     * If it is, the continue button returns to close the dialogue since it
     * should not be a 'yes' or 'no' question.
     */
    public void CheckLastYesSentence(Sentence s)
    {
        if (yesSentences.Count == 1 || s.isFinal)
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
    public void CheckLastNoSentence(Sentence s)
    {
        if (noSentences.Count == 1 || s.isFinal)
        {
            continueButton.SetActive(true);
            yesButton.SetActive(false);
            noButton.SetActive(false);
            return;
        }
        return;
    }

    /**
     * Will adjust the stress amount on the stress bar according to the
     * amount given as part of the sentence. 
     */
    public void AffectStress(Sentence s)
    {
        float amount = s.getEffectOnStress();
        if (amount < 0)
        {
            stressBar.decreaseStress(Math.Abs(amount));
        }
        else if (amount > 0)
        {
            stressBar.increaseStress(Math.Abs(amount));
        }
    }
}
