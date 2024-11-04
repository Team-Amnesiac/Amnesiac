using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using TMPro;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance;
    public bool isPlayerTurn;
    private bool battleInitialized = false;
    public TMP_Text turnIndicatorText;
    public TMP_Text roundText;
    public TMP_Text turnsText;
    public Button meleeAttackButton;
    public Button skillCardButton1;
    public Button skillCardButton2;
    public Button skillCardButton3;
    public Button skillCardButton4;
    public Slider playerHealthSlider;
    public Slider enemyHealthSlider;
    private float playerMaxHealth = 100f;
    private float enemyMaxHealth = 150f;
    private float currentPlayerHealth;
    private float currentEnemyHealth;
    private int roundNumber = 1;
    private int playerTurns = 1;
    private bool playerUsedExtraTurn = false;

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

    void Start()
    {
        if (GameManager.Instance.GetGameState() == GameManager.GameState.Battle)
        {
            this.enabled = false;
            return;
        }
    
        currentPlayerHealth = playerMaxHealth;
        currentEnemyHealth = enemyMaxHealth;
        UpdateHealthBars();

        if (meleeAttackButton != null) meleeAttackButton.onClick.AddListener(OnMeleeAttackButtonClicked);
        if (skillCardButton1 != null) skillCardButton1.onClick.AddListener(OnSkillCardButton1Clicked);
        if (skillCardButton2 != null) skillCardButton2.onClick.AddListener(OnSkillCardButton2Clicked);
        if (skillCardButton3 != null) skillCardButton3.onClick.AddListener(OnSkillCardButton3Clicked);
        if (skillCardButton4 != null) skillCardButton4.onClick.AddListener(OnSkillCardButton4Clicked);
    }

    public void StartBattle(bool playerInitiated)
    {
        isPlayerTurn = playerInitiated;
        Debug.Log("Transitioning to battle scene...");
    }

    private void OnBattleSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "battlescene" && !battleInitialized)
        {
            battleInitialized = true;
            InitializeBattle();
        }
    }

    public void DealDamageToPlayer(float damage)
    {
        currentPlayerHealth -= damage;
        currentPlayerHealth = Mathf.Clamp(currentPlayerHealth, 0, playerMaxHealth);
        UpdateHealthBars();

        Debug.Log("Player took " + damage + " damage.");

        if (currentPlayerHealth <= 0)
        {
            Debug.Log("Player defeated!");
            EndBattle();
        }
    }

    public void DealDamageToEnemy(float damage)
    {
        currentEnemyHealth -= damage;
        currentEnemyHealth = Mathf.Clamp(currentEnemyHealth, 0, enemyMaxHealth);
        UpdateHealthBars();

        Debug.Log("Enemy took " + damage + " damage.");

        if (currentEnemyHealth <= 0)
        {
            Debug.Log("Enemy defeated!");
            EndBattle();
        }
    }

    public void InitializeBattle()
    {
        Debug.Log("Battle initialized! " + (isPlayerTurn ? "Player's turn first." : "Enemy's turn first."));
        UpdateUI();
        StartCoroutine(ShowTurnIndicator(isPlayerTurn));

        SetButtonInteractability(isPlayerTurn);
    }

    void ProcessTurn()
    {
        if (isPlayerTurn)
        {
            PlayerTurn();
        }
        else
        {
            EnemyTurn();
        }
    }

    void PlayerTurn()
    {
        Debug.Log("Player's Turn now.");
        SetButtonInteractability(true);
        UpdateUI();
        StartCoroutine(ShowTurnIndicator(true));
    }

    public void OnMeleeAttackButtonClicked()
    {
        if (isPlayerTurn)
        {
            PerformMeleeAttack();
        }
    }

    //placeholders for all the skill cards now
    public void OnSkillCardButton1Clicked()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Player uses Skill Card 1.");
        }
    }

    public void OnSkillCardButton2Clicked()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Player uses Skill Card 2.");
        }
    }

    public void OnSkillCardButton3Clicked()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Player uses Skill Card 3.");
        }
    }

    public void OnSkillCardButton4Clicked()
    {
        if (isPlayerTurn)
        {
            Debug.Log("Player uses Skill Card 4.");
        }
    }

    void PerformMeleeAttack()
    {
        float damage = 20f;
        Debug.Log("Player attacked the enemy, dealt: " + damage + " damage.");
        DealDamageToEnemy(damage);

        if (!playerUsedExtraTurn && EnemyIsWeakToAttack())
        {
            playerTurns++;
            playerUsedExtraTurn = true;
            Debug.Log("Player hits enemy's weakness and now has an extra turn.");
        }

        playerTurns--;
        if (playerTurns <= 0)
        {
            EndPlayerTurn();
        }
        else
        {
            UpdateUI();
        }
    }

    public void EndPlayerTurn()
    {
        isPlayerTurn = false;
        SetButtonInteractability(false);
        Debug.Log("Player's turn ends and so enemy's turn begins.");
        ProcessTurn();
    }

    void EnemyTurn()
    {
        Debug.Log("Enemy's Turn now.");
        UpdateUI();
        StartCoroutine(ShowTurnIndicator(false));
        EndEnemyTurn();
    }

    public void EndEnemyTurn()
    {
        isPlayerTurn = true;
        SetButtonInteractability(true);
        Debug.Log("Enemy's turn ends and so player's turn begins.");

        if (isPlayerTurn && playerTurns == 1)
        {
            roundNumber++;
            playerUsedExtraTurn = false;
            Debug.Log("Round= " + roundNumber + " begins.");
        }

        UpdateUI();
        ProcessTurn();
    }

    IEnumerator ShowTurnIndicator(bool isPlayerTurn)
    {
        turnIndicatorText.text = isPlayerTurn ? "Player's Turn" : "Enemy's Turn";
        turnIndicatorText.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        turnIndicatorText.gameObject.SetActive(false);
    }

    void UpdateUI()
    {
        if (roundText != null)
        {
            roundText.text = "Round: " + roundNumber;
        }

        if (turnsText != null)
        {
            turnsText.text = "Turns: " + playerTurns;
        }
    }

    void UpdateHealthBars()
    {
        if (playerHealthSlider != null)
        {
            playerHealthSlider.value = currentPlayerHealth / playerMaxHealth;
        }

        if (enemyHealthSlider != null)
        {
            enemyHealthSlider.value = currentEnemyHealth / enemyMaxHealth;
        }
    }

    void SetButtonInteractability(bool interactable)
    {
        meleeAttackButton.interactable = interactable;
        skillCardButton1.interactable = interactable;
        skillCardButton2.interactable = interactable;
        skillCardButton3.interactable = interactable;
        skillCardButton4.interactable = interactable;
    }

    public void EndBattle()
    {
        battleInitialized = false;
        meleeAttackButton.onClick.RemoveListener(OnMeleeAttackButtonClicked);
        skillCardButton1.onClick.RemoveListener(OnSkillCardButton1Clicked);
        skillCardButton2.onClick.RemoveListener(OnSkillCardButton2Clicked);
        skillCardButton3.onClick.RemoveListener(OnSkillCardButton3Clicked);
        skillCardButton4.onClick.RemoveListener(OnSkillCardButton4Clicked);
        Debug.Log("Battle has ended for now.");
    }

    bool EnemyIsWeakToAttack()
    {
        return false;
    }
}
