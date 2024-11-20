/* using UnityEngine;
using TMPro;

public class QuestUI : MonoBehaviour
{
    public GameObject activeQuestParent;
    public GameObject completedQuestParent;
    public GameObject questEntryPrefab;

    private QuestManager questManager;

    private void OnEnable()
    {
        questManager = QuestManager.Instance;

        if (questManager != null)
        {
            UpdateQuestsUI();
        }
    }

    public void UpdateQuestsUI()
    {
        ClearQuestEntries(activeQuestParent);
        ClearQuestEntries(completedQuestParent);

        foreach (Quest quest in questManager.activeQuests)
        {
            AddQuestToUI(activeQuestParent, quest.questName, "Active Quest");
        }

        foreach (Quest quest in questManager.completedQuests)
        {
            AddQuestToUI(completedQuestParent, quest.questName, "Completed Quest");
        }
    }

    private void ClearQuestEntries(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    private void AddQuestToUI(GameObject parent, string questName, string status)
    {
        GameObject questEntry = Instantiate(questEntryPrefab, parent.transform);
        TextMeshProUGUI questText = questEntry.GetComponentInChildren<TextMeshProUGUI>();
        questText.text = $"{status}: {questName}";
    }
}
 */