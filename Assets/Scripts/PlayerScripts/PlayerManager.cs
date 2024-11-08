using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
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

    [SerializeField] private SkillCard[] EquippedSkillCardArray;
    

    void Awake()
    {
        if (Instance == null)
        {
            // Set the singleton reference.
            Instance = this;
            DontDestroyOnLoad(gameObject);

            EquippedSkillCardArray = new SkillCard[4];
            // TESTING CODE ARRAY
            EquippedSkillCardArray[0] = new SkillCard(35.0f, SkillCard.SkillCardType.Fire);
            EquippedSkillCardArray[1] = new SkillCard(35.0f, SkillCard.SkillCardType.Water);
            EquippedSkillCardArray[2] = new SkillCard(35.0f, SkillCard.SkillCardType.Electric); 
            EquippedSkillCardArray[3] = new SkillCard(35.0f, SkillCard.SkillCardType.Ground);
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

    // Update is called once per frame
    void Update()
    {
        
    }

    public float CalculateHealthPercent()
    {
        return playerHealth / maxPlayerHealth;
    }

    public void TakeDamage(float damage)
    {
        playerHealth = Mathf.Clamp(playerHealth - damage, 0, maxPlayerHealth);
    }

    public SkillCard GetSkillCard(SkillSlot slot)
    {
        if (slot == SkillSlot.Melee)  
        {
            return null;  // Melee has no skill card.
        }

        return EquippedSkillCardArray[(int)slot];
    }

    public float GetMeleeDamage()
    {
        return meleeDamage;
    }

    public float GetHealth()
    {
        return playerHealth;
    }
}
