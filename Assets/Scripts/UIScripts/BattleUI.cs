using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// This class manages the battle UI, updating and displaying relevant information during combat.
// It handles player input, skill cards, health/stamina bars, and turn-based mechanics.

public class BattleUI : MonoBehaviour
{
    // UI text elements for displaying various battle-related information.
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

    // Buttons for performing actions during combat.
    [SerializeField] private Button meleeAttackButton;
    [SerializeField] private Button skillCardButton1;
    [SerializeField] private Button skillCardButton2;
    [SerializeField] private Button skillCardButton3;
    [SerializeField] private Button skillCardButton4;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button runAwayButton;

    // Health and stamina bar UI elements.
    [SerializeField] private Image playerHealthFillBar;
    [SerializeField] private Image playerStaminaFillBar;
    [SerializeField] private Image enemyHealthFillBar;

    // Combat log container for displaying battle messages.
    [SerializeField] private GameObject combatLogContent;
    // Prefab used for creating combat log messages.
    [SerializeField] private GameObject messagePrefab;

    /* UNITY FUNCTIONS */

    // Initializes the battle UI and sets up button listeners.
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Battle, this); // Register the battle UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.Battle); // Hide the battle UI initially.
        addOnClickListeners(); // Attach listeners to the buttons.
    }

    // Cleans up listeners when the object is destroyed.
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

    // Resets the combat log and prepares the UI for hiding.
    public void prepareBattleHide()
    {
        foreach (Transform combatLogMessageTransform in combatLogContent.transform) // Clear all combat log messages.
        {
            Destroy(combatLogMessageTransform.gameObject);
        }
    }

    // Updates all visuals in the battle UI based on the current game state.
    public void updateVisuals()
    {
        criticalHitTMP.gameObject.SetActive(false); // Hide critical hit text initially.
        updateSkillCardButtonText(); // Update skill card button text and stamina costs.
        updateTurnText(); // Update the turn indicator text.
        updateTurnCounters(); // Update the turn counters for player and enemy.
        updatePlayerStamina(); // Update the player's stamina bar.
        updateHealthBars(); // Update the health bars for the player and enemy.
        updateRoundCounter(BattleManager.Instance.getRoundNumber()); // Update the round counter.

        if (BattleManager.Instance.isCriticalHit()) // If a critical hit occurred, show the notification.
        {
            StartCoroutine(criticalHitCoroutine());
        }

        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Enemy) // If it's the enemy's turn.
        {
            disableButtons(); // Disable player interaction buttons.
        }
        else
        {
            enableButtons(); // Enable player interaction buttons.
        }
    }

    // Adds a message to the combat log.
    public void addCombatLogMessage(string message)
    {
        GameObject newMessage = Instantiate(messagePrefab, combatLogContent.transform); // Create a new message object.
        TextMeshProUGUI messageTMP = newMessage.GetComponent<TextMeshProUGUI>(); // Get the TMP component.
        messageTMP.text = message; // Set the message text.
    }

    /* BUTTON ON CLICK LISTENERS */

    private void onMeleeAttackButtonClicked()
    {
        Debug.Log("Player uses Melee attack.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Melee); // Trigger a melee attack.
    }

    private void onSkillCardButton1Clicked()
    {
        Debug.Log("Player uses Skill Card 1.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.One); // Trigger Skill Card 1 attack.
    }

    private void onSkillCardButton2Clicked()
    {
        Debug.Log("Player uses Skill Card 2.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Two); // Trigger Skill Card 2 attack.
    }

    private void onSkillCardButton3Clicked()
    {
        Debug.Log("Player uses Skill Card 3.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Three); // Trigger Skill Card 3 attack.
    }

    private void onSkillCardButton4Clicked()
    {
        Debug.Log("Player uses Skill Card 4.");
        BattleManager.Instance.playerAttack(PlayerManager.SkillSlot.Four); // Trigger Skill Card 4 attack.
    }

    private void onInventoryButtonClicked()
    {
        UIManager.Instance.showUI(UIManager.UI.Inventory); // Show the inventory UI.
        Debug.Log("Inventory button clicked!");
    }

    private void onRunAwayButtonClicked()
    {
        Debug.Log("Run away button clicked!");
        BattleManager.Instance.runAway(); // Trigger the run-away functionality.
    }

    /* PRIVATE FUNCTIONS */

    // Updates the player's stamina bar.
    private void updatePlayerStamina()
    {
        playerStaminaFillBar.fillAmount = PlayerManager.Instance.getStamina() / 100.0f; // Update stamina percentage.
    }

    // Updates the health bars for the player and enemy.
    private void updateHealthBars()
    {
        playerHealthFillBar.fillAmount = PlayerManager.Instance.calculateHealthPercent(); // Update player's health bar.
        enemyHealthFillBar.fillAmount = Mathf.Abs(BattleManager.Instance.getEnemyHealthPercentage()); // Update enemy's health bar.
    }

    // Updates the round counter in the UI.
    private void updateRoundCounter(int round)
    {
        roundTMP.text = "Round: " + round; // Display the current round number.
    }

    // Enables buttons for player interaction during their turn.
    private void enableButtons()
    {
        meleeAttackButton.interactable = true; // Enable melee attack button.

        int playerStamina = PlayerManager.Instance.getStamina();
        // Check if skill cards can be used based on stamina and enable their respective buttons.
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

        inventoryButton.interactable = true; // Enable inventory button.
        runAwayButton.interactable   = true; // Enable run away button.
    }

    // Disables buttons for player interaction during the enemy's turn.
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

    // Updates the turn indicator text based on the current attacker.
    private void updateTurnText()
    {
        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Player) // Player's turn.
        {
            turnIndicatorTMP.text = "Player's Turn";
        }
        else // Enemy's turn.
        {
            turnIndicatorTMP.text = "Enemy's Turn";
        }
    }

    // Updates the turn counters for both the player and the enemy.
    private void updateTurnCounters()
    {
        if (BattleManager.Instance.getAttacker() == BattleManager.Attacker.Player) // Player's turn.
        {
            playerTurnTMP.text = "Turn: " + BattleManager.Instance.getPlayerTurnCount();
        }
        else // Enemy's turn.
        {
            enemyTurnTMP.text = "Turn: " + BattleManager.Instance.getEnemyTurnCount();
        }
    }

    // Updates the text on skill card buttons and their stamina costs.
    private void updateSkillCardButtonText()
    {
        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.One) == null) // Skill Card 1 is empty.
        {
            skillCard1TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard1 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.One);
            skillCard1TMP.text = $"{skillCard1.getItemName()}";
            skillCard1CostTMP.text = $"Stamina Cost: {skillCard1.getStaminaCost()}";
        }

        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Two) == null) // Skill Card 2 is empty.
        {
            skillCard2TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard2 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Two);
            skillCard2TMP.text = $"{skillCard2.getItemName()}";
            skillCard2CostTMP.text = $"Stamina Cost: {skillCard2.getStaminaCost()}";
        }

        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Three) == null) // Skill Card 3 is empty.
        {
            skillCard3TMP.text = "EMPTY";
        }
        else
        {
            SkillCardSO skillCard3 = PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Three);
            skillCard3TMP.text = $"{skillCard3.getItemName()}";
            skillCard3CostTMP.text = $"Stamina Cost: {skillCard3.getStaminaCost()}";
        }

        if (PlayerManager.Instance.getSkillCard(PlayerManager.SkillSlot.Four) == null) // Skill Card 4 is empty.
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

    // Attaches listeners to button click events.
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

    // Displays a critical hit notification for a short duration.
    private IEnumerator criticalHitCoroutine()
    {
        criticalHitTMP.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f); // Show the text for 3 seconds.
        criticalHitTMP.gameObject.SetActive(false);
    }
}
