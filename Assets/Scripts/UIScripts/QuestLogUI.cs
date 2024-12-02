using UnityEngine;
using TMPro;
using UnityEngine.UI;

// This class manages the Quest Log UI, allowing the player to view active and completed quests.

public class QuestLogUI : MonoBehaviour
{
    // Prefab used to create entries for each quest.
    [SerializeField] private GameObject questEntryPrefab;
    // Content container for active quests.
    [SerializeField] private GameObject activeQuestContent;
    // Content container for completed quests.
    [SerializeField] private GameObject completedQuestContent;
    // Button to close the Quest Log UI.
    [SerializeField] private Button closeQuestLogButton;

    // Initializes the Quest Log UI and attaches the close button listener.
    void Start()
    {
        closeQuestLogButton.onClick.AddListener(closeQuestLogButtonOnClick); // Attach the close button listener.
        UIManager.Instance.setUI(UIManager.UI.QuestLog, this); // Register the Quest Log UI with the UIManager.
        UIManager.Instance.hideUI(UIManager.UI.QuestLog); // Hide the Quest Log UI initially.
    }

    // Prepares the Quest Log UI for display by populating active and completed quests.
    public void prepareQuestLogShow()
    {
        prepareActiveQuestLogShow(); // Populate the active quests section.
        prepareCompletedQuestLogShow(); // Populate the completed quests section.
    }

    // Cleans up the Quest Log UI by clearing active and completed quests.
    public void prepareQuestLogHide()
    {
        prepareActiveQuestLogHide(); // Clear the active quests section.
        prepareCompletedQuestLogHide(); // Clear the completed quests section.
    }

    // Handles the close button click, hiding the Quest Log UI.
    private void closeQuestLogButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.QuestLog); // Hide the Quest Log UI.
    }

    // Populates the active quests section with quest entries.
    private void prepareActiveQuestLogShow()
    {
        foreach (QuestSO activeQuest in QuestManager.Instance.getActiveQuests()) // Iterate through active quests.
        {
            GameObject obj = Instantiate(questEntryPrefab, activeQuestContent.transform); // Create a new entry.
            QuestEntryController controller = obj.GetComponent<QuestEntryController>();

            controller.setQuest(activeQuest); // Assign the quest data to the entry.
        }
    }

    // Populates the completed quests section with quest entries.
    private void prepareCompletedQuestLogShow()
    {
        foreach (QuestSO completedQuest in QuestManager.Instance.getCompletedQuests()) // Iterate through completed quests.
        {
            GameObject obj = Instantiate(questEntryPrefab, completedQuestContent.transform); // Create a new entry.
            QuestEntryController controller = obj.GetComponent<QuestEntryController>();

            controller.setQuest(completedQuest); // Assign the quest data to the entry.
        }
    }

    // Clears the active quests section by destroying its children.
    private void prepareActiveQuestLogHide()
    {
        foreach (Transform child in activeQuestContent.transform) // Iterate through all child objects.
        {
            Destroy(child.gameObject); // Destroy each child.
        }
    }

    // Clears the completed quests section by destroying its children.
    private void prepareCompletedQuestLogHide()
    {
        foreach (Transform child in completedQuestContent.transform) // Iterate through all child objects.
        {
            Destroy(child.gameObject); // Destroy each child.
        }
    }
}
