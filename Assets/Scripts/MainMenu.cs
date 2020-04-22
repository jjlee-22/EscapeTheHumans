using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string Url = "https://adaa.org/understanding-anxiety/social-anxiety-disorder";

    // Loads the desired game scene.
    public void PlayGame()
    {
        // This will load the next scene in the build order. This can be changed to load a specific scene.
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

        SceneManager.LoadScene("Level_1");
    }

    // Loads the scene containing instructions on how to play the game.
    public void HowToPlayGame()
    {
        // This loads a scene called "HowToPlay" which contains instructions.
        //TODO: Correct this name
        SceneManager.LoadScene("HowToPlay");
    }

    // Loads the ADAA Page on Social Anxiety.
    public void AnxietyInfo()
    {
        Application.OpenURL(Url);
    }

    // Rage Quit
    public void ExitGame()
    {
        Application.Quit();
    }
}
