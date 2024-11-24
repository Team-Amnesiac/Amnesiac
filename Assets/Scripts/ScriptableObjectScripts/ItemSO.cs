using UnityEngine;


[CreateAssetMenu(fileName = "New ItemSO", menuName = "Item/Create New ItemSO")]
public class ItemSO : ScriptableObject
{
    [SerializeField] private int      id;
    [SerializeField] private string   itemName;
    [SerializeField] private int      value;
    [SerializeField] private Sprite   sprite;
    [SerializeField] private ItemType itemType;

    public enum ItemType
    {
        Gem,
        Potion,
        Equipment,
        SkillCard,
        Collectible,
        Relic,
    }


    public void use()
    {
        switch (itemType)
        {
            case ItemType.Potion:
                break;
            
            case ItemType.Gem:
                PlayerManager.Instance.increaseHealth(value);
                break;

            case ItemType.SkillCard:
                ((SkillCardSO)this).toggleEquip();
                break;
        }
    }


    public string getItemName()
    {
        return itemName;
    }


    public int getValue()
    {
        return value;
    }


    public Sprite getItemSprite()
    {
        return sprite;
    }


    public ItemType getItemType()
    {
        return itemType;
    }
}
