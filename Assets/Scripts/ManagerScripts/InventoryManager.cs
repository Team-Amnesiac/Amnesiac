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
        if (item == null)
        {
            Debug.LogError("Cannot remove item: item reference is null!");
            return;
        }

        if (Items.Contains(item))
        {
            Items.Remove(item);
            ListItems();
        }
        else
        {
            Debug.LogWarning($"Item not found in inventory: {item.itemName}");
        }
    }

    public void ListItems()
    {
        foreach (Transform item in ItemContent)
        {
            Destroy(item.gameObject);
        }

        InventoryItems = new InventoryItemController[0];

        foreach (var item in Items)
        {
            GameObject obj = Instantiate(InventoryItem, ItemContent);

            var controller = obj.GetComponent<InventoryItemController>();
            if (controller != null)
            {
                controller.AddItem(item);
            }

            var itemName = obj.transform.Find("ItemName")?.GetComponent<TextMeshProUGUI>();
            if (itemName != null)
            {
                itemName.text = item.itemName;
            }

            var itemIcon = obj.transform.Find("ItemIcon")?.GetComponent<Image>();
            if (itemIcon != null)
            {
                itemIcon.sprite = item.icon;
            }

            var removeItemButton = obj.transform.Find("RemoveItemButton")?.GetComponent<Button>();
            if (removeItemButton != null)
            {
                removeItemButton.onClick.RemoveAllListeners();
                removeItemButton.onClick.AddListener(() => Remove(item));
                removeItemButton.gameObject.SetActive(EnableRemove.isOn);
            }
        }
    }

    public void EnableItemsRemove()
    {
        foreach (Transform item in ItemContent)
        {
            var removeButton = item.Find("RemoveItemButton")?.gameObject;
            if (removeButton != null)
            {
                removeButton.SetActive(EnableRemove.isOn);
            }
        }
    }


    public void SetInventoryItems()
    {
        InventoryItems = ItemContent.GetComponentsInChildren<InventoryItemController>();

        if (Items.Count != InventoryItems.Length)
        {
            Debug.LogError("Mismatch between Items count and InventoryItems array length. Refreshing Inventory UI.");
            ListItems();
            return;
        }

        for (int i = 0; i < Items.Count; i++)
        {
            if (InventoryItems[i] != null && Items[i] != null)
            {
                InventoryItems[i].AddItem(Items[i]);
            }
        }
    }
}
