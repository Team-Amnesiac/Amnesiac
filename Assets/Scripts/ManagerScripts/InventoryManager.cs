using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Image = UnityEngine.UIElements.Image;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;


    [SerializeField] private GameObject inventoryItemPrefab;

    public List<Item>   inventoryItems = new List<Item>();
    
    // The amount of set pieces collected in each collectible set.
    private int trophyCount = 0;


    private void Awake()
    {
        Instance = this;
    }


    public void Add(Item item)
    {
        inventoryItems.Add(item);

        switch (item.GetItemType())
        {
            case Item.ItemType.Collectible:
                Collectible collectible = (Collectible)item;

                // Identify which set this collectible belongs to
                switch (collectible.GetSet())
                {
                    case Collectible.Set.Trophy:
                        trophyCount++;
                        if (trophyCount == (int)Collectible.SetSize.Trophy)  // Trophy set complete
                        {
                            Debug.Log("Trophy set complete!");
                        }
                        break;

                    default:
                        Debug.Log("Unknown collectible set complete!");
                        break;
                }
                break;

            case Item.ItemType.SkillCard:
                if (PlayerManager.Instance.HasAvailableSkillCardSlot())  // There is an empty slot available
                {
                    PlayerManager.Instance.Equip((SkillCard)item);
                }
                break;

            case Item.ItemType.Potion:
                Debug.Log($"Potion added to inventory: {item.itemName}");
                // might not need potions since we have gems, but keeping it as an option if needed later.
                break;

            case Item.ItemType.Gem:
                Debug.Log($"Gem added to inventory: {item.itemName}");
                // Gems could potentially trigger immediate effects, e.g., healing, increase melee attack, etc.
                Player.Instance.IncreaseHealth(item.value); // Example action
                break;

            case Item.ItemType.Equipment:
                Debug.Log($"Equipment added to inventory: {item.itemName}");
                // optionally, you could equip it directly or add it to the inventory for later equipping.
                break;

            default:
                Debug.LogWarning($"Unhandled item type: {item.GetItemType()}");
                break;
        }
    }

   public void Remove(Item item)
    {
        if (item == null)
        {
            Debug.LogError("Cannot remove item: item reference is null!");
            return;
        }

        if (inventoryItems.Contains(item))
        {
            inventoryItems.Remove(item);
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {item.itemName}");
        }
    }


    public List<Item> getInventoryItems()
    {
        return inventoryItems;
    }
}
