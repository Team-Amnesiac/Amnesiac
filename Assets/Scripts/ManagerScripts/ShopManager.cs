// This class manages the shop system, including items for sale, the player's shopping cart, and purchase finalization.

using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    // Singleton instance of the ShopManager class.
    public static ShopManager Instance;

    // List of items available in the shop, defined as ScriptableObjects.
    [SerializeField] private List<ItemSO> shopItems;

    // Tracks the items the player has selected for purchase.
    private List<ItemSO> selectedItems = new List<ItemSO>();

    // Tracks the total cost of the selected items.
    private int totalCost = 0;

    // Ensures only one instance of ShopManager exists and persists across scenes.
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Assign the singleton instance.
            DontDestroyOnLoad(gameObject); // Preserve the ShopManager across scene loads.
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances.
        }
    }

    // Adds an item to the player's shopping cart and updates the total cost.
    public void addToCart(ItemSO item)
    {
        totalCost += item.getValue(); // Add the item's cost to the total cost.
        selectedItems.Add(item); // Add the item to the selected items list.
        UIManager.Instance.updateUI(UIManager.UI.Shop); // Update the shop UI to reflect the changes.
    }

    // Removes an item from the player's shopping cart and updates the total cost.
    public void removeFromCart(ItemSO item)
    {
        totalCost -= item.getValue(); // Subtract the item's cost from the total cost.
        selectedItems.Remove(item); // Remove the item from the selected items list.
        UIManager.Instance.updateUI(UIManager.UI.Shop); // Update the shop UI to reflect the changes.
    }

    // Finalizes the purchase, deducting the total cost from the player's currency and adding the items to the inventory.
    public void finalizePurchase()
    {
        PlayerManager.Instance.spendCurrency(totalCost); // Deduct the total cost from the player's currency.

        // Add each selected item to the player's inventory.
        foreach (ItemSO item in selectedItems)
        {
            InventoryManager.Instance.addItem(item); // Add the item to the inventory.
            Debug.Log($"Purchased: {item.getItemName()}"); // Log the purchase.
        }

        // Reset the shop selections and total cost.
        totalCost = 0;
        selectedItems.Clear();

        UIManager.Instance.updateUI(UIManager.UI.Shop); // Update the shop UI to reflect the cleared cart.
    }

    // Returns the total cost of the items currently in the player's shopping cart.
    public int getTotalCost()
    {
        return totalCost;
    }

    // Returns the list of items available in the shop.
    public List<ItemSO> getShopItems()
    {
        return shopItems;
    }
}
