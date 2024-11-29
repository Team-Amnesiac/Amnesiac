using UnityEngine;


[CreateAssetMenu(fileName = "New ItemSO", menuName = "ItemSO/Create New ItemSO")]
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
                // Retrieve the skill card script.
                SkillCardSO skillCard = (SkillCardSO)this;

                if (skillCard.isEquipped())  // Skill card is equipped.
                {
                    // Unequip skill card.
                    PlayerManager.Instance.unequipSkillCard(skillCard);
                }
                else                         // Skill card is not equipped.
                {
                    // Equip skill card.
                    PlayerManager.Instance.equipSkillCard(skillCard);
                }
                
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
