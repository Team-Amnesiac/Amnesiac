using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    // Singleton instance of the ShopManager class.
    public static ShopManager Instance;

    [SerializeField] private List<ItemSO> shopItems; // ScriptableObject Items available in the shop

    private List<ItemSO> selectedItems = new List<ItemSO>(); // Tracks the items the player has selected
    private int totalCost = 0;
    

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void addToCart(ItemSO item)
    {
        totalCost += item.getValue();
        selectedItems.Add(item);
        UIManager.Instance.updateUI(UIManager.UI.Shop);
    }


    public void removeFromCart(ItemSO item)
    {
        totalCost -= item.getValue();
        selectedItems.Remove(item);
        UIManager.Instance.updateUI(UIManager.UI.Shop);
    }


    public void finalizePurchase()
    {
        // Deduct the total cost from the player's currency
        PlayerManager.Instance.spendCurrency(totalCost);

        // Add purchased items to the player's inventory
        foreach (ItemSO item in selectedItems)
        {
            InventoryManager.Instance.addItem(item);
            Debug.Log($"Purchased: {item.getItemName()}");
        }

        // Reset the shop selections
        totalCost = 0;
        selectedItems.Clear();

        UIManager.Instance.updateUI(UIManager.UI.Shop);
    }


    public int getTotalCost()
    {
        return totalCost;
    }


    public List<ItemSO> getShopItems()
    {
        return shopItems;
    }
}
