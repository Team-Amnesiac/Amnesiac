using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Controller class for managing the display and interaction of collectible sets in the UI.
public class CollectibleSetEntryController : MonoBehaviour
{
    // Reference to the collectible set scriptable object being managed by this entry.
    [SerializeField] private CollectibleSetSO collectibleSet;
    // TextMeshProUGUI element to display the collectible set's text information (e.g., name and count).
    [SerializeField] private TextMeshProUGUI  collectibleTMP;
    // Array of buttons representing the collectible set items' sprites in the UI.
    [SerializeField] private Button[]         spriteButtons;

    // Method to assign a collectible set and update its displayed information.
    public void setCollectibleSet(CollectibleSetSO collectibleSet)
    {
        this.collectibleSet = collectibleSet; // Assign the passed collectible set to the internal variable.
        updateSetText(); // Update the displayed collectible set text and button states.
    }

    // Updates the collectible set's text and enables/disables buttons based on collected count.
    private void updateSetText()
    {
        CollectibleManager.Set setType = collectibleSet.getSetType(); 
        // Retrieve the set type (e.g., a specific collectible category).

        int collectedCount = CollectibleManager.Instance.getCollectedCount(setType); 
        // Get the number of collected items in the set from the CollectibleManager.

        collectibleTMP.text =
            $"{setType.ToString()}: {collectedCount} / {CollectibleManager.Instance.getSetSize(setType)}"; 
        // Update the UI text to show the collected count and total size of the set.

        foreach (Button button in spriteButtons) 
        // Iterate through the buttons representing collectible items.
        {
            if (collectedCount == 0) 
            // If no items are collected, stop enabling buttons.
            {
                return;
            }

            button.interactable = true; 
            // Enable interaction with the button to indicate the item is collected.

            collectedCount--; 
            // Decrease the collected count as we enable buttons for collected items.
        }
    }
}
