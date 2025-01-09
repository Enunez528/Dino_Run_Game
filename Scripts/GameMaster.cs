using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public GameObject baloonRef;  // Reference to the balloon prefab
    public int baloonNr;  // Number of balloons to spawn
    public GameObject door; // Reference to the door GameObject
    public Text messageText; // Reference to a UI Text element for messages

    public float spawnAreaWidth = 10f;  // The width of the area where balloons can spawn
    public float spawnAreaHeight = 10f; // The depth of the area where balloons can spawn
    public float spawnAreaY = 0f;      // The Y position at which balloons will spawn (you can adjust this for different heights)

    private int remainingBalloons;  // Track the remaining balloons in the scene
    private List<GameObject> spawnedBalloons; // List to keep track of spawned balloons

    // Add the given number of balloons randomly within the spawn area
    void AddBalloons()
    {
        spawnedBalloons = new List<GameObject>(); // Initialize the list

        for (int i = 0; i < baloonNr; i++)
        {
            GameObject balloon = Instantiate(baloonRef);

            // Randomly choose a position for the balloon within the defined spawn area
            float fx = Random.Range(-spawnAreaWidth / 2, spawnAreaWidth / 2);
            float fz = Random.Range(-spawnAreaHeight / 2, spawnAreaHeight / 2);

            Vector3 pos = new Vector3(fx, spawnAreaY, fz);  // Set balloon position in world space
            balloon.transform.position = pos;  // Apply position to the instantiated balloon

            // Add the balloon to the list and increase the count
            spawnedBalloons.Add(balloon);
        }

        remainingBalloons = baloonNr; // Set the initial number of remaining balloons
        ShowMessage($"Eliminate all {baloonNr} balloons to unlock the door!", 3f);
    }

    // This method should be called whenever a balloon is popped
    public void OnBalloonPopped()
    {
        remainingBalloons--;

        if (AreAllBalloonsPopped())
        {
            EnableDoor();  // Enable the door once all balloons are popped
            ShowMessage("All balloons eliminated! Door is now unlocked.", 3f);
        }
        else
        {
            ShowMessage($"Balloons remaining: {remainingBalloons}. Keep going!", 3f);
        }
    }

    // Function to check if all balloons are popped
    public bool AreAllBalloonsPopped()
    {
        return remainingBalloons <= 0;  // Return true if no balloons remain
    }

    // This method will enable the door, making it interactable
    private void EnableDoor()
    {
        if (door != null)
        {
            door.GetComponent<Collider>().enabled = true;
            Debug.Log("All balloons eliminated! Door is now interactable.");
        }
    }

    // Coroutine to display a message for a few seconds and then hide it
    private IEnumerator HideMessageAfterDelay(float duration)
    {
        yield return new WaitForSeconds(duration);  // Wait for the specified duration
        if (messageText != null)
        {
            messageText.text = "";  // Clear the message
        }
    }

    // Show a message on the screen for a specific duration
    private void ShowMessage(string message, float duration)
    {
        if (messageText != null)
        {
            messageText.text = message;
            StartCoroutine(HideMessageAfterDelay(duration));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        AddBalloons();  // Add the balloons when the game starts
        if (door != null)
        {
            door.GetComponent<Collider>().enabled = false; // Disable the door initially
        }
    }
}
