// This class manages the player's inventory, including adding and removing items,
// equipping skill cards, and saving/loading inventory states.

using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Singleton instance for global access.
    public static InventoryManager Instance;

    // List to store the player's inventory items.
    private List<ItemSO> inventoryItems = new List<ItemSO>();

    // Initializes the singleton instance.
    private void Awake()
    {
        Instance = this;
    }

    // Resets the inventory by clearing all items.
    public void reset()
    {
        inventoryItems.Clear(); // Clear all inventory items.
    }

    // Adds an item to the inventory and handles special logic for skill cards.
    public void addItem(ItemSO item)
    {
        inventoryItems.Add(item); // Add the item to the inventory.

        if (item.getItemType() == ItemSO.ItemType.SkillCard) // Check if the item is a skill card.
        {
            SkillCardSO skillCard = (SkillCardSO)item;

            if (PlayerManager.Instance.hasAvailableSkillCardSlot()) // Check for available skill card slots.
            {
                // Equip the skill card to the first available slot.
                PlayerManager.Instance.equipSkillCard(skillCard);
            }
            else
            {
                skillCard.setEquipped(false); // Mark the skill card as unequipped.
            }
        }
    }

    // Removes an item from the inventory, logging errors or warnings as needed.
    public void removeItem(ItemSO itemSo)
    {
        if (itemSo == null) // Check if the item reference is null.
        {
            Debug.LogError("Cannot remove item: item reference is null!"); // Log an error message.
            return;
        }

        if (inventoryItems.Contains(itemSo)) // Check if the item exists in the inventory.
        {
            inventoryItems.Remove(itemSo); // Remove the item.
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {itemSo.getItemName()}"); // Log a warning for missing items.
        }
    }

    // Returns the list of items currently in the inventory.
    public List<ItemSO> getInventoryItems()
    {
        return inventoryItems;
    }

    // Saves the current inventory state into a data object.
    public InventoryData SaveState()
    {
        return new InventoryData
        {
            items = inventoryItems.ConvertAll(item => item.getItemName()) // Save item names.
        };
    }

    // Loads inventory state from saved data.
    public void LoadState(InventoryData data)
    {
        inventoryItems = data.items.ConvertAll(name => FindItemByName(name)); // Load items by name.
    }

    // Finds an item by its name in the Resources folder.
    private ItemSO FindItemByName(string name)
    {
        return Resources.Load<ItemSO>($"Items/{name}"); // Locate the ItemSO asset by name.
    }
}
