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
    [SerializeField] private int   experienceReward = 20;
    [SerializeField] private float health;
    [SerializeField] private bool ispatrol;
    [SerializeField] private List<Transform> patrols;
    private Transform patrol;

    private bool  isAggroed = false;
    private bool  specialAttackUsed = false;

    private Animator     animator;
    private Transform    playerTransform;
    private NavMeshAgent navMeshAgent;

    public SkillCardSO.AttackType weakness;


    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        health = maxHealth;

        if (Camera.main != null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        if (ispatrol)
        {
            patrol = patrols[Random.Range(0, patrols.Count)];
        }
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, playerTransform.position);

        if (isAggroed)
        {
            if (distanceToPlayer > loseAggroRange || GameObject.FindGameObjectWithTag("Player") == null)
            {
                loseAggro();
            }
            else if (distanceToPlayer > attackRange)
            {
                chasePlayer();
            }
            else
            {
                attackPlayer();
            }
        }
        else
        {
            
            if (distanceToPlayer <= detectionRange)
            {
                gainAggro();
            }
            else
            {
                if (ispatrol)
                {
                    navMeshAgent.isStopped = false;
                    navMeshAgent.SetDestination(patrol.position);
                    if(Vector3.Distance(transform.position, patrol.position) < 1)
                    {
                        patrol = patrols[Random.Range(0, patrols.Count)];
                    }
                }
            }
        }
    }

    public void chasePlayer()
    {
        navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(playerTransform.position);
        animator.SetFloat("motion", 1);
    }

    public void attackPlayer()
    {
        navMeshAgent.isStopped = true;
        animator.SetTrigger("hit");

        if (Vector3.Distance(transform.position, playerTransform.position) <= attackRange)
        {
            Debug.Log("Nightcrawler hits the player, initiating battle with enemy turn first.");
            // Start the battle.
            BattleManager.Instance.initializeBattle(BattleManager.Attacker.Enemy, this);
        }
    }


    public void gainAggro()
    {
        isAggroed = true;
    }

    public void loseAggro()
    {
        isAggroed = false;
        animator.SetFloat("motion", 0);
        navMeshAgent.ResetPath();
    }


    public float calculateHealthPercentage()
    {
        return health / maxHealth;
    }


    public void takeDamage(float damage)
    {
        // Take away the damage amount from the enemy's health.
        health = Mathf.Clamp(health - damage, 0, maxHealth);
    }

    public float getHealth()
    {
        return health;
    }

    public SkillCardSO.AttackType getWeakness()
    {
        return weakness;
    }

    public float getStrength()
    {
        return strength;
    }


    public int getExperienceReward()
    {
        return experienceReward;
    }
}
