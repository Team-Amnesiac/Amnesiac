using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New SkillCardSO", menuName = "SkillCardSO/Create New SkillCardSO")]
// Allows creation of new instances of SkillCardSO from the Unity Editor.

// This ScriptableObject class represents a skill card item in the game.
// It extends the base ItemSO class and includes properties and methods specific to skill cards.

public class SkillCardSO : ItemSO
{
    // Enumeration of possible attack types for the skill card.
    public enum AttackType
    {
        Earth, // Earth-based attack type.
        Fire,  // Fire-based attack type.
        Water, // Water-based attack type.
        Wind,  // Wind-based attack type.
        None,  // No specific attack type.
    }

    // The amount of damage dealt by the skill card.
    [SerializeField] private float damage;
    // The stamina cost to use this skill card.
    [SerializeField] private int staminaCost;
    // The attack type associated with this skill card.
    [SerializeField] private AttackType attackType;

    // Tracks whether the skill card is currently equipped.
    private bool equipped = false;

    // Returns the damage value of the skill card.
    public float getDamage()
    {
        return damage;
    }

    // Returns the stamina cost of the skill card.
    public int getStaminaCost()
    {
        return staminaCost;
    }

    // Returns the attack type of the skill card.
    public AttackType getAttackType()
    {
        return attackType;
    }

    // Returns whether the skill card is currently equipped.
    public bool isEquipped()
    {
        return equipped;
    }

    // Sets the equipped status of the skill card.
    public void setEquipped(bool equipped)
    {
        this.equipped = equipped;
    }
}
