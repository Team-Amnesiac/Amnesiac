using System;
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

    // event triggered when the player levels up (via callbacks).
    public event Action<int> OnLevelUp;

    [SerializeField] private float maxPlayerHealth   = 100.0f;
    [SerializeField] private float playerHealth;
    [SerializeField] private float meleeDamage       = 25.0f;
    [SerializeField] private int currency            = 100;
    [SerializeField] private int playerLevel         = 1;
    [SerializeField] private int experienceThreshold = 50; // eXP required for the next level
    [SerializeField] private int playerStamina       = 100; // stamina used when skillcard used, no stamina means player cannot use skillcards.
    [SerializeField] private int targetEnemiesToDefeat = 3;
    public int enemiesDefeated { get; private set; } = 0;

    private GameObject playerGameObject;

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
        equippedSkillCardArray = null;
        playerStamina = 100;
        currency = 100;
        meleeDamage = 25.0f;
        playerLevel = 1;
        experienceThreshold = 50;
        playerExperience = 0;
    }

    // Reset the all player stats and values.
    public void reset()
    {
        playerHealth = maxPlayerHealth;
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            equippedSkillCardArray[i] = null;
        }
        playerStamina = 100;
        currency = 100;
        meleeDamage = 25.0f;
        playerLevel = 1;
        experienceThreshold = 50;
        playerExperience = 0;
    }


    public void decreaseStamina(int cost)
    {
        playerStamina -= cost;
    }


    public float calculateHealthPercent()
    {
        return playerHealth / maxPlayerHealth;
    }


    public void takeDamage(float damage)
    {
        playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxPlayerHealth);
    }


    public void equipSkillCard(SkillCardSO skillCard)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == null)
            {
                equippedSkillCardArray[i] = skillCard;
                Debug.Log($"SkillCard {skillCard.getItemName()} equipped!");
                
                skillCard.setEquipped(true);
                break;
            }
        }
    }


    public void IncrementEnemiesDefeated()
    {
        enemiesDefeated++;
    }

    public void increaseHealth(int value)
    {
        playerHealth += value;
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);
    }

    public void increaseStamina(int value)
    {
        playerStamina += value;
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);
    }

    // gain exp upon completing quest, fights, completing collectibles.
    public void increaseExperience(int value)
    {
        playerExperience += value;
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);

        // level up while loop depending on the amount of exp player currently has, keep incrementing level according to threshold
        while (playerExperience >= experienceThreshold)
        {
            levelUp();
        }
    }


    public void disablePlayerGameObject()
    {
        playerGameObject.SetActive(false);
    }


    public void enablePlayerGameObject()
    {
        playerGameObject.SetActive(true);
    }


    private int CalculateThreshold()
    {
        // exponential growth for threshold
        return Mathf.RoundToInt(100 * Mathf.Pow(1.5f, playerLevel - 1));
    }

    private void levelUp()
    {
        playerExperience -= experienceThreshold; // carries over extra experience to the next level
        playerLevel++;
        
        // increase the experience threshold dynamically
        experienceThreshold = CalculateThreshold();

        // update player stats for each level
        maxPlayerHealth += 10;       // increase max health by 10 per level.
        meleeDamage += 5;            // increase melee damage by 5 per level.
        currency += 50;              // also reward player with bonus currency.
        playerStamina += 20;

        // refill health to the new max health.
        playerHealth = maxPlayerHealth;

        Debug.Log($"Level Up! Level: {playerLevel}, New Threshold: {experienceThreshold}, Max Health: {maxPlayerHealth}, Melee Damage: {meleeDamage}");

        UIManager.Instance.newNotification($"You leveled up! New Level: {playerLevel}");
        
        // update the UI to reflect the new stats.
        UIManager.Instance.updateUI(UIManager.UI.PlayerHud);
    }

    public void unequipSkillCard(SkillCardSO skillCard)
    {
        for (int i = 0; i < MAX_EQUIPPED_SKILL_CARDS; i++)
        {
            if (equippedSkillCardArray[i] == skillCard)
            {
                skillCard.setEquipped(false);
                equippedSkillCardArray[i] = null;
                break;
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


    public int getStamina()
    {
        return playerStamina;
    }


    public float getExperience()
    {
        return playerExperience;
    }

    public int getPlayerLevel()
    {
        return playerLevel;
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

    // Following Functions are currency related
    public int getCurrency()
    {
        return currency;
    }

    // adding currency after defeating enemy, completing quest, etc.
    public void addCurrency(int amount)
    {
        currency += amount;
    }

    // spending currency in shop.
    public bool spendCurrency(int amount)
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

    public string CurrentCheckpointID { get; set; }
    private void SpawnAtCheckpoint(string checkpointID)
    {
        GameObject checkpoint = GameObject.Find(checkpointID);
        if (checkpoint != null)
        {
            playerGameObject.transform.position = checkpoint.transform.position;
            Debug.Log($"Spawned at checkpoint: {checkpointID}");
        }
        else
        {
            Debug.LogWarning($"Checkpoint {checkpointID} not found. Spawning at default position.");
        }
    }



    public void setPlayerGameObject(GameObject playerGameObject)
    {
        this.playerGameObject = playerGameObject;
    }

    public PlayerData SaveState()
    {
        return new PlayerData
        {
            health = playerHealth,
            level = playerLevel,
            stamina = playerStamina,
            currency = currency,
            experience = playerExperience,
            checkpointID = CurrentCheckpointID
        };
    }

    public void LoadState(PlayerData data)
    {
        playerHealth = data.health;
        playerLevel = data.level;
        playerStamina = data.stamina;
        currency = data.currency;
        playerExperience = data.experience;
        SpawnAtCheckpoint(data.checkpointID);
    }
}
