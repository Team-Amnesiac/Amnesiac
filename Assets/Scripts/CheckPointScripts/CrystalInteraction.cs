using UnityEngine; 
// Crystal Interaction That is used for interacting with the world crystal

public class CrystalInteraction : MonoBehaviour
{
    // A private boolean variable to track whether the player is near the crystal or not.
    private bool isPlayerNearby = false;



    private bool hasPromptShown = false;

    private void Update()
    {
        // Check if the player is nearby and the prompt hasn't been shown yet.
        if (isPlayerNearby && !hasPromptShown)
        {
            UIManager.Instance.newPrompt(this.gameObject, $"Press E to interact with the World Crystal.");
            hasPromptShown = true; // Mark the prompt as shown.
        }

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Show the Checkpoint UI and pause the game only when the player interacts.
            UIManager.Instance.showUI(UIManager.UI.Checkpoint);
            GameManager.Instance.setGameState(GameManager.GameState.Pause);
        }

        // Reset the prompt state if the player is no longer nearby.
        if (!isPlayerNearby && hasPromptShown)
        {
            hasPromptShown = false; // Allow the prompt to be shown again when the player re-enters the vicinity.
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Unity's OnTriggerEnter method is called when another collider enters the trigger collider attached to this object.

        if (other.CompareTag("Player"))
        {
            // Check if the object entering the trigger zone has the tag "Player".
            
            isPlayerNearby = true;
            // Set `isPlayerNearby` to true, indicating the player is within range.
            
            Debug.Log("Player is near the world crystal. Press 'E' to interact.");
            // Log a message to the console indicating that the player is near the crystal.
        }
    }


    private void OnTriggerExit(Collider other)
    {
        // Unity's OnTriggerExit method is called when another collider exits the trigger collider attached to this object.

        if (other.CompareTag("Player"))
        {
            // Check if the object exiting the trigger zone has the tag "Player".
            
            isPlayerNearby = false;
            // Set `isPlayerNearby` to false, indicating the player is no longer within range.
            
            Debug.Log("Player left the world crystal.");
            // Log a message to the console indicating that the player has left the crystal's range.
        }
    }
}
