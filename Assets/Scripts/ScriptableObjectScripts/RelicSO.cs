using UnityEngine;

[CreateAssetMenu(fileName = "New RelicSO", menuName = "RelicSO/Create New RelicSO")]
// Allows creation of new instances of RelicSO from the Unity Editor.

// This ScriptableObject class represents a relic item in the game.
// It extends the base ItemSO class and includes additional properties specific to relics.

public class RelicSO : ItemSO
{
    // Reference to the quest associated with this relic.
    [SerializeField] private QuestSO relatedQuest;

    // Returns the quest associated with this relic.
    public QuestSO getRelatedQuest()
    {
        return relatedQuest;
    }
}