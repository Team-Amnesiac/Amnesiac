using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] List<Quest> activeQuests;
    [SerializeField] List<Quest> completedQuests;

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
        UIManager.Instance.newNotification($"New Quest(s) in quest log!");
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
            }
        }
        else
        {
            Debug.Log($"[QuestManager] Quest progress update skipped. Quest not active or already completed: {quest.questName}");
        }
    }
    

    /* GET FUNCTIONS */

    public List<Quest> getActiveQuests()
    {
        return activeQuests;
    }


    public List<Quest> getCompletedQuests()
    {
        return completedQuests;
    }
}
