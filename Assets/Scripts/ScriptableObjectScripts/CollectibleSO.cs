using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CollectibleSO", menuName = "Collectible/Create New CollectibleSO")]
// Allows creation of new instances of CollectibleSO from the Unity Editor.

// This ScriptableObject class represents an individual collectible item in the game.
// It extends the ItemSO base class and includes additional properties related to quests and collectible sets.

public class CollectibleSO : ItemSO
{
    // Reference to a related quest, if this collectible is tied to one.
    [SerializeField] private QuestSO relatedQuest;
    // The collectible set to which this collectible belongs.
    [SerializeField] private CollectibleManager.Set collectibleSet;

    // Returns the collectible set type for this item.
    public CollectibleManager.Set getSetType()
    {
        return collectibleSet;
    }

    // Returns the quest associated with this collectible, if any.
    public QuestSO getRelatedQuest()
    {
        return relatedQuest;
    }
}
