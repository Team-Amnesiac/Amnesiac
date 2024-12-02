using UnityEngine;

[CreateAssetMenu(fileName = "New QuestSO", menuName = "QuestSO/QuestSO")]
// Allows creation of new instances of QuestSO from the Unity Editor.

// This ScriptableObject class defines the structure and attributes of a quest in the game.
// It includes basic information, quest type, and specific requirements for completing the quest.
public class QuestSO : ScriptableObject
{
    [Header("Basic Info")]
    // The name of the quest.
    public string questName;
    // A detailed description of the quest.
    public string description;
    // Tracks whether the quest is completed.
    public bool isCompleted;

    [Header("Quest Type")]
    // The type of the quest (e.g., Main or Side quest).
    public QuestType questType;

    [Header("Quest Requirements")]
    // The name of the target item required to complete the quest (e.g., a relic for collection quests).
    public string targetItem;
    // The number of items required to complete the quest.
    public int requiredAmount;

    // Enumeration of quest types.
    public enum QuestType
    {
        Main, // Represents a main quest essential to the game's storyline.
        Side  // Represents a side quest that is optional or provides additional rewards.
    }
}
