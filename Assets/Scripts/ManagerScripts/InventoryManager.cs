using System.Collections.Generic;
using UnityEngine;


public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private List<ItemSO> inventoryItems = new List<ItemSO>();


    private void Awake()
    {
        Instance = this;
    }


    public void reset()
    {
        inventoryItems.Clear();
    }


    public void addItem(ItemSO item)
    {
        inventoryItems.Add(item);
        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Item is a skill card.  
        {
            SkillCardSO skillCard = (SkillCardSO)item;
            if (PlayerManager.Instance.hasAvailableSkillCardSlot())  // Available skill card slot.
            {
                // Equip the skill card to the first available slot.
                PlayerManager.Instance.equipSkillCard(skillCard);
            }
            else
            {
                skillCard.setEquipped(false);
            }
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

    
    public InventoryData SaveState()
    {
        return new InventoryData
        {
            items = inventoryItems.ConvertAll(item => item.getItemName())
        };
    }

    public void LoadState(InventoryData data)
    {
        inventoryItems = data.items.ConvertAll(name => FindItemByName(name));
    }

    private ItemSO FindItemByName(string name)
    {
        // Locate the ItemSO asset by name
        return Resources.Load<ItemSO>($"Items/{name}");
    }
}
