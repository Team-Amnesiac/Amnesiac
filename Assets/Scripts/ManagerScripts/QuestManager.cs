using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] List<QuestSO> activeQuests;
    [SerializeField] List<QuestSO> completedQuests;

    [Header("Quest Assets")]
    [SerializeField] private QuestSO firstQuest;
    [SerializeField] private QuestSO secondQuest;
    [SerializeField] private QuestSO thirdQuest;


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


    public void talkToKeeper()
    {
        if (!activeQuests.Contains(firstQuest) && !completedQuests.Contains(firstQuest))
        {
            addQuest(firstQuest);
        }
        else if (completedQuests.Contains(firstQuest) && !activeQuests.Contains(secondQuest))
        {
            addQuest(secondQuest);
        }
        else if (completedQuests.Contains(secondQuest) && !activeQuests.Contains(thirdQuest))
        {
            addQuest(thirdQuest);
        }
    }


    public void addRelic(RelicSO relic)
    {
        QuestSO relicQuest = relic.getRelatedQuest();
        if (activeQuests.Contains(relicQuest) && !relicQuest.isCompleted)
        {
            updateQuestProgress(relicQuest);
        }
    }


    public void addCollectible(CollectibleSO collectible)
    {
        QuestSO collectibleQuest = collectible.getRelatedQuest();
        if (activeQuests.Contains(collectibleQuest) && !collectibleQuest.isCompleted)
        {
            updateQuestProgress(collectibleQuest);
        }
    }


    private void addQuest(QuestSO newQuestSo)
    {
        activeQuests.Add(newQuestSo);
        UIManager.Instance.newNotification($"New Quest(s) in quest log!");
    }


    private void updateQuestProgress(QuestSO questSo)
    {
        if (activeQuests.Contains(questSo) && !questSo.isCompleted)
        {
            Debug.Log($"[QuestManager] Updating progress for quest: {questSo.questName}");
            questSo.requiredAmount--;
            Debug.Log($"[QuestManager] Remaining items for quest: {questSo.requiredAmount}");

            if (questSo.requiredAmount <= 0)
            {
                Debug.Log($"[QuestManager] Quest completed: {questSo.questName}");
                questSo.isCompleted = true;
                completedQuests.Add(questSo);
                activeQuests.Remove(questSo);
            }
        }
        else
        {
            Debug.Log($"[QuestManager] Quest progress update skipped. Quest not active or already completed: {questSo.questName}");
        }
    }


    /* GET FUNCTIONS */

    public List<QuestSO> getActiveQuests()
    {
        return activeQuests;
    }


    public List<QuestSO> getCompletedQuests()
    {
        return completedQuests;
    }
}
