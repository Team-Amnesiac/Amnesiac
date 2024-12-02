// This class defines the battle behavior for the "Nightcrawler" enemy in the game.
// It handles attack logic, including a special attack, and maintains the state of the special attack.

using UnityEngine;

public class NightcrawlerBattle : MonoBehaviour
{
    // The maximum health of the Nightcrawler during the battle.
    public float maxHealth = 150f;

    // Tracks whether the special attack has already been used.
    private bool specialAttackUsed = false;

    // Unity's Start method, used for initialization if needed.
    void Start()
    {
        // Currently, no initialization logic is defined for this class.
    }

    // Executes the Nightcrawler's attack logic.
    public void executeAttack()
    {
        float damage = 15f; // Default damage for a standard attack.

        if (!specialAttackUsed && Random.value > 0.9f) // Check if the special attack can be triggered.
        {
            damage = 40f; // Set damage for the special attack.
            specialAttackUsed = true; // Mark the special attack as used.
            Debug.Log("Nightcrawler used Earthshatter! Deals 40 damage."); // Log the special attack message.
        }
        else
        {
            Debug.Log("Nightcrawler used a standard attack. Deals 15 damage."); // Log the standard attack message.
        }

        // Apply the damage to the player (currently commented out, to be implemented).
        // BattleManager.Instance.DealDamageToPlayer(damage);
    }

    // Resets the special attack state, allowing it to be used again in the future.
    public void resetSpecialAttack()
    {
        specialAttackUsed = false; // Reset the special attack flag.
    }
}
