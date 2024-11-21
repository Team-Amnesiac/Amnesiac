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

    private InventoryUI inventoryUI;
    public List<Item>   inventoryItems = new List<Item>();
    private bool        canRemove      = true;
    
    // The amount of set pieces collected in each collectible set.
    private int trophyCount = 0;


    private void Awake()
    {
        Instance = this;
    }


    public void Add(Item item)
    {
        inventoryItems.Add(item);
        // Increment the collectible set count, if item is a collectible.
        if (item.GetItemType() == Item.ItemType.Collectible)
        {
            Collectible collectible = (Collectible)item;

            // Identify which set this collectible belongs to.
            switch (collectible.GetSet())
            {
                case Collectible.Set.Trophy:
                    trophyCount++;
                    if (trophyCount == (int)Collectible.SetSize.Trophy)  // Trophy set complete.
                    {
                        Debug.Log("Trophy set complete!");
                    }

                    break;
                
                default:
                    Debug.Log("Trophy set complete!");
                    break;
            }
        }
        else if (item.GetItemType() == Item.ItemType.SkillCard)
        {
            if (PlayerManager.Instance.HasAvailableSkillCardSlot())  // There is an empty slot available.
            {
                PlayerManager.Instance.Equip((SkillCard)item);
            }
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


    public void ListItems()
    {
        foreach (Item item in inventoryItems)
        {
            GameObject obj = Instantiate(inventoryItemPrefab, inventoryUI.GetInventoryContent().transform);

            InventoryItemController controller = obj.GetComponent<InventoryItemController>();
            controller.setItem(item);
            controller.setItemName(item.itemName);
            controller.setSprite(item.sprite);
            if (item.itemType == Item.ItemType.SkillCard || !canRemove)  // Item is a SkillCard or all items cannot be removed.
            {
                controller.setRemovable(false);
            }
            else
            {
                controller.setRemovable(true);
            }

        }
    }


    public void SetInventoryUI(InventoryUI inventoryUI)
    {
        this.inventoryUI = inventoryUI;
    }


    public void toggleCanRemove(bool canRemove)
    {
        this.canRemove = canRemove;
    }
}
