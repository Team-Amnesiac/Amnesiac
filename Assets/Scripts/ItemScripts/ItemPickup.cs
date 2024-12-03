using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    // The item scriptable object associated with this pickup.
    public ItemSO item;
    // Tracks whether the player is near the item.
    private bool playerNearby = false;
    // Reference to the player's animator for triggering pickup animations.
    private Animator playerAnimator;
    private bool hasPromptShown = false; // Tracks whether the prompt has already been displayed.

    // Start method, initializes item behavior and checks if it should spawn.
    void Start()
    {
        ItemSO.ItemType itemType = item.getItemType(); // Get the type of the item.
        if (itemObjectShouldNotSpawn()) // Check if the item should not appear in the game world.
        {
            gameObject.SetActive(false); // Deactivate the item if it shouldn't spawn.
        }
    }

    // Update method, checks for player interaction with the item.
    void Update()
    {
        if (playerNearby && !hasPromptShown) // If the player is near the item and the prompt hasn't been shown.
        {
            UIManager.Instance.newPrompt(this.gameObject, $"Press E to pick up the item."); // Show a prompt to pick up the item.
            hasPromptShown = true; // Mark the prompt as shown.
        }

        if (playerNearby && Input.GetKeyDown(KeyCode.E)) // If the player presses the 'E' key while nearby.
        {
            Debug.Log("E key pressed, attempting to pick up item."); // Log the action.
            pickup(); // Attempt to pick up the item.
            hasPromptShown = false; // Reset the prompt so it can reappear for other items or scenarios if needed.
        }

        if (!playerNearby && hasPromptShown) // If the player moves away and the prompt was shown.
        {
            hasPromptShown = false; // Reset the prompt state, allowing it to reappear when re-entering the vicinity.
        }
    }


    // Handles the logic for picking up the item.
    void pickup()
    {
        if (playerAnimator != null) // If the player's animator is set.
        {
            Debug.Log("Triggering gather animation."); // Log the animation trigger.
            playerAnimator.SetTrigger("Gather"); // Trigger the player's gather animation.
        }

        Debug.Log($"Picking up item: {item.name}"); // Log the item's name being picked up.
        ItemSO.ItemType itemType = item.getItemType(); // Get the item's type.

        if (itemType == ItemSO.ItemType.Relic) // If the item is a relic.
        {
            QuestManager.Instance.addRelic((RelicSO)item); // Add the relic to the quest manager.
        }
        else if (itemType == ItemSO.ItemType.Collectible) // If the item is a collectible.
        {
            CollectibleManager.Instance.addCollectible((CollectibleSO)item); // Add the collectible to the collectible manager.
            QuestManager.Instance.addCollectible((CollectibleSO)item); // Add the collectible to the quest manager.
        }
        else // If the item is not a collectible or a relic.
        {
            InventoryManager.Instance.addItem(item); // Add the item to the inventory.
        }

        Destroy(gameObject); // Remove the item from the game world after pickup.
    }

    // Sets whether the player is near the item.
    public void setPlayerNearby(bool playerNearby)
    {
        this.playerNearby = playerNearby; // Update the nearby status.
    }

    // Sets the player's animator for triggering animations.
    public void setPlayerAnimator(Animator playerAnimator)
    {
        this.playerAnimator = playerAnimator; // Update the animator reference.
    }

    // Checks whether the item object should spawn in the game world.
    private bool itemObjectShouldNotSpawn()
    {
        ItemSO.ItemType itemType = item.getItemType(); // Get the item's type.
        return (itemType == ItemSO.ItemType.SkillCard || // Check if the item is a skill card.
                itemType == ItemSO.ItemType.Collectible || // Check if the item is a collectible.
                itemType == ItemSO.ItemType.Relic) && // Check if the item is a relic.
               InventoryManager.Instance.getInventoryItems().Contains(item);
        // Ensure the item is not already in the inventory.
    }
}