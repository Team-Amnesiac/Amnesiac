using UnityEngine;


[CreateAssetMenu(fileName = "New ItemSO", menuName = "Item/Create New ItemSO")]
public class ItemSO : ScriptableObject
{
    public int      id;
    public string   itemName;
    public int      value;
    public Sprite   sprite;
    public ItemType itemType;

    public enum ItemType
    {
        Gem,
        Potion,
        Equipment,
        SkillCard,
        Collectible,
        Relic,
    }


    public void Use()
    {
        switch (itemType)
        {
            case ItemType.Potion:
                break;
            
            case ItemType.Gem:
                Player.Instance.IncreaseHealth(value);
                break;

            case ItemType.SkillCard:
                ((SkillCardSO)this).toggleEquip();
                break;
        }
    }


    public ItemType GetItemType()
    {
        return itemType;
    }


    public Sprite GetSprite()
    {
        return sprite;
    }
}
