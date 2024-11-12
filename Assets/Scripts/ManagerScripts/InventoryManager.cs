using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;
    public Transform ItemContent;
    public GameObject InventoryItem;
    public Toggle EnableRemove;
    public List<Item> Items = new List<Item>();
    public InventoryItemController[] InventoryItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Item item)
    {
        Items.Add(item);
    }

    public void Remove(Item item)
    {
        if (Items.Contains(item))
        {
            Items.Remove(item);
            Debug.Log($"Removed item: {item.itemName}");
            ListItems();
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {item.itemName}");
        }
    }

    public void ListItems()
    {
        foreach (Transform child in ItemContent)
        {
            Destroy(child.gameObject);
        }

        if (Items.Count == 0)
        {
            Debug.Log("Inventory is empty. No items to list.");
            return;
        }

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            var itemName = obj.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            var itemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();
            var removeItemButton = obj.transform.Find("RemoveItemButton")?.GetComponent<Button>();

            if (itemName != null)
            {
                itemName.text = item.itemName;
            }

            if (itemIcon != null)
            {
                itemIcon.sprite = item.icon;
            }

            if (removeItemButton != null)
            {
                removeItemButton.onClick.AddListener(() => Remove(item));
                removeItemButton.gameObject.SetActive(EnableRemove.isOn);
            }
        }

        SetInventoryItems();
    }

    public void EnableItemsRemove()
    {
        if (EnableRemove.isOn)
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveItemButton").gameObject.SetActive(true);
            }
        }
        else
        {
            foreach (Transform item in ItemContent)
            {
                item.Find("RemoveItemButton").gameObject.SetActive(false);
            }
        }
    }

    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        if (Items.Count == 0 || InventoryItems.Length == 0)
        {
            Debug.Log("No items left in the inventory. Skipping SetInventoryItems.");
            return;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (i < InventoryItems.Length && InventoryItems[i] != null)
            {
                InventoryItems[i].AddItem(Items[i]);
            }
        }
    }
}
