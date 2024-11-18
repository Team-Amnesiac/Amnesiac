using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public enum Attacker
    {
        Player,  // Player attacked the enemy.
        Enemy,   // Enemy attacked the player,
        None     // No battle has been initiated.
    }

    
    // Singleton instance of the BattleManager class.
    public static BattleManager Instance;

    // The current enemy the player is engaged in battle with.
    private EnemyAI  enemyAI;
    // A reference to the BattleUI class to update the battle visuals.
    private BattleUI battleUI;

    // The current round number.
    private int   roundNumber     = 0;
    // The amount of times the player has taken a turn (including extra turns earned from critical hits).
    private int   playerTurnCount = 0;
    // The amount of times the enemy has taken a turn.
    private int   enemyTurnCount  = 0;
    // Represents whether it is currently the player's extra turn, earned from a critical hit.
    private bool  playerExtraTurn = false;
    // Represents whether the current attack was a critical or not.
    private bool  criticalHit     = false;

    // Represents the current attacker in the battle.
    private Attacker attacker         = Attacker.None;
    private Attacker previousAttacker = Attacker.None;
    private Attacker firstAttacker    = Attacker.None;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /* PUBLIC FUNCTIONS */

    public void InitializeBattle(Attacker attacker, EnemyAI enemyAI)
    {
        // Set the GameState to Battle.
        GameManager.Instance.SetGameState(GameManager.GameState.Battle);
        // Set the attacker to whoever attacked first.
        this.attacker = attacker;
        this.firstAttacker = attacker;
        // Set the EnemyAI of the current battle.
        this.enemyAI = enemyAI;
    }


    public void StartBattle(BattleUI battleUI)
    {
        // Retrieve the BattleUI script.
        this.battleUI = battleUI;
        // Update the turn text on the screen with the current attacker.
        battleUI.UpdateTurnText(attacker);

        roundNumber++;
        battleUI.UpdateRoundCounter(roundNumber);

        if (attacker == Attacker.Enemy)  // Enemy's turn.
        {
            // Play the enemy's turn.
            StartCoroutine(EnemyTurn());
        }
        else
        {
            // Play the player's turn.
            PlayerTurn();
        }
    }

    
    public void PlayerAttack(PlayerManager.SkillSlot slot)
    {
        // Get the SkillCard the Player attacked with, if they used one.
        SkillCard skillCard = PlayerManager.Instance.GetSkillCard(slot);

        if (skillCard == null)  // Melee attack.
        {
            // Deal the player's melee damage to the enemy.
            DealDamageToEnemy(PlayerManager.Instance.GetMeleeDamage(), SkillCard.AttackType.None);
        }
        else                    // SkillCard attack.
        {
            // Deal the selected skill card's type of damage to the enemy.
            DealDamageToEnemy(skillCard.GetDamage(), skillCard.GetAttackType());
        }

        // Update the health bars on the screen.
        battleUI.UpdateHealthBar();

        if (enemyAI.GetHealth() <= 0)   // Enemy has been defeated.
        {
            Debug.Log("Enemy defeated!");
            // End the battle, as the enemy has been defeated.
            EndBattle();

            return;
        }

        if (criticalHit && !playerExtraTurn)  // Player landed a critical hit.
        {
            // Display the critical hit on screen.
            battleUI.ShowCriticalHit();
            // Player has earned their extra turn.
            playerExtraTurn = true;
            // Reset for the next attack by the player.
            criticalHit = false;
            // Player's extra turn, earned from critical hit.
            PlayerTurn();

            // Exit the function.
            return;
        }

        // Reset player's extra turn for next round.
        playerExtraTurn = false;
        // Player did not critical hit the enemy, swap to enemy turn.
        SwapTurns();
    }


    /* PRIVATE FUNCTIONS */

    private void PlayerTurn()
    {
        // Activate the functionality of the player's action buttons.
        battleUI.SetButtonInteractability(true);
        playerTurnCount++;
        battleUI.UpdateIndividualTurnCounters(attacker, playerTurnCount);
    }


    private IEnumerator EnemyTurn()
    {
        // Disable functionality of the player's action buttons.
        battleUI.SetButtonInteractability(false);

        enemyTurnCount++;
        battleUI.UpdateIndividualTurnCounters(attacker, enemyTurnCount);

        yield return new WaitForSeconds(5.0f);

        DealDamageToPlayer(enemyAI.GetStrength());

        if (PlayerManager.Instance.GetHealth() <= 0)  // Player defeated.
        {
            Debug.Log("Player defeated!");
            EndBattle();
        }

        SwapTurns();
    }


    private void SwapTurns()
    {
        if (attacker == Attacker.Enemy)
        {
            attacker = Attacker.Player;
        }
        else
        {
            attacker = Attacker.Enemy;
        }

        if (attacker == firstAttacker)
        {
            // A new round has begun.
            roundNumber++;
            // Update the round counter visuals.
            battleUI.UpdateRoundCounter(roundNumber);
        }

        // Update the turn text
        battleUI.UpdateTurnText(attacker);

        if (attacker == Attacker.Enemy)
        {
            StartCoroutine(EnemyTurn());
        }
        else
        {
            PlayerTurn();
        }
    }


    private void DealDamageToPlayer(float damage)
    {
        // Deal damage to the player.
        PlayerManager.Instance.TakeDamage(damage);
        // Update the BattleUI health bars.
        battleUI.UpdateHealthBar();

        Debug.Log("Player took " + damage + " damage.");
    }


    private void DealDamageToEnemy(float damage, SkillCard.AttackType type)
    {
        if (type == enemyAI.GetWeakness()) // Critical hit.
        {
            // Enemy weakness hit. Multiply the damage.
            damage *= 1.5f;
            // The current hit is a critical hit.
            criticalHit = true;
        }
        enemyAI.TakeDamage(damage);
    }


    private void EndBattle()
    {
        GameManager.Instance.SetGameState(GameManager.GameState.Play);

        Debug.Log("Battle has ended for now.");
    }


    /* GET FUNCTIONS */

    public float GetEnemyHealthPercentage()
    {
        return enemyAI.CalculateHealthPercentage();
    }
}
