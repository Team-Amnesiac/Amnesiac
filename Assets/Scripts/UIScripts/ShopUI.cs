// This class manages the Shop UI, allowing the player to view and purchase items from the shop.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    // Prefab for creating individual shop item entries.
    [SerializeField] private GameObject shopItemPrefab;
    // Content container for displaying shop items.
    [SerializeField] private GameObject shopContent;
    // Text to display the total cost of selected items.
    [SerializeField] private TextMeshProUGUI totalCostText;
    // Text to display the player's current currency.
    [SerializeField] private TextMeshProUGUI playerCurrencyText;
    // Button to finalize the purchase.
    [SerializeField] private Button buyButton;
    // Button to exit the shop UI.
    [SerializeField] private Button exitShopButton;

    // Initializes the Shop UI and attaches button listeners.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Shop, this); // Register the Shop UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Shop); // Hide the Shop UI initially.

        // Attach listeners to button click events.
        buyButton.onClick.AddListener(buyButtonOnClick);
        exitShopButton.onClick.AddListener(exitShopButtonOnClick);
    }

    // Prepares the Shop UI for display by populating the list of shop items.
    public void prepareShopShow()
    {
        foreach (ItemSO item in ShopManager.Instance.getShopItems()) // Iterate through available shop items.
        {
            GameObject shopItem = Instantiate(shopItemPrefab, shopContent.transform); // Create a new shop item entry.

            ShopItemController itemController = shopItem.GetComponent<ShopItemController>();

            // Set up the shop item UI using the ScriptableObject data.
            itemController.initialize(item);
        }
        updateVisuals(); // Update the shop's visual elements.
    }

    // Cleans up the Shop UI by destroying all shop item entries.
    public void prepareShopHide()
    {
        foreach (Transform child in shopContent.transform) // Iterate through all child objects in the shop content.
        {
            Destroy(child.gameObject); // Destroy each child.
        }
    }

    // Updates the visual elements of the Shop UI.
    public void updateVisuals()
    {
        totalCostText.text = $"Total Cost: {ShopManager.Instance.getTotalCost()}"; // Display the total cost of selected items.
        playerCurrencyText.text = $"Currency: {PlayerManager.Instance.getCurrency()}"; // Display the player's current currency.
    }

    // Handles the Buy button click, finalizing the purchase if the player has enough currency.
    private void buyButtonOnClick()
    {
        if (PlayerManager.Instance.getCurrency() < ShopManager.Instance.getTotalCost()) // Check if the player has enough currency.
        {
            UIManager.Instance.newNotification("Not enough currency!"); // Show a notification if the player lacks currency.
            Debug.Log("Not enough currency!"); // Log the insufficient currency message.
        }
        else
        {
            ShopManager.Instance.finalizePurchase(); // Finalize the purchase through the ShopManager.
        }
    }

    // Handles the Exit button click, returning to the Keeper UI.
    private void exitShopButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Shop); // Hide the Shop UI.
        UIManager.Instance.showUI(UIManager.UI.Keeper); // Show the Keeper UI.
    }
}
