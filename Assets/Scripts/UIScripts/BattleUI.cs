using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI turnIndicatorTMP;
    [SerializeField] private TextMeshProUGUI roundTMP;
    [SerializeField] private TextMeshProUGUI playerTurnTMP;
    [SerializeField] private TextMeshProUGUI enemyTurnTMP;
    [SerializeField] private TextMeshProUGUI criticalHitTMP;
    [SerializeField] private TextMeshProUGUI skillCard1TMP;
    [SerializeField] private TextMeshProUGUI skillCard2TMP;
    [SerializeField] private TextMeshProUGUI skillCard3TMP;
    [SerializeField] private TextMeshProUGUI skillCard4TMP;
    [SerializeField] private TextMeshProUGUI skillCard1CostTMP;
    [SerializeField] private TextMeshProUGUI skillCard2CostTMP;
    [SerializeField] private TextMeshProUGUI skillCard3CostTMP;
    [SerializeField] private TextMeshProUGUI skillCard4CostTMP;

    [SerializeField] private Button meleeAttackButton;
    [SerializeField] private Button skillCardButton1;
    [SerializeField] private Button skillCardButton2;
    [SerializeField] private Button skillCardButton3;
    [SerializeField] private Button skillCardButton4;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button runAwayButton;
    [SerializeField] private Image playerHealthFillBar;
    [SerializeField] private Image playerStaminaFillBar;
    [SerializeField] private Image enemyHealthFillBar;
    [SerializeField] private GameObject combatLogContent;
    [SerializeField] private GameObject messagePrefab;


    /* UNITY FUNCTIONS */

    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Battle, this);
        UIManager.Instance.hideUI(UIManager.UI.Battle);
        addOnClickListeners();
    }


    void OnDestroy()
    {
        meleeAttackButton.onClick.RemoveListener(onMeleeAttackButtonClicked);
        skillCardButton1.onClick.RemoveListener(onSkillCardButton1Clicked);
        skillCardButton2.onClick.RemoveListener(onSkillCardButton2Clicked);
        skillCardButton3.onClick.RemoveListener(onSkillCardButton3Clicked);
        skillCardButton4.onClick.RemoveListener(onSkillCardButton4Clicked);
        inventoryButton.onClick.RemoveListener(onInventoryButtonClicked);
        runAwayButton.onClick.RemoveListener(onRunAwayButtonClicked);
    }


    /* PUBLIC FUNCTIONS */


    public void updateVisuals()
    {
        criticalHitTMP.gameObject.SetActive(false);
        updateSkillCardButtonText();
        updateTurnText();
        updateTurnCounters();
        updatePlayerStamina();
        updateHealthBars();
        updateRoundCounter(BattleManager.Instance.getRoundNumber());
        if (BattleManager.Instance.isCriticalHit())
        {
            StartCoroutine(criticalHitCoroutine());
        }

        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Enemy)  // Enemies turn.
        {
            disableButtons();
        }
        else
        {
            enableButtons();
        }
    }


    public void addCombatLogMessage(string message)
    {
        GameObject newMessage = Instantiate(messagePrefab, combatLogContent.transform);

        TextMeshProUGUI messageTMP = newMessage.GetComponent<TextMeshProUGUI>();
        messageTMP.text = message;
    }


    /* BUTTON ON CLICK LISTENERS */

    private void onMeleeAttackButtonClicked()
    {
        Debug.Log("Player uses Melee attack.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Melee);
    }


    private void onSkillCardButton1Clicked()
    {
        Debug.Log("Player uses Skill Card 1.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.One);
    }

    private void onSkillCardButton2Clicked()
    {
        Debug.Log("Player uses Skill Card 2.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Two);
    }


    private void onSkillCardButton3Clicked()
    {
        Debug.Log("Player uses Skill Card 3.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Three);
    }


    private void onSkillCardButton4Clicked()
    {
        Debug.Log("Player uses Skill Card 4.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Four);
    }

    private void onInventoryButtonClicked()
    {
        UIManager.Instance.showUI(UIManager.UI.Inventory);
        Debug.Log("Inventory button clicked!");
    }


    private void onRunAwayButtonClicked()
    {
        Debug.Log("Run away button clicked!");
        BattleManager.Instance.runAway();
    }


    /* PRIVATE FUNCTIONS */


    private void updatePlayerStamina()
    {
        playerStaminaFillBar.fillAmount = PlayerManager.Instance.getStamina() / 100.0f;
    }

    private void updateHealthBars()
    {
        // Set the player health bar slider to their health percentage from 1.0f to 0.0f (1.0f is full).
        playerHealthFillBar.fillAmount = PlayerManager.Instance.calculateHealthPercent();

        // Set the enemy health bar slider to their health percentage from 1.0f to 0.0f (1.0f is full).
        enemyHealthFillBar.fillAmount = Mathf.Abs(BattleManager.Instance.getEnemyHealthPercentage());

    }


    private void updateRoundCounter(int round)
    {
        roundTMP.text = "Round: " + round;
    }


    private void enableButtons()
    {
        meleeAttackButton.interactable = true;

        int playerStamina = PlayerManager.Instance.getStamina();
        SkillCardSO skillCardOne = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.One);
        if (skillCardOne != null && skillCardOne.getStaminaCost() <= playerStamina)
        {
            skillCardButton1.interactable = true;
        }

        SkillCardSO skillCardTwo = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Two);
        if (skillCardTwo != null && skillCardTwo.getStaminaCost() <= playerStamina)
        {
            skillCardButton2.interactable = true;
        }

        SkillCardSO skillCardThree = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Three);
        if (skillCardThree != null && skillCardThree.getStaminaCost() <= playerStamina)
        {
            skillCardButton3.interactable = true;
        }

        SkillCardSO skillCardFour = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Four);
        if (skillCardFour != null && skillCardFour.getStaminaCost() <= playerStamina)
        {
            skillCardButton4.interactable = true;
        }

        inventoryButton.interactable = true;
        runAwayButton.interactable   = true;
    }


    private void disableButtons()
    {
        meleeAttackButton.interactable = false;
        
        skillCardButton1.interactable = false;
        skillCardButton2.interactable = false;
        skillCardButton3.interactable = false;
        skillCardButton4.interactable = false;

        inventoryButton.interactable = false;
        runAwayButton.interactable   = false;
    }


    private void updateTurnText()
    {
        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Player)  // Player's turn.
        {
            turnIndicatorTMP.text = "Player's Turn";
        }
        else                                            // Enemy's turn.
        {
            turnIndicatorTMP.text = "Enemy's Turn";
        }
    }


    private void updateTurnCounters()
    {
        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Player)  // Player's turn.
        {
            playerTurnTMP.text = "Turn: " + BattleManager.Instance.getPlayerTurnCount();
        }
        else                                                                        // Enemy's turn.
        {
            enemyTurnTMP.text = "Turn: " + BattleManager.Instance.getEnemyTurnCount();
        }
    }


    private void updateSkillCardButtonText()
    {
        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.One) == null)
        {
            skillCard1TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard1 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.One);
            skillCard1TMP.text = $"{skillCard1.getItemName()}";
            skillCard1CostTMP.text = $"Stamina Cost: {skillCard1.getStaminaCost()}";
        }
        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Two) == null)
        {
            skillCard2TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard2 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Two);
            skillCard2TMP.text = $"{skillCard2.getItemName()}";
            skillCard2CostTMP.text = $"Stamina Cost: {skillCard2.getStaminaCost()}";
        }
        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Three) == null)
        {
            skillCard3TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard3 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Three);
            skillCard3TMP.text = $"{skillCard3.getItemName()}";
            skillCard3CostTMP.text = $"Stamina Cost: {skillCard3.getStaminaCost()}";
        }
        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Four) == null)
        {
            skillCard4TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard4 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Four);
            skillCard4TMP.text = $"{skillCard4.getItemName()}";
            skillCard4CostTMP.text = $"Stamina Cost: {skillCard4.getStaminaCost()}";
        }
    }


    private void addOnClickListeners()
    {
        meleeAttackButton.onClick.AddListener(onMeleeAttackButtonClicked);
        skillCardButton1.onClick.AddListener(onSkillCardButton1Clicked);
        skillCardButton2.onClick.AddListener(onSkillCardButton2Clicked);
        skillCardButton3.onClick.AddListener(onSkillCardButton3Clicked);
        skillCardButton4.onClick.AddListener(onSkillCardButton4Clicked);
        inventoryButton.onClick.AddListener(onInventoryButtonClicked);
        runAwayButton.onClick.AddListener(onRunAwayButtonClicked);
    }


    private IEnumerator criticalHitCoroutine()
    {
        criticalHitTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        criticalHitTMP.gameObject.SetActive(false);
    }
}
