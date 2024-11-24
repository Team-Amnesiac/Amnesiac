using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    // Singleton instance of the ShopManager class.
    public static ShopManager Instance;

    [SerializeField] private List<ItemSO> shopItems; // ScriptableObject Items available in the shop
    [SerializeField] private GameObject shopItemPrefab; // Prefab for each ShopItemButton
    [SerializeField] private Transform shopItemContainer; // Parent object for dynamically created ShopItemButtons

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
    

    void Start()
    {
        PopulateShop();
    }

    private void PopulateShop()
    {
        foreach (ItemSO item in shopItems)
        {
            GameObject shopItem = Instantiate(shopItemPrefab, shopItemContainer);

            ShopItemController itemController = shopItem.GetComponent<ShopItemController>();

            // Set up the shop item UI using the ScriptableObject data
            itemController.SetUp(item, this);
        }
    }

    public void AddToTotalCost(ItemSO item)
    {
        totalCost += item.getValue();
        selectedItems.Add(item);
        ShopUI.Instance.UpdateTotalCost(totalCost);
    }

    public void RemoveFromTotalCost(ItemSO item)
    {
        totalCost -= item.getValue();
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
            foreach (ItemSO item in selectedItems)
            {
                InventoryManager.Instance.addItem(item);
                Debug.Log($"Purchased: {item.getItemName()}");
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
