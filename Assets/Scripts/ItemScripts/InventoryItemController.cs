using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryItemController : MonoBehaviour
{
    private Item item;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private Image           itemImage;
    [SerializeField] private Button          removeItemButton;


    void Start()
    {
        removeItemButton.onClick.AddListener(removeItem);
    }

    private void removeItem()
    {
        InventoryManager.Instance.Remove(item);
        Destroy(gameObject);
    }


    public void useItem()
    {
        item.Use();
        InventoryManager.Instance.Remove(item);
    }


    public Item getItem()
    {
        return item;
    }


    public void setItem(Item item)
    {
        this.item = item;
    }


    public void setSprite(Sprite sprite)
    {
        itemImage.sprite = sprite;
    }


    public void setItemName(string itemName)
    {
        itemNameTMP.text = itemName;
    }


    public void setRemovable(bool removable)
    {
        removeItemButton.gameObject.SetActive(removable);
    }
}
