using UnityEngine;

[CreateAssetMenu(fileName = "New ItemSO", menuName = "ItemSO/Create New ItemSO")]
// Allows creation of new instances of ItemSO from the Unity Editor.

// This ScriptableObject class defines the base properties and behavior for items in the game.
// It includes functionality for using items and retrieving their attributes.
public class ItemSO : ScriptableObject
{
    // Unique identifier for the item.
    [SerializeField] private int id;
    // Name of the item.
    [SerializeField] private string itemName;
    // Value of the item (e.g., healing amount, stamina boost, etc.).
    [SerializeField] private int value;
    // Sprite used to visually represent the item.
    [SerializeField] private Sprite sprite;
    // Type of the item, as defined in the ItemType enum.
    [SerializeField] private ItemType itemType;

    // Enumeration of possible item types.
    public enum ItemType
    {
        HealthGem,   // Increases player's health.
        StaminaGem,  // Increases player's stamina.
        Equipment,   // Represents an equipable item (not implemented in this class).
        SkillCard,   // Represents a skill card that can be equipped or unequipped.
        Collectible, // Represents a collectible item.
        Relic,       // Represents a relic item.
    }

    // Defines how the item is used in the game.
    public void use()
    {
        switch (itemType)
        {
            case ItemType.HealthGem: // If the item is a HealthGem.
                PlayerManager.Instance.increaseHealth(value); // Increase the player's health by the item's value.
                break;
            
            case ItemType.StaminaGem: // If the item is a StaminaGem.
                PlayerManager.Instance.increaseStamina(value); // Increase the player's stamina by the item's value.
                break;

            case ItemType.SkillCard: // If the item is a SkillCard.
                SkillCardSO skillCard = (SkillCardSO)this; // Cast the item to a SkillCardSO.

                if (skillCard.isEquipped()) // Check if the skill card is equipped.
                {
                    PlayerManager.Instance.unequipSkillCard(skillCard); // Unequip the skill card.
                }
                else // If the skill card is not equipped.
                {
                    PlayerManager.Instance.equipSkillCard(skillCard); // Equip the skill card.
                }
                
                break;

            // Other item types (e.g., Collectible, Relic) do not define specific behavior in this method.
        }
    }

    // Returns the name of the item.
    public string getItemName()
    {
        return itemName;
    }

    // Returns the value of the item.
    public int getValue()
    {
        return value;
    }

    // Returns the sprite representing the item.
    public Sprite getItemSprite()
    {
        return sprite;
    }

    // Returns the type of the item.
    public ItemType getItemType()
    {
        return itemType;
    }
}
