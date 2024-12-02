using System.Collections.Generic;
// Mark the class as serializable so it can be converted to and from JSON or other data formats.
[System.Serializable]

// For Saving Collectibles Data
public class CollectiblesData
{
    // An integer variable to store the total number of trophies collected by the player.
    public int trophyCount;

    // A list of strings to store the names of collectible sets that are currently active (not yet completed).
    public List<string> activeSets;

    // A list of strings to store the names of collectible sets that the player has completed.
    public List<string> completedSets;
}
