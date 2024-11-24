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

    public void initializeBattle(Attacker attacker, EnemyAI enemyAI)
    {
        // Set the GameState to Battle.
        GameManager.Instance.setGameState(GameManager.GameState.Battle);
        // Set the attacker to whoever attacked first.
        this.attacker = attacker;
        this.firstAttacker = attacker;
        // Set the EnemyAI of the current battle.
        this.enemyAI = enemyAI;
    }


    public void startBattle(BattleUI battleUI)
    {
        // Retrieve the BattleUI script.
        this.battleUI = battleUI;

        UIManager.Instance.updateUI(UIManager.UI.Battle);
        if (attacker == Attacker.Enemy)  // Enemy's turn.
        {
            // Play the enemy's turn.
            StartCoroutine(enemyTurn());
        }
        else
        {
            // Play the player's turn.
            playerTurn();
        }
    }

    
    public void playerAttack(PlayerManager.SkillSlot slot)
    {
        // Reset for this attack by the player.
        criticalHit = false;

        string combatLogMessage;
        // Get the SkillCard the Player attacked with, if they used one.
        SkillCardSO skillCardSo = PlayerManager.Instance.getSkillCard(slot);

        if (skillCardSo == null)  // Melee attack.
        {
            float damage = PlayerManager.Instance.getMeleeDamage();
            // Deal the player's melee damage to the enemy.
            dealDamageToEnemy(damage, SkillCardSO.AttackType.None);
            combatLogMessage = $"Player meleed the enemy for {damage} damage!";
        }
        else                    // SkillCard attack.
        {
            float damage = skillCardSo.getDamage();
            SkillCardSO.AttackType skillType = skillCardSo.getAttackType();
            
            // Deal the selected skill card's type of damage to the enemy.
            dealDamageToEnemy(damage, skillType);
            string adjective;
            switch (skillType)
            {
                case SkillCardSO.AttackType.Earth:
                    adjective = "quaked";
                    break;
                case SkillCardSO.AttackType.Fire:
                    adjective = "burned";
                    break;
                case SkillCardSO.AttackType.Water:
                    adjective = "splashed";
                    break;
                case SkillCardSO.AttackType.Wind:
                    adjective = "gusted";
                    break;
                default:
                    adjective = "ERROR: INVALID SKILL TYPE";
                    break;
            }

            combatLogMessage = $"Player {adjective} the enemy for {damage} damage !";
        }

        if (criticalHit)
        {
            combatLogMessage += " A critical hit !";
        }

        UIManager.Instance.addCombatLogMessage(combatLogMessage);

        if (enemyAI.getHealth() <= 0)   // Enemy has been defeated.
        {
            Debug.Log("Enemy defeated!");
            // End the battle, as the enemy has been defeated.
            endBattle();

            return;
        }

        if (criticalHit && !playerExtraTurn)  // Player landed a critical hit.
        {
            // Player has earned their extra turn.
            playerExtraTurn = true;
            // Player's extra turn, earned from critical hit.
            playerTurn();

            // Exit the function.
            return;
        }

        // Reset player's extra turn for next round.
        playerExtraTurn = false;
        // Player did not critical hit the enemy, swap to enemy turn.
        swapTurns();
    }
    
    public void runAway()
    {
        endBattle();
    }


    /* PRIVATE FUNCTIONS */

    private void playerTurn()
    {
        // Activate the functionality of the player's action buttons.
        playerTurnCount++;
        UIManager.Instance.updateUI(UIManager.UI.Battle);
    }


    private IEnumerator enemyTurn()
    {
        enemyTurnCount++;
        UIManager.Instance.updateUI(UIManager.UI.Battle);

        yield return new WaitForSeconds(5.0f);

        dealDamageToPlayer(enemyAI.getStrength());

        battleUI.addCombatLogMessage($"Enemy attacked the Player for {enemyAI.getStrength()} damage !");

        if (PlayerManager.Instance.getHealth() <= 0)  // Player defeated.
        {
            Debug.Log("Player defeated!");
            endBattle();
        }

        swapTurns();
    }


    private void swapTurns()
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
        }

        if (attacker == Attacker.Enemy)
        {
            StartCoroutine(enemyTurn());
        }
        else
        {
            playerTurn();
        }
    }


    private void dealDamageToPlayer(float damage)
    {
        // Deal damage to the player.
        PlayerManager.Instance.takeDamage(damage);
    }


    private void dealDamageToEnemy(float damage, SkillCardSO.AttackType type)
    {
        if (type == enemyAI.getWeakness()) // Critical hit.
        {
            // Enemy weakness hit. Multiply the damage.
            damage *= 1.5f;
            // The current hit is a critical hit.
            criticalHit = true;
        }
        enemyAI.takeDamage(damage);
    }


    private void endBattle()
    {
        GameManager.Instance.setGameState(GameManager.GameState.Play);
    }


    /* GET FUNCTIONS */

    public float getEnemyHealthPercentage()
    {
        return enemyAI.calculateHealthPercentage();
    }


    public bool isCriticalHit()
    {
        return criticalHit;
    }


    public Attacker getAttacker()
    {
        return attacker;
    }


    public int getRoundNumber()
    {
        return roundNumber;
    }
}
