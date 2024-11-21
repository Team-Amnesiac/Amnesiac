using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    public List<Quest> activeQuests;
    public List<Quest> completedQuests;

    public GameObject questLogUI;
    public GameObject activeQuestParent;
    public GameObject completedQuestParent;
    public GameObject questEntryPrefab;

    public GameObject notificationBar;
    private TextMeshProUGUI notificationText;

    [Header("Quest Assets")]
    public Quest firstQuest;
    public Quest secondQuest;
    public Quest thirdQuest;


    private void Awake()
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


   public void TalkToKeeper()
    {
        if (!activeQuests.Contains(firstQuest) && !completedQuests.Contains(firstQuest))
        {
            AddQuest(firstQuest);
        }
        else if (completedQuests.Contains(firstQuest) && !activeQuests.Contains(secondQuest))
        {
            AddQuest(secondQuest);
        }
        else if (completedQuests.Contains(secondQuest) && !activeQuests.Contains(thirdQuest))
        {
            AddQuest(thirdQuest);
        }
    }


    private void AddQuest(Quest newQuest)
    {
        activeQuests.Add(newQuest);
        ShowNotification($"New Quest(s) in quest log!");
        UpdateQuestLog();
        Debug.Log($"New quest added: {newQuest.questName}");
    }


    public void UpdateQuestProgress(Quest quest)
    {
        if (activeQuests.Contains(quest) && !quest.isCompleted)
        {
            Debug.Log($"[QuestManager] Updating progress for quest: {quest.questName}");
            quest.requiredAmount--;
            Debug.Log($"[QuestManager] Remaining items for quest: {quest.requiredAmount}");

            if (quest.requiredAmount <= 0)
            {
                Debug.Log($"[QuestManager] Quest completed: {quest.questName}");
                quest.isCompleted = true;
                completedQuests.Add(quest);
                activeQuests.Remove(quest);

                UpdateQuestLog();
            }
        }
        else
        {
            Debug.Log($"[QuestManager] Quest progress update skipped. Quest not active or already completed: {quest.questName}");
        }
    }


    private void UpdateQuestLog()
    {
        Debug.Log("[QuestManager] Updating Quest Log UI...");

        ClearQuestEntries(activeQuestParent);
        ClearQuestEntries(completedQuestParent);

        foreach (Quest quest in activeQuests)
        {
            Debug.Log($"[QuestManager] Adding to Active Quests UI: {quest.questName}");
            AddQuestToUI(activeQuestParent, quest.questName, "Active Quest");
        }

        foreach (Quest quest in completedQuests)
        {
            Debug.Log($"[QuestManager] Adding to Completed Quests UI: {quest.questName}");
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


    private void ShowNotification(string message)
    {
        if (notificationBar == null || notificationText == null) return;

        notificationBar.SetActive(true);
        notificationText.text = message;
        Invoke(nameof(HideNotification), 3f);
    }


    private void HideNotification()
    {
        if (notificationBar != null)
        {
            notificationBar.SetActive(false);
        }
    }
}
