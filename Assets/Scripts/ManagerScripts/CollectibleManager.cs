// This class manages all collectible-related functionality, including tracking active and completed sets,
// handling collectible additions, and managing save/load states.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    // Singleton instance for global access.
    public static CollectibleManager Instance;

    // Enumeration representing collectible sets.
    public enum Set
    {
        Trophy, // Represents the Trophy collectible set.
    }

    // Enumeration representing the size of collectible sets.
    public enum SetSize
    {
        Trophy = 3, // The Trophy set contains 3 items.
    }

    // Counter for the number of trophies collected.
    private int trophyCount = 0;

    // Lists of active and completed collectible sets.
    [SerializeField] private List<CollectibleSetSO> activeSets;
    [SerializeField] private List<CollectibleSetSO> completedSets;

    // Ensures only one instance of the manager exists, persisting across scenes.
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Preserve the manager across scene loads.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    // Resets the collectible manager, clearing all sets and resetting counters.
    public void reset()
    {
        activeSets.Clear(); // Clear all active sets.
        completedSets.Clear(); // Clear all completed sets.
        trophyCount = 0; // Reset the trophy count.
    }

    // Adds a collectible to its respective set, updating progress and notifying the player.
    public void addCollectible(CollectibleSO collectible)
    {
        foreach (CollectibleSetSO collectibleSet in activeSets) // Iterate through active sets.
        {
            Set collectibleSetType = collectibleSet.getSetType();
            if (collectibleSetType == collectible.getSetType()) // Check for a matching set.
            {
                int collectibleCount = -1;
                int totalCount = -2;

                // Handle Trophy set progress.
                if (collectibleSetType == Set.Trophy)
                {
                    trophyCount++;
                    collectibleCount = trophyCount;
                    totalCount = (int)SetSize.Trophy;
                }

                // Check if the set is completed.
                if (collectibleCount == totalCount)
                {
                    UIManager.Instance.newNotification($"{collectibleSetType.ToString()} set complete! See the keeper."); // Notify player.
                    activeSets.Remove(collectibleSet); // Move the set to completed sets.
                    completedSets.Add(collectibleSet);
                }
                else
                {
                    // Notify player of progress.
                    UIManager.Instance.newNotification(
                        $"{collectibleSetType.ToString()} collected!: {collectibleCount} / {totalCount}");
                }

                return; // Exit after handling the collectible.
            }
        }
    }

    /* GET FUNCTIONS */

    // Returns the list of active collectible sets.
    public List<CollectibleSetSO> getActiveSets()
    {
        return activeSets;
    }

    // Returns the list of completed collectible sets.
    public List<CollectibleSetSO> getCompletedSets()
    {
        return completedSets;
    }

    // Returns the count of collected items for a specific set type.
    public int getCollectedCount(Set setType)
    {
        if (setType == Set.Trophy)
        {
            return trophyCount;
        }

        return -1; // Return -1 for invalid set types.
    }

    // Returns the size of a specific set type.
    public int getSetSize(Set setType)
    {
        if (setType == Set.Trophy)
        {
            return (int)SetSize.Trophy;
        }

        return -1; // Return -1 for invalid set types.
    }

    // Loads the collectible state from saved data.
    public void LoadState(CollectiblesData data)
    {
        trophyCount = data.trophyCount; // Load the trophy count.

        // Load active and completed sets by converting their names to CollectibleSetSO objects.
        activeSets = data.activeSets.ConvertAll(name => FindCollectibleSetByName(name));
        completedSets = data.completedSets.ConvertAll(name => FindCollectibleSetByName(name));
    }

    // Finds a collectible set by its name in the resources folder.
    private CollectibleSetSO FindCollectibleSetByName(string name)
    {
        return Resources.Load<CollectibleSetSO>($"Collectibles/{name}");
    }

    // Saves the current collectible state into a data object.
    public CollectiblesData SaveState()
    {
        return new CollectiblesData
        {
            trophyCount = trophyCount, // Save the trophy count.
            activeSets = activeSets.ConvertAll(set => set.name), // Save active set names.
            completedSets = completedSets.ConvertAll(set => set.name) // Save completed set names.
        };
    }
}
