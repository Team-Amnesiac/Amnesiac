using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;

    public enum SkillSlot
    {
        One,
        Two,
        Three,
        Four,
        Melee
    }

    private const int MAX_EQUIPPED_SKILL_CARDS = 4;

    [SerializeField] private float maxPlayerHealth = 100.0f;
    [SerializeField] private float playerHealth;
    [SerializeField] private float meleeDamage     = 25.0f;

    private float playerExperience = 0.0f;

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


    public float calculateHealthPercent()
    {
        return playerHealth / maxPlayerHealth;
    }


    public void takeDamage(float damage)
    {
        playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxPlayerHealth);
    }


    public void equipSkillCard(SkillCardSO skillCardSo)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == null)
            {
                equippedSkillCardArray[i] = skillCardSo;
                Debug.Log($"SkillCard {skillCardSo.getItemName()} equipped!");
                
                return;
            }
        }
    }


    public void increaseHealth(int value)
    {
        playerHealth += value;
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);
    }


    public void increaseExperience(int value)
    {
        playerExperience += value;
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);
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


    public SkillCardSO getSkillCard(SkillSlot slot)
    {
        if (slot == SkillSlot.Melee)  
        {
            return null;  // Melee has no skill card.
        }

        return equippedSkillCardArray[(int)slot];
    }


    public float getMeleeDamage()
    {
        return meleeDamage;
    }


    public float getHealth()
    {
        return playerHealth;
    }


    public float getExperience()
    {
        return playerExperience;
    }


    public bool hasAvailableSkillCardSlot()
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
