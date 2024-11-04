using UnityEngine;
using UnityEngine.AI;

public class NightcrawlerAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float loseAggroRange = 15f;
    public float attackRange = 2f;
    public float speed = 3.5f;
    private Animator animator;
    private Transform playerTransform;
    private NavMeshAgent navMeshAgent;

    public float maxHealth = 150f;
    //private HealthSystem healthSystem;

    private bool isAggroed = false;
    private bool specialAttackUsed = false;
    void Start()
    {
        animator = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        healthSystem = GetComponent<HealthSystem>();

        if (healthSystem != null)
        {
            healthSystem.maxHealth = maxHealth;
        }
        
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
            GameManager.Instance.TriggerBattleScene();
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
}
