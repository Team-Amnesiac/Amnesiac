using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<ItemSO> inventoryItems = new List<ItemSO>();
    
    // The amount of set pieces collected in each collectible set.
    private int trophyCount = 0;


    private void Awake()
    {
        Instance = this;
    }


    public void addItem(ItemSO item)
    {
        inventoryItems.Add(item);
        if (item.getItemType() == ItemSO.ItemType.SkillCard &&   // Item is a skill card.
            PlayerManager.Instance.hasAvailableSkillCardSlot())  // Available skill card slot.
        {
            SkillCardSO skillCard = (SkillCardSO)item;

            // Equip the skill card to the first available slot.
            PlayerManager.Instance.equipSkillCard(skillCard);
        }
    }


   public void removeItem(ItemSO itemSo)
    {
        if (itemSo == null)
        {
            Debug.LogError("Cannot remove item: item reference is null!");
            return;
        }

        if (inventoryItems.Contains(itemSo))
        {
            inventoryItems.Remove(itemSo);
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {itemSo.getItemName()}");
        }
    }


    public List<ItemSO> getInventoryItems()
    {
        return inventoryItems;
    }
}
