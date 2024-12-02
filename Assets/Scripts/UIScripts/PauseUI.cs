// This class manages the Pause Menu UI, allowing the player to continue, load the game, or exit to the main menu.

using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour
{
    // Buttons for interacting with the Pause Menu.
    [SerializeField] private Button continueButton; // Button to resume gameplay.
    [SerializeField] private Button loadButton;     // Button to load a saved game.
    [SerializeField] private Button exitButton;     // Button to exit to the main menu.

    // Initializes the Pause Menu UI and attaches button listeners.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.PauseMenu, this); // Register the Pause Menu UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu); // Hide the Pause Menu UI initially.

        // Attach listeners to button click events.
        continueButton.onClick.AddListener(continueButtonOnClick);
        loadButton.onClick.AddListener(loadButtonOnClick);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }

    // Quits the game when called.
    public void QuitGame()
    {
        Debug.Log("Quitting the game..."); // Log the quit action.
        Application.Quit(); // Close the application.
    }

    // Initiates the process to load a saved game.
    public void LoadGame()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading); // Set the game state to "Loading."
    }

    // Handles the Continue button click, resuming gameplay.
    private void continueButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play); // Resume the game state to "Play."
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu); // Hide the Pause Menu UI.
    }

    // Handles the Load button click, initiating the load process.
    private void loadButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Loading); // Set the game state to "Loading."
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu); // Hide the Pause Menu UI.
    }

    // Handles the Exit button click, returning to the main menu.
    private void exitButtonOnClick()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Title); // Set the game state to "Title."
        UIManager.Instance.hideUI(UIManager.UI.PauseMenu); // Hide the Pause Menu UI.
    }
}
