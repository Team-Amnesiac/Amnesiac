using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

// This class manages the Worlds UI, allowing the player to travel between different worlds (scenes) 
// and restricts access to certain worlds based on game progress or the current location.

public class WorldsUI : MonoBehaviour
{
    // Buttons for traveling to specific worlds.
    [SerializeField] private Button hubButton;    // Button to travel to the Hub world.
    [SerializeField] private Button noryxButton;  // Button to travel to the Noryx world.
    [SerializeField] private Button loikartButton; // Button to travel to the Loikart world.
    [SerializeField] private Button exitButton;   // Button to exit the Worlds UI and return to the Checkpoint UI.

    void Update()
    {
        toggleAppropriateWorldButtons();
    }

    // Initializes the Worlds UI, attaches button listeners, and disables buttons for unavailable worlds.
    private void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Worlds, this); // Register the Worlds UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Worlds); // Hide the Worlds UI initially.

        toggleAppropriateWorldButtons(); // Disable buttons based on the current scene and quest progress.

        // Attach listeners to button click events.
        hubButton.onClick.AddListener(hubButtonOnClick);
        noryxButton.onClick.AddListener(noryxButtonOnClick);
        loikartButton.onClick.AddListener(loikartButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    // Disables a button.
    private void DisableButton(Button button)
    {
        button.interactable = false; // Make the button non-interactable.
    }

    // Enables a button.
    private void enableButton(Button button)
    {
        button.interactable = true;  // Make the button interactable.
    }

    // Handles the Hub button click, loading the Hub scene.
    private void hubButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Hub); // Load the Hub scene.
    }

    // Handles the Noryx button click, loading the Noryx scene.
    private void noryxButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Noryx); // Load the Noryx scene.
    }

    // Handles the Loikart button click, loading the Loikart scene.
    private void loikartButtonOnClick()
    {
        SceneLoader.Instance.loadScene(SceneLoader.Scene.Loikart); // Load the Loikart scene.
    }

    // Handles the Exit button click, returning to the Checkpoint UI.
    private void exitButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Worlds); // Hide the Worlds UI.
        UIManager.Instance.showUI(UIManager.UI.Checkpoint); // Show the Checkpoint UI.
    }

    // Disables buttons for worlds that are unavailable based on the current scene or quest progress.
    private void toggleAppropriateWorldButtons()
    {
        string currentScene = SceneManager.GetActiveScene().name; // Get the name of the current scene.

        if (currentScene == "Hub")
        {
            DisableButton(hubButton); // Disable the Hub button if the player is already in the Hub world.
        }
        else
        {
            enableButton(hubButton);
        }

        // Disable the Noryx button if the player is already in Noryx or hasn't unlocked the quest for Noryx.
        if (currentScene == "Noryx" ||
            (!QuestManager.Instance.getActiveQuests().Contains(QuestManager.Instance.SecondQuest) &&
             !QuestManager.Instance.getCompletedQuests().Contains(QuestManager.Instance.SecondQuest)))
        {
            DisableButton(noryxButton);
        }
        else
        {
            enableButton(noryxButton);
        }

        // Disable the Loikart button if the player is already in Loikart or hasn't unlocked the quest for Loikart.
        if (currentScene == "Loikart" ||
            (!QuestManager.Instance.getActiveQuests().Contains(QuestManager.Instance.ThirdQuest) &&
             !QuestManager.Instance.getCompletedQuests().Contains(QuestManager.Instance.ThirdQuest)))
        {
            DisableButton(loikartButton);
        }
        else
        {
            enableButton(loikartButton);
        }
    }
}
