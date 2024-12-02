// This class manages the Collectibles UI, displaying active and completed collectible sets,
// and handling transitions in and out of the collectibles menu.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    // Prefab used to create entries for collectible sets.
    [SerializeField] private GameObject trophySetEntryPrefab;
    // Content container for active collectible sets.
    [SerializeField] private GameObject activeSetContent;
    // Content container for completed collectible sets.
    [SerializeField] private GameObject completedSetContent;
    // Button for exiting the Collectibles UI.
    [SerializeField] private Button exitButton;

    // Initializes the Collectibles UI and attaches the exit button listener.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Collectibles, this); // Register the Collectibles UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Collectibles); // Hide the Collectibles UI initially.
        exitButton.onClick.AddListener(exitButtonOnClick); // Attach the listener for the exit button.
    }

    // Prepares the Collectibles UI for display by showing active and completed sets.
    public void prepareCollectiblesShow()
    {
        prepareActiveCollectiblesShow(); // Populate the active collectibles section.
        prepareCompletedCollectiblesShow(); // Populate the completed collectibles section.
    }

    // Cleans up the Collectibles UI by hiding active and completed sets.
    public void prepareCollectiblesHide()
    {
        prepareActiveCollectiblesHide(); // Clear the active collectibles section.
        prepareCompletedCollectiblesHide(); // Clear the completed collectibles section.
    }

    // Populates the active collectibles section with active trophy sets.
    private void prepareActiveCollectiblesShow()
    {
        foreach (CollectibleSetSO activeSet in CollectibleManager.Instance.getActiveSets()) // Iterate through active sets.
        {
            if (activeSet.getSetType() == CollectibleManager.Set.Trophy) // Check if the set is a trophy set.
            {
                GameObject obj = Instantiate(trophySetEntryPrefab, activeSetContent.transform); // Create a new entry.
                CollectibleSetEntryController controller = obj.GetComponent<CollectibleSetEntryController>();

                controller.setCollectibleSet(activeSet); // Assign the active set to the entry.
            }
            else
            {
                Debug.Log("INVALID COLLECTIBLE SET IN ACTIVE SET"); // Log an error for invalid sets.
            }
        }
    }

    // Populates the completed collectibles section with completed trophy sets.
    private void prepareCompletedCollectiblesShow()
    {
        foreach (CollectibleSetSO completedSet in CollectibleManager.Instance.getCompletedSets()) // Iterate through completed sets.
        {
            if (completedSet.getSetType() == CollectibleManager.Set.Trophy) // Check if the set is a trophy set.
            {
                GameObject obj = Instantiate(trophySetEntryPrefab, completedSetContent.transform); // Create a new entry.
                CollectibleSetEntryController controller = obj.GetComponent<CollectibleSetEntryController>();

                controller.setCollectibleSet(completedSet); // Assign the completed set to the entry.
            }
            else
            {
                Debug.Log("INVALID COLLECTIBLE SET IN ACTIVE SET"); // Log an error for invalid sets.
            }
        }
    }

    // Clears the active collectibles section by destroying its children.
    private void prepareActiveCollectiblesHide()
    {
        foreach (Transform child in activeSetContent.transform) // Iterate through all child objects.
        {
            Destroy(child.gameObject); // Destroy each child.
        }
    }

    // Clears the completed collectibles section by destroying its children.
    private void prepareCompletedCollectiblesHide()
    {
        foreach (Transform child in completedSetContent.transform) // Iterate through all child objects.
        {
            Destroy(child.gameObject); // Destroy each child.
        }
    }

    // Handles the exit button click, returning to the Keeper UI.
    private void exitButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Collectibles); // Hide the Collectibles UI.
        UIManager.Instance.showUI(UIManager.UI.Keeper); // Show the Keeper UI.
    }
}
