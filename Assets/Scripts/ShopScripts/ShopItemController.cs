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

    private Item item;
    private ShopManager shopManager;
    private int quantity = 0; // Tracks quantity for this item

    public void SetUp(Item newItem, ShopManager manager)
    {
        item = newItem;
        shopManager = manager;

        // Populate the UI with the item's data
        itemNameText.text = item.itemName;
        priceText.text = $"Price: {item.value}";
        itemImage.sprite = item.GetSprite();

        // Add listeners to the buttons
        addButton.onClick.AddListener(() => AddToCart());
        removeButton.onClick.AddListener(() => RemoveFromCart());
    }

    private void AddToCart()
    {
        quantity++;
        shopManager.AddToTotalCost(item);
        Debug.Log($"Added {item.itemName} to cart. Quantity: {quantity}");
    }

    private void RemoveFromCart()
    {
        if (quantity > 0)
        {
            quantity--;
            shopManager.RemoveFromTotalCost(item);
            Debug.Log($"Removed {item.itemName} from cart. Quantity: {quantity}");
        }
    }
}
