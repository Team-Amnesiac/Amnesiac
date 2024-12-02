using System.Collections.Generic;

// Makes the class serializable for saving and loading data.
[System.Serializable]

// For Saving Quest Related Data
public class QuestData
{
    // A list of quest names or IDs that are currently active for the player.
    public List<string> activeQuests;

    // A list of quest names or IDs that the player has completed.
    public List<string> completedQuests;
}