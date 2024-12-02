using UnityEngine; 
// Crystal Interaction That is used for interacting with the world crystal

public class CrystalInteraction : MonoBehaviour
{
    // A private boolean variable to track whether the player is near the crystal or not.
    private bool isPlayerNearby = false;

    private void Update()
    {
        // Unity's Update method is called once per frame.

        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            // Check if the player is nearby (`isPlayerNearby` is true) and the 'E' key was pressed this frame.
            
            UIManager.Instance.showUI(UIManager.UI.Checkpoint);
            // Show the Checkpoint UI using the UIManager instance.
            
            GameManager.Instance.setGameState(GameManager.GameState.Pause);
            // Pause the game by setting the game state to Pause using GameManager.
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
