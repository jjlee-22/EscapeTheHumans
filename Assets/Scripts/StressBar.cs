using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StressBar : MonoBehaviour
{
    // startingStressLevel is public so it can be changed in the menus of Unity.
    public int startingStressLevel;
    // this is a placeholder until we have a UI bar or something for stress.
    public Text stressLvlText;
    // stressLevel represents a percentage. 0 is no stress and 100 is full stress (lose game).
    private int stressLevel;
    private int maxStress = 100; // in case we want to have power-ups to alter this number.

    // Start is called before the first frame update
    void Start()
    {
        stressLevel = startingStressLevel;
    }

    // Update is called once per frame
    void Update()
    {
        checkStressLevel();
    }

    private void LateUpdate()
    {
        checkStressLevel();
        // Adjust the placeholder text. Replace this when we have a UI.
        stressLvlText.text = stressLevel.ToString() + "%";
    }

    /**
     *  Takes an integer value and adds it to the stress level.
     */
    public void increaseStress(int amount)
    {
        if(!(stressLevel >= maxStress))
        {
            stressLevel += amount;
        }
    }

    /**
     *  Takes an integer value and subtracts it to the stress level.
     *  If the new value would be less than the minimum of 0,
     *  then the stress level is set to 0.
     */
    public void decreaseStress(int amount)
    {
        int tempStress = stressLevel - amount;
        if(!(stressLevel < 0) && !(tempStress < 0))
        {
            stressLevel -= amount;
        }
        else if(!(stressLevel <0) && (tempStress < 0))
        {
            stressLevel = 0;
        }
    }

    /**
     *  Checks the current stress level and determines if it is within legal parameters.
     *  If the stress level is less than 0, the stress level is set to 0. If the stress 
     *  level is equal to or greater than the loss condition, you lose the game.
     */
    private void checkStressLevel()
    {
        if (stressLevel < 0) // Prevent negative stress level
        {
            stressLevel = 0;
        }
        else if (stressLevel >= maxStress)
        {
            // Game loss functions
        }
    }
}
