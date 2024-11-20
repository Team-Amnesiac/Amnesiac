using UnityEngine;


[CreateAssetMenu(fileName = "New Item", menuName = "Item/Create New Item")]
public class Item : ScriptableObject
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
                break;
            case ItemType.Equipment:
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
