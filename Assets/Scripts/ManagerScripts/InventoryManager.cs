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

    public List<ItemSO>   inventoryItems = new List<ItemSO>();
    
    // The amount of set pieces collected in each collectible set.
    private int trophyCount = 0;


    private void Awake()
    {
        Instance = this;
    }


    public void Add(ItemSO itemSo)
    {
        inventoryItems.Add(itemSo);
        // Increment the collectible set count, if item is a collectible.
        if (itemSo.GetItemType() == ItemSO.ItemType.Collectible)
        {
            CollectibleSO collectibleSo = (CollectibleSO)itemSo;

            // Identify which set this collectible belongs to.
            switch (collectibleSo.GetSet())
            {
                case CollectibleSO.Set.Trophy:
                    trophyCount++;
                    if (trophyCount == (int)CollectibleSO.SetSize.Trophy)  // Trophy set complete.
                    {
                        Debug.Log("Trophy set complete!");
                    }

                    break;
                
                default:
                    Debug.Log("Trophy set complete!");
                    break;
            }
        }
        else if (itemSo.GetItemType() == ItemSO.ItemType.SkillCard)
        {
            if (PlayerManager.Instance.HasAvailableSkillCardSlot())  // There is an empty slot available.
            {
                PlayerManager.Instance.Equip((SkillCardSO)itemSo);
            }
        }
    }

   public void Remove(ItemSO itemSo)
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
            Debug.LogWarning($"Item not found in inventory: {itemSo.itemName}");
        }
    }


    public List<ItemSO> getInventoryItems()
    {
        return inventoryItems;
    }
}
