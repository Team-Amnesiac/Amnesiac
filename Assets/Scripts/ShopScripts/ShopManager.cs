using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    [SerializeField] private List<Item> shopItems; // ScriptableObject Items available in the shop
    [SerializeField] private GameObject shopItemPrefab; // Prefab for each ShopItemButton
    [SerializeField] private Transform shopItemContainer; // Parent object for dynamically created ShopItemButtons
    [SerializeField] private TextMeshProUGUI totalCostText; // Displays the total cost
    [SerializeField] private TextMeshProUGUI playerCurrencyText; // Displays the player's current currency
    [SerializeField] private Button buyButton; // Finalizes purchases

    private int totalCost = 0;
    private List<Item> selectedItems = new List<Item>(); // tracks the items the player has selected

    private void Start()
    {
        PopulateShop();
        UpdatePlayerCurrency();
        UpdateTotalCost();

        buyButton.onClick.AddListener(FinalizePurchase);
    }

    private void PopulateShop()
    {
        foreach (Item item in shopItems)
        {
            GameObject shopItem = Instantiate(shopItemPrefab, shopItemContainer);

            ShopItemController itemController = shopItem.GetComponent<ShopItemController>();

            // set up the shop item UI using the ScriptableObject data
            itemController.SetUp(item, this);
        }
    }

    public void AddToTotalCost(Item item)
    {
        totalCost += item.value;
        selectedItems.Add(item);
        UpdateTotalCost();
    }

    public void RemoveFromTotalCost(Item item)
    {
        totalCost -= item.value;
        selectedItems.Remove(item);
        UpdateTotalCost();
    }

    private void UpdateTotalCost()
    {
        totalCostText.text = $"Total Cost: {totalCost}";
    }

    private void UpdatePlayerCurrency()
    {
        playerCurrencyText.text = $"Currency: {PlayerManager.Instance.GetCurrency()}";
    }

    private void FinalizePurchase()
    {
        if (PlayerManager.Instance.GetCurrency() >= totalCost)
        {
            // deduct the total cost from the player's currency
            PlayerManager.Instance.SpendCurrency(totalCost);
            UpdatePlayerCurrency();

            // Add purchased items to the player's inventory
            foreach (Item item in selectedItems)
            {
                InventoryManager.Instance.Add(item);
                Debug.Log($"Purchased: {item.itemName}");
            }

            // reset the shop selections
            totalCost = 0;
            selectedItems.Clear();
            UpdateTotalCost();
        }
        else
        {
            Debug.Log("Not enough currency!");
        }
    }
}
