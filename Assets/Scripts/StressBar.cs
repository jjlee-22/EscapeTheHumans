using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StressBar : MonoBehaviour
{
	// stressBar is the slider object in Unity.
	public Slider stressBar;
    public Text lossText;
    public Button mainMenu;
    // startingStressLevel is public so it can be changed in the menus of Unity. Should be between 0 and 1.
    public float startingStressLevel;
    // stressLvlText is for if we want to add text on top of the stress bar
    //public Text stressLvlText;
    private float maxStress = 1f; // in case we want to have power-ups to alter this number.
	private float minStress = 0f; // this is just to make the code cleaner to read.
	//[SerializeField]
	// stressLevel represents a percentage as a float. 0 is no stress and 1 is full stress (lose game).
	private float stressLevel;
	public float StressLevel
	{
		get { return stressLevel; }
		set
		{
			stressLevel = value;
			stressBar.value = stressLevel;
			//stressLvlText.text = (stressLevel * 100).ToString("0.00") + "%";
		}
	}

	// Start is called before the first frame update
	void Start()
    {
        stressLevel = startingStressLevel;
   		stressBar.maxValue = maxStress;
        lossText.text = "";
        mainMenu.interactable = false;
        mainMenu.GetComponentInChildren<Text>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
		// -- TESTING PURPOSE --
		//stressLevel += 0.0043f;

        stressBar.value = stressLevel;
    }

    //LateUpdate called end of each frame
    private void LateUpdate()
    {
        checkStressLevel();
    }

	/**
     *  Takes a float value and adds it to the stress level. 
     *  This value is taken as a float ranging from 0 to maxStress (Default: 1).
     *  If the new value of the stress level is above or equal the maxStress
     *  (Default: 1), the player loses the game.
     */
	public void increaseStress(float amount)
	{
		float tempStress = stressLevel - amount;
		if (!(stressLevel >= maxStress) && !(tempStress >= maxStress))
		{
			stressLevel += amount;
		}
		else if (!(stressLevel >= maxStress) && (tempStress >= maxStress))
		{
			stressLevel = maxStress;
			loseGame();
		}
	}

    /**
     *  Takes an integer value and subtracts it to the stress level.
     *  If the new value would be less than the minimum of 0,
     *  then the stress level is set to 0.
     */
    public void decreaseStress(float amount)
    {
        float tempStress = stressLevel - amount;
        if(!(stressLevel < minStress) && !(tempStress < minStress))
        {
            stressLevel -= amount;
        }
        else if(!(stressLevel < minStress) && (tempStress < minStress))
        {
            stressLevel = minStress;
        }
    }

    /**
     *  Checks the current stress level and determines if it is within legal parameters.
     *  If the stress level is less than 0, the stress level is set to 0. If the stress 
     *  level is equal to or greater than the loss condition, you lose the game.
     */
    private void checkStressLevel()
    {
        if (stressLevel < minStress) // Prevent negative stress level
        {
			stressLevel = minStress;

		}
        else if (stressLevel >= maxStress)
        {
			loseGame();
        }
    }

    private void loseGame()
	{
        lossText.text = "Your stress level is too high!" + Environment.NewLine + "Maybe you should go rest.";
        GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
        mainMenu.interactable = true;
        mainMenu.GetComponentInChildren<Text>().text = "Main Menu";
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
