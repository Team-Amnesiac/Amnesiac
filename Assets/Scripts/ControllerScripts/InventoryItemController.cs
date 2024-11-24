using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private Image           itemImage;
    [SerializeField] private Button          itemButton;
    [SerializeField] private Button          removeItemButton;

    private ItemSO item;


    void Start()
    {
        removeItemButton.onClick.AddListener(removeItem);
    }

    private void removeItem()
    {
        InventoryManager.Instance.removeItem(item);
        Destroy(gameObject);
    }


    public void useItem()
    {
        item.use();
        InventoryManager.Instance.removeItem(item);
    }


    public ItemSO getItem()
    {
        return item;
    }


    public void setItem(ItemSO itemSo)
    {
        this.item = itemSo;
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
