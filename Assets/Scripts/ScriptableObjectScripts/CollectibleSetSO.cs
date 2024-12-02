using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New CollectibleSetSO", menuName = "CollectibleSet/Create New CollectibleSetSO")]
// Allows creation of new instances of CollectibleSetSO from the Unity Editor.

// This CollectibleSet Scriptable Object class represents a set of collectibles in the game.
// It is used to define and manage collections of items that belong to a specific set type.

public class CollectibleSetSO : ScriptableObject
{
    // The type of collectible set, defined in the CollectibleManager.
    [SerializeField] private CollectibleManager.Set setType;
    // Array of collectible items that belong to this set.
    [SerializeField] private CollectibleSO[] collectiblsInSet;

    // Returns the type of collectible set (e.g., a specific category of collectibles).
    public CollectibleManager.Set getSetType()
    {
        return setType;
    }
}
