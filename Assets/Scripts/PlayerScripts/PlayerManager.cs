using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    private const int MAX_EQUIPPED_SKILL_CARDS = 4;

    public enum SkillSlot
    {
        One,
        Two,
        Three,
        Four,
        Melee
    }


    public static PlayerManager Instance;


    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private float playerHealth;
    [SerializeField] private float meleeDamage     = 25.0f;
    [SerializeField] private int currency          = 100;

    [SerializeField] private SkillCard[] equippedSkillCardArray;
    

    void Awake()
    {
        if (Instance == null)
        {
            // Set the singleton reference.
            Instance = this;
            DontDestroyOnLoad(gameObject);

            equippedSkillCardArray = new SkillCard[MAX_EQUIPPED_SKILL_CARDS];
        }
        else
        {
            // Destroy this object, as one already exists.
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the player to full health.
        playerHealth = maxPlayerHealth;
    }

    public float CalculateHealthPercent()
    {
        return playerHealth / maxPlayerHealth;
    }

    public void TakeDamage(float damage)
    {
        playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxPlayerHealth);
    }

    public void Equip(SkillCard skillCard)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == null)
            {
                equippedSkillCardArray[i] = skillCard;
                Debug.Log($"SkillCard {skillCard.itemName} equipped!");
                
                return;
            }
        }
    }

    public SkillCard GetSkillCard(SkillSlot slot)
    {
        if (slot == SkillSlot.Melee)  
        {
            return null;  // Melee has no skill card.
        }

        return equippedSkillCardArray[(int)slot];
    }

    public float GetMeleeDamage()
    {
        return meleeDamage;
    }

    public float GetHealth()
    {
        return playerHealth;
    }

    public bool HasAvailableSkillCardSlot()
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == null)
            {
                return true;
            }
        }

        return false;
    }

    // Following Functions are currency related
    public int GetCurrency()
    {
        return currency;
    }

    // adding currency after defeating enemy, completing quest, etc.
    public void AddCurrency(int amount)
    {
        currency += amount;
    }

    // spending currency in shop.
    public bool SpendCurrency(int amount)
    {
        if (currency >= amount)
        {
            currency -= amount;
            return true;
        }
        else
        {
            return false;
        }
    }
}
