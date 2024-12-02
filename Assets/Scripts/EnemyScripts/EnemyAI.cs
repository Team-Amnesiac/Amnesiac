// This class defines the AI behavior for enemy characters in the game.
// It includes logic for patrolling, detecting the player, chasing, attacking, and interacting with the battle system.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    // Range within which the enemy detects the player.
    [SerializeField] private float detectionRange = 10f;
    // Range at which the enemy stops chasing the player.
    [SerializeField] private float loseAggroRange = 15f;
    // Range within which the enemy can attack the player.
    [SerializeField] private float attackRange    = 2f;
    // Movement speed of the enemy.
    [SerializeField] private float speed          = 3.5f;
    // Maximum health of the enemy.
    [SerializeField] private float maxHealth      = 150f;
    // Attack strength of the enemy.
    [SerializeField] private float strength       = 15.0f;
    // Experience points awarded to the player upon defeating the enemy.
    [SerializeField] private int   experienceReward = 20;
    // Current health of the enemy.
    [SerializeField] private float health;
    // Indicates whether the enemy is patrolling.
    [SerializeField] private bool ispatrol;
    // List of patrol points for the enemy to move between.
    [SerializeField] private List<Transform> patrols;
    // Current patrol point the enemy is heading toward.
    private Transform patrol;

    // Indicates whether the enemy is aggroed (actively chasing the player).
    private bool  isAggroed = false;
    // Indicates if the enemy has used its special attack.
    private bool  specialAttackUsed = false;

    // Reference to the enemy's animator component for animations.
    private Animator     animator;
    // Reference to the player's transform.
    private Transform    playerTransform;
    // Reference to the enemy's NavMeshAgent for movement.
    private NavMeshAgent navMeshAgent;

    // Enemy's weakness to a specific type of attack.
    public SkillCardSO.AttackType weakness;

    // Initializes enemy properties and components.
    void Start()
    {
        animator = GetComponent<Animator>(); // Get the animator component for controlling animations.
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent for pathfinding and movement.
        health = maxHealth; // Set the enemy's health to its maximum.

        if (Camera.main != null) // Ensure the main camera exists.
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Find the player's transform.
        }

        if (ispatrol) // If the enemy is set to patrol.
        {
            patrol = patrols[Random.Range(0, patrols.Count)]; // Select a random patrol point.
        }
    }

    // Updates the enemy's behavior every frame.
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position); 
        // Calculate the distance between the enemy and the player.

        if (isAggroed) // If the enemy is actively chasing the player.
        {
            if (distanceToPlayer > loseAggroRange || GameObject.FindGameObjectWithTag("Player") == null) 
            // Lose aggro if the player is out of range or doesn't exist.
            {
                loseAggro();
            }
            else if (distanceToPlayer > attackRange) // If the player is within chasing range but not attack range.
            {
                chasePlayer();
            }
            else // If the player is within attack range.
            {
                attackPlayer();
            }
        }
        else // If the enemy is not aggroed.
        {
            if (distanceToPlayer <= detectionRange) // Detect the player if within detection range.
            {
                gainAggro();
            }
            else // Continue patrolling if the player is not detected.
            {
                if (ispatrol) // If the enemy is set to patrol.
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(patrol.position); // Move to the current patrol point.

                    if(Vector3.Distance(transform.position, patrol.position) < 1) 
                    // If the enemy reaches the patrol point.
                    {
                        patrol = patrols[Random.Range(0, patrols.Count)]; // Select a new random patrol point.
                    }
                }
            }
        }
    }

    // Handles chasing the player.
    public void chasePlayer()
    {
        navMeshAgent.isStopped = false; // Enable movement.
        navMeshAgent.SetDestination(playerTransform.position); // Set the destination to the player's position.
        animator.SetFloat("motion", 1); // Trigger running animation.
    }

    // Handles attacking the player.
    public void attackPlayer()
    {
        navMeshAgent.isStopped = true; // Stop movement.
        animator.SetTrigger("hit"); // Trigger attack animation.

        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange) 
        // Ensure the player is still within attack range.
        {
            Debug.Log("Nightcrawler hits the player, initiating battle with enemy turn first.");
            BattleManager.Instance.initializeBattle(BattleManager.Attacker.Enemy, this); 
            // Start the battle with the enemy attacking first.
        }
    }

    // Enables the enemy to start chasing the player.
    public void gainAggro()
    {
        isAggroed = true; // Set the enemy to aggro mode.
    }

    // Stops the enemy from chasing the player and resets its state.
    public void loseAggro()
    {
        isAggroed = false; // Disable aggro mode.
        animator.SetFloat("motion", 0); // Stop animations.
        navMeshAgent.ResetPath(); // Clear the current path.
    }

    // Calculates the enemy's remaining health as a percentage.
    public float calculateHealthPercentage()
    {
        return health / maxHealth; // Return the health percentage.
    }

    // Reduces the enemy's health by the given damage amount.
    public void takeDamage(float damage)
    {
        health = Mathf.Clamp(health - damage, 0, maxHealth); 
        // Subtract damage from health, ensuring it doesn't go below 0 or above the maximum.
    }

    // Returns the enemy's current health.
    public float getHealth()
    {
        return health;
    }

    // Returns the enemy's weakness type.
    public SkillCardSO.AttackType getWeakness()
    {
        return weakness;
    }

    // Returns the enemy's attack strength.
    public float getStrength()
    {
        return strength;
    }

    // Returns the amount of experience points the player earns upon defeating this enemy.
    public int getExperienceReward()
    {
        return experienceReward;
    }
}
