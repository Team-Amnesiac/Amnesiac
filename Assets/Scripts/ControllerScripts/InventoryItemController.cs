using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;


public class InventoryItemController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameTMP;
    [SerializeField] private Button          itemButton;
    [SerializeField] private Button          removeItemButton;
    [SerializeField] private Image           equippedImage;

    private ItemSO item;


    void Start()
    {
        itemButton.onClick.AddListener(useItem);
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
        if (item.getItemType() == ItemSO.ItemType.SkillCard)  // Item is a skill card.
        {
            SkillCardSO skillCard = (SkillCardSO)item;
            if (skillCard.isEquipped())  // Skill card was equipped.
            {
                equipItem();
            }
            else                         // Skill card was unequipped.
            {
                unequipItem();
            }
        }
        else
        {
            InventoryManager.Instance.removeItem(item);
        }
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
        itemButton.image.sprite = sprite;
    }


    public void setItemName(string itemName)
    {
        itemNameTMP.text = itemName;
    }


    public void setRemovable(bool removable)
    {
        removeItemButton.gameObject.SetActive(removable);
    }


    public void equipItem()
    {
        equippedImage.gameObject.SetActive(true);
    }


    public void unequipItem()
    {
        equippedImage.gameObject.SetActive(false);
    }
}
