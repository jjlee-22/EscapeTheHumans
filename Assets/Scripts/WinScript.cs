using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    public ObjectiveManager objectiveManager;
    public Text winText;
    public Button mainMenu;

    // Start is called before the first frame update
    void Start()
    {
        winText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        CheckWin();
    }

    private void CheckWin()
    {
        if (objectiveManager.levelComplete)
        {
            winText.text = "Congratulations!" + Environment.NewLine + "You made it to class while avoiding people!";
            GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>().enabled = false;
            mainMenu.interactable = true;
            mainMenu.GetComponentInChildren<Text>().text = "Main Menu";
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
