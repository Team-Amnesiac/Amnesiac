using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float detectionRange = 10f;
    [SerializeField] private float loseAggroRange = 15f;
    [SerializeField] private float attackRange    = 2f;
    [SerializeField] private float speed          = 3.5f;
    [SerializeField] private float maxHealth      = 150f;
    [SerializeField] private float strength       = 15.0f;

    [SerializeField] private float health;
    private bool  isAggroed = false;
    private bool  specialAttackUsed = false;

    private Animator     animator;
    private Transform    playerTransform;
    private NavMeshAgent navMeshAgent;

    private SkillCard.SkillCardType weakness;

    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = maxHealth;

        if (Camera.main != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (isAggroed)
        {
            if (distanceToPlayer > loseAggroRange)
            {
                LoseAggro();
            }
            else if (distanceToPlayer > attackRange)
            {
                ChasePlayer();
            }
            else
            {
                AttackPlayer();
            }
        }
        else
        {
            if (distanceToPlayer <= detectionRange)
            {
                GainAggro();
            }
        }
    }

    void ChasePlayer()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(playerTransform.position);
        animator.SetFloat("motion", 1);
    }

    void AttackPlayer()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("hit");

        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            Debug.Log("Nightcrawler hits the player, initiating battle with enemy turn first.");
            // Start the battle.
            BattleManager.Instance.InitializeBattle(BattleManager.Attacker.Enemy, this);
            
        }
    }


    void GainAggro()
    {
        isAggroed = true;
    }

    void LoseAggro()
    {
        isAggroed = false;
        animator.SetFloat("motion", 0);
        navMeshAgent.ResetPath();
    }


    public float CalculateHealthPercentage()
    {
        return health / maxHealth;
    }


    public void TakeDamage(float damage)
    {
        // Take away the damage amount from the enemy's health.
        health = Mathf.Clamp(health - damage, 0, maxHealth);
    }

    public float GetHealth()
    {
        return health;
    }

    public SkillCard.SkillCardType GetWeakness()
    {
        return weakness;
    }

    public float GetStrength()
    {
        return strength;
    }
}
