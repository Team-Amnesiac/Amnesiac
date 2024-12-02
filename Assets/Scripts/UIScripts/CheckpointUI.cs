using UnityEngine;
using UnityEngine.UI;

// This class manages the Checkpoint UI, which provides options to save the game, load a saved state, switch worlds, or exit the menu.

public class CheckpointUI : MonoBehaviour
{
    // Buttons for interacting with the Checkpoint UI.
    [SerializeField] private Button saveButton;   // Button for saving the game.
    [SerializeField] private Button loadButton;   // Button for loading a saved state.
    [SerializeField] private Button worldsButton; // Button for navigating to the Worlds UI.
    [SerializeField] private Button exitButton;   // Button for exiting the Checkpoint UI.

    // Start method, initializes button listeners and hides the UI initially.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Checkpoint, this); // Register the Checkpoint UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint); // Hide the Checkpoint UI initially.
        
        // Attach listeners to button click events.
        saveButton.onClick.AddListener(saveButtonOnClick); 
        loadButton.onClick.AddListener(loadButtonOnClick);
        worldsButton.onClick.AddListener(worldsButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    // Saves the current game state.
    public void saveButtonOnClick()
    {
        SaveSystem.SaveGame(); // Call the SaveSystem to save the game.
        Debug.Log("Game saved successfully!"); // Log the success message.
    }

    // Loads the last saved game state.
    public void loadButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading); // Set the game state to "Loading."
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint); // Hide the Checkpoint UI.
        Debug.Log("Game loaded successfully!"); // Log the success message.
    }

    // Opens the Worlds UI for navigating between worlds.
    public void worldsButtonOnClick()
    {
        UIManager.Instance.showUI(UIManager.UI.Worlds); // Show the Worlds UI.
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint); // Hide the Checkpoint UI.
    }

    // Exits the Checkpoint UI and resumes the game.
    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play); // Resume the game state to "Play."
        UIManager.Instance.hideUI(UIManager.UI.Checkpoint); // Hide the Checkpoint UI.
    }
}
