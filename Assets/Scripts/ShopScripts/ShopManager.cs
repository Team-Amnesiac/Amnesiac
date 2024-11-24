using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Item> shopItems; // ScriptableObject Items available in the shop
    [SerializeField] private GameObject shopItemPrefab; // Prefab for each ShopItemButton
    [SerializeField] private Transform shopItemContainer; // Parent object for dynamically created ShopItemButtons

    private List<Item> selectedItems = new List<Item>(); // Tracks the items the player has selected
    private int totalCost = 0;

    private void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (Item item in shopItems)
        {
            GameObject shopItem = Instantiate(shopItemPrefab, shopItemContainer);

            ShopItemController itemController = shopItem.GetComponent<ShopItemController>();

            // Set up the shop item UI using the ScriptableObject data
            itemController.SetUp(item, this);
        }
    }

    public void AddToTotalCost(Item item)
    {
        totalCost += item.value;
        selectedItems.Add(item);
        ShopUI.Instance.UpdateTotalCost(totalCost);
    }

    public void RemoveFromTotalCost(Item item)
    {
        totalCost -= item.value;
        selectedItems.Remove(item);
        ShopUI.Instance.UpdateTotalCost(totalCost);
    }

    public void FinalizePurchase()
    {
        int playerCurrency = PlayerManager.Instance.GetCurrency();

        if (playerCurrency >= totalCost)
        {
            // Deduct the total cost from the player's currency
            PlayerManager.Instance.SpendCurrency(totalCost);
            ShopUI.Instance.UpdatePlayerCurrency(playerCurrency - totalCost);

            // Add purchased items to the player's inventory
            foreach (Item item in selectedItems)
            {
                InventoryManager.Instance.Add(item);
                Debug.Log($"Purchased: {item.itemName}");
            }

            // Reset the shop selections
            totalCost = 0;
            selectedItems.Clear();
            ShopUI.Instance.UpdateTotalCost(totalCost);
        }
        else
        {
            ShopUI.Instance.ShowNotEnoughCurrencyWarning();
        }
    }
}
