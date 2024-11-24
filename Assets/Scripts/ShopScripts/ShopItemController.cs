using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Image itemImage;
    [SerializeField] private Button addButton; // Button to add the item to the total
    [SerializeField] private Button removeButton; // Button to remove the item from the total

    private ItemSO item;
    private ShopManager shopManager;
    private int quantity = 0; // Tracks quantity for this item

    public void SetUp(ItemSO newItem, ShopManager manager)
    {
        item = newItem;
        shopManager = manager;

        // Populate the UI with the item's data
        itemNameText.text = item.getItemName();
        priceText.text = $"Price: {item.getValue()}";
        itemImage.sprite = item.getItemSprite();

        // Add listeners to the buttons
        addButton.onClick.AddListener(() => AddToCart());
        removeButton.onClick.AddListener(() => RemoveFromCart());
    }

    private void AddToCart()
    {
        quantity++;
        shopManager.AddToTotalCost(item);
        Debug.Log($"Added {item.getItemName()} to cart. Quantity: {quantity}");
    }

    private void RemoveFromCart()
    {
        if (quantity > 0)
        {
            quantity--;
            shopManager.RemoveFromTotalCost(item);
            Debug.Log($"Removed {item.getItemName()} from cart. Quantity: {quantity}");
        }
    }
}
