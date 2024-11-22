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

    [SerializeField] private SkillCardSO[] equippedSkillCardArray;
    

    void Awake()
    {
        if (Instance == null)
        {
            // Set the singleton reference.
            Instance = this;
            DontDestroyOnLoad(gameObject);

            equippedSkillCardArray = new SkillCardSO[MAX_EQUIPPED_SKILL_CARDS];
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

    public void Equip(SkillCardSO skillCardSo)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == null)
            {
                equippedSkillCardArray[i] = skillCardSo;
                Debug.Log($"SkillCard {skillCardSo.itemName} equipped!");
                
                return;
            }
        }
    }


    public void unequip(SkillCardSO skillCardSo)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == skillCardSo)
            {
                equippedSkillCardArray[i] = null;
            }
        }
    }

    public SkillCardSO GetSkillCard(SkillSlot slot)
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
}
