using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    private static int levelCounter = 0; // Static counter to track the level selected

    public static int LevelCounter
    {
        get { return levelCounter; }
    }

    // Method to load a scene based on the button's name or string parameter
    public void LoadScene(string sceneName)
    {
        string buttonName = gameObject.name; // Get the name of the button clicked
        Debug.Log("Button clicked: " + buttonName); // Debug log to check the button's name

        // If the button is a level button, set the levelCounter and load the corresponding level
        if (buttonName == "Level1Button")
        {
            levelCounter = 1; // Set levelCounter for Level 1
            SceneManager.LoadScene("Level 1");
        }
        else if (buttonName == "Level2Button")
        {
            levelCounter = 2; // Set levelCounter for Level 2
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            // For non-level buttons, load the scene specified by the string parameter
            if (!string.IsNullOrEmpty(sceneName))
            {
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                Debug.LogError("Scene name is empty. Please specify a valid scene name.");
            }
        }
    }

    // Method to load the correct level when pressing Play Again
    public void PlayAgain()
    {
        if (levelCounter == 1)
        {
            SceneManager.LoadScene("Level 1");
        }
        else if (levelCounter == 2)
        {
            SceneManager.LoadScene("Level 2");
        }
        else
        {
            Debug.LogError("No level selected. Please select a valid level before restarting.");
        }
    }

    public void ResetBestTimeAndLoadMenu(string mainMenuSceneName)
    {
        // Generate the unique key for the current level's best time
        string bestTimeKey = "BestTime_Level" + levelCounter;

        if (PlayerPrefs.HasKey(bestTimeKey))
        {
            PlayerPrefs.DeleteKey(bestTimeKey);
            PlayerPrefs.Save(); // Save changes to PlayerPrefs
            Debug.Log("Best time reset for level: " + levelCounter);
        }

        // Load the main menu scene
        SceneManager.LoadScene(mainMenuSceneName);
    }
}
