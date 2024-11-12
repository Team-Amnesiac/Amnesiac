using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    Item item;
    public Button RemoveItemButton;

    public void RemoveItem()
    {
        Debug.Log($"Removing item: {item.itemName}");
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }

    public void UseItem()
    {
        switch (item.itemType)
        {
            case Item.ItemType.Potion:
                break;
            case Item.ItemType.Gem:
                Player.Instance.IncreaseHealth(item.value);
                break;
            case Item.ItemType.SkillCard:
                break;
            case Item.ItemType.Equipment:
                break;
        }
        RemoveItem();
    }
}
