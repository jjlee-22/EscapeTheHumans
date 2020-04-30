using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveManager : MonoBehaviour
{
    public Text objectiveListNameText;
    public Text objectiveListText;
    public Objective[] objectives;
    [HideInInspector]
    public bool levelComplete = false;

    void Start()
    {
        objectiveListNameText.text = "Objectives";
        UpdateObjectiveList();
    }

    void Update()
    {
        CheckObjectives();
        UpdateObjectiveList();
    }

    /**
     * Checks for each objective in the objectives array to see if the player has come within 1 unit of that object, 
     * signifying that the player has collected or completed that object. The objective's hasCompleted boolean will 
     * be set to true.
     */
    void CheckObjectives()
    {
        Transform player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        foreach(Objective o in objectives)
        {
            if(Vector2.Distance(o.objective.transform.position, player.position) < 1)
            {
                o.hasCompleted = true;
                if(o.objective.activeSelf)
                {
                    SoundManagerScript.PlaySound("money");
                }
                o.objective.SetActive(false);
            }
        }
    }

    /**
     * Sets the text value of the objectivesListText object to the display text properties of all of the uncompleted 
     * objectives in the objectives array. Once an objective has been marked as completed, it will be removed from 
     * the UI text.
     */
    void UpdateObjectiveList()
    {
        objectiveListText.text = "";
        foreach (Objective o in objectives)
        {
            if (!o.hasCompleted)
            {
                objectiveListText.text += (Environment.NewLine + o.displayText + "." + Environment.NewLine);
            }
        }
        if (objectiveListText.text == "")
        {
            objectiveListNameText.text = "All objectives complete!";
            //SoundManagerScript.PlaySound("money");
            levelComplete = true;
        }
    }
}
