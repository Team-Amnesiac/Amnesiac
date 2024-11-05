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

    /* EVENTS */

    // Attacker change event.
    public event EventHandler OnAttackerChange;
    // Health change event.
    public event EventHandler OnHealthChange;

    
    private EnemyAI  enemyAI;
    private BattleUI battleUI;

    private bool  battleInitialized   = false;
    private int   roundNumber         = 0;
    private int   playerTurnCount     = 0;
    private int   enemyTurnCount      = 0;
    private bool  playerUsedExtraTurn = false;

    // Represents who attacked who first, to initialize a battle.
    private Attacker attacker = Attacker.None;


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


    /* EVENT LISTENERS */

    private void BattleUIMelee(object sender, EventArgs eventArgs)
    {
        DealDamageToEnemy(PlayerManager.Instance.GetMeleeDamage(), SkillCard.SkillCardType.None);
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }


    private void BattleUISkillCard(object sender, BattleUI.OnSkillCardEventArgs eventArgs)
    {
        SkillCard skillCard = PlayerManager.Instance.GetSkillCard(eventArgs.skillCardSlot);
        DealDamageToEnemy(skillCard.damage, skillCard.type);
        OnHealthChange?.Invoke(this, EventArgs.Empty);
    }


    /* PUBLIC FUNCTIONS */

    public void InitializeBattle(Attacker attacker, EnemyAI enemyAI)
    {
        // Set the GameState to Battle.
        GameManager.Instance.SetGameState(GameManager.GameState.Battle);
        // Set the attacker to whoever attacked first.
        this.attacker = attacker;
        // Set the EnemyAI of the current battle.
        this.enemyAI = enemyAI;
    }


    public void StartBattle()
    {
        // Retrieve the BattleUI script.
        battleUI = GameObject.FindGameObjectWithTag("BattleUI").GetComponent<BattleUI>();

        // Subscribe to the BattleUI events.
        battleUI.OnMelee += BattleUIMelee;
        battleUI.OnSkillCard += BattleUISkillCard;

        if (attacker == Attacker.Enemy)  // Enemy's turn.
        {
            EnemyTurn();
        }

        // Invoke the OnAttackerChange event, as the attacker has changed.
        OnAttackerChange?.Invoke(this, EventArgs.Empty);
    }

    /* PRIVATE FUNCTIONS */
    private void EnemyTurn()
    {
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
        // Invoke the OnAttackerChange event, as the attacker has changed.
        OnAttackerChange?.Invoke(this, EventArgs.Empty);
    }


    private void DealDamageToPlayer(float damage)
    {
        // Deal damage to the player.
        PlayerManager.Instance.TakeDamage(damage);
        // Invoke the OnHealthChange event.
        OnHealthChange?.Invoke(this, EventArgs.Empty);

        Debug.Log("Player took " + damage + " damage.");
    }


    private void DealDamageToEnemy(float damage, SkillCard.SkillCardType type)
    {
        if (type == enemyAI.GetWeakness())
        {
            // Enemy weakness hit. Multiply the damage.
            damage *= 1.5f;
        }
        // Apply the damage to the enemy.
        enemyAI.TakeDamage(damage);

        Debug.Log("Enemy took " + damage + " damage.");

        if (enemyAI.GetHealth() <= 0)  // Enemy has been defeated.
        {
            Debug.Log("Enemy defeated!");
            EndBattle();
        }
    }


    private void EndBattle()
    {
        battleInitialized = false;
        GameManager.Instance.SetGameState(GameManager.GameState.Play);

        Debug.Log("Battle has ended for now.");
    }


    /* GET FUNCTIONS */

    public int GetPlayerTurnCount()
    {
        return playerTurnCount;
    }


    public int GetEnemyTurnCount()
    {
        return enemyTurnCount;
    }

    public int GetRound()
    {
        return roundNumber;
    }


    public EnemyAI GetEnemyAI()
    {
        return enemyAI;
    }


    public Attacker GetAttacker()
    {
        return attacker;
    }
}
