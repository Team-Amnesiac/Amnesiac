using UnityEngine;
using UnityEngine.UI;
using TMPro;

// This class controls the behavior and UI representation of a shop item in the shop system.
// It handles adding and removing items from the shopping cart and updates the UI accordingly.

public class ShopItemController : MonoBehaviour
{
    // Reference to the UI text element displaying the item's name.
    [SerializeField] private TextMeshProUGUI  itemNameText;
    // Reference to the UI text element displaying the item's price.
    [SerializeField] private TextMeshProUGUI  priceText;
    // Reference to the UI image displaying the item's sprite.
    [SerializeField] private Image            itemImage;
    // Button to add the item to the shopping cart.
    [SerializeField] private Button           addButton;
    // Button to remove the item from the shopping cart.
    [SerializeField] private Button           removeButton;

    // Reference to the item's scriptable object data.
    private ItemSO item;
    // Tracks the quantity of this item in the shopping cart.
    private int quantity = 0;

    // Initializes the shop item UI with the item's data.
    public void initialize(ItemSO newItem)
    {
        item = newItem; // Set the item reference.

        // Populate the UI with the item's name, price, and sprite.
        itemNameText.text = item.getItemName(); // Set the item name in the UI.
        priceText.text    = $"Price: {item.getValue()}"; // Set the item's price in the UI.
        itemImage.sprite  = item.getItemSprite(); // Set the item's sprite in the UI.

        // Attach event listeners to the add and remove buttons.
        addButton.onClick.AddListener(addToCart); // Call `addToCart` when the add button is clicked.
        removeButton.onClick.AddListener(removeFromCart); // Call `removeFromCart` when the remove button is clicked.
    }

    // Adds the item to the shopping cart and updates the quantity.
    private void addToCart()
    {
        quantity++; // Increment the quantity of the item in the cart.
        ShopManager.Instance.addToCart(item); // Notify the ShopManager to add the item to the cart.
        Debug.Log($"Added {item.getItemName()} to cart. Quantity: {quantity}"); // Log the action.
    }

    // Removes the item from the shopping cart if the quantity is greater than zero.
    private void removeFromCart()
    {
        if (quantity > 0) // Ensure there are items to remove.
        {
            quantity--; // Decrement the quantity of the item in the cart.
            ShopManager.Instance.removeFromCart(item); // Notify the ShopManager to remove the item from the cart.
            Debug.Log($"Removed {item.getItemName()} from cart. Quantity: {quantity}"); // Log the action.
        }
    }
}
