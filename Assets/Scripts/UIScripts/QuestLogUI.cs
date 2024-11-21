using UnityEngine;
using TMPro;


public class QuestLogUI : MonoBehaviour
{
    public GameObject activeQuestParent;
    public GameObject completedQuestParent;
    public GameObject questEntryPrefab;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.QuestLog, this);
        UIManager.Instance.hideUI(UIManager.UI.QuestLog);
    }


    public void show()
    {
        gameObject.SetActive(true);
    }


    public void hide()
    {
        gameObject.SetActive(false);
    }


    public void UpdateQuestsUI()
    {
        ClearQuestEntries(activeQuestParent);
        ClearQuestEntries(completedQuestParent);

        foreach (Quest quest in QuestManager.Instance.activeQuests)
        {
            AddQuestToUI(activeQuestParent, quest.questName, "Active Quest");
        }

        foreach (Quest quest in QuestManager.Instance.completedQuests)
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