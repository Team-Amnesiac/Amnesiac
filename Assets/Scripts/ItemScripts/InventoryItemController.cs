using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemController : MonoBehaviour
{
    public Item item;
    public Button RemoveItemButton;

    public void RemoveItem()
    {
        if (item != null)
        {
            Debug.Log($"Removing item: {item.itemName}");
            InventoryManager.Instance.Remove(item);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("Cannot remove item: item reference is null!");
        }
    }

    public void AddItem(Item newItem)
    {
        item = newItem;
    }
}
