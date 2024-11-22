using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryItemController : MonoBehaviour
{
    private ItemSO _itemSo;
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private Image           itemImage;
    [SerializeField] private Button          itemButton;
    [SerializeField] private Button          removeItemButton;


    void Start()
    {
        removeItemButton.onClick.AddListener(removeItem);
    }

    private void removeItem()
    {
        InventoryManager.Instance.Remove(_itemSo);
        Destroy(gameObject);
    }


    public void useItem()
    {
        _itemSo.Use();
        InventoryManager.Instance.Remove(_itemSo);
    }


    public ItemSO getItem()
    {
        return _itemSo;
    }


    public void setItem(ItemSO itemSo)
    {
        this._itemSo = itemSo;
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
