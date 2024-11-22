using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

    [SerializeField] List<QuestSO> activeQuests;
    [SerializeField] List<QuestSO> completedQuests;

    [Header("Quest Assets")]
    public QuestSO FirstQuestSo;
    public QuestSO SecondQuestSo;
    public QuestSO ThirdQuestSo;


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
        if (!activeQuests.Contains(FirstQuestSo) && !completedQuests.Contains(FirstQuestSo))
        {
            AddQuest(FirstQuestSo);
        }
        else if (completedQuests.Contains(FirstQuestSo) && !activeQuests.Contains(SecondQuestSo))
        {
            AddQuest(SecondQuestSo);
        }
        else if (completedQuests.Contains(SecondQuestSo) && !activeQuests.Contains(ThirdQuestSo))
        {
            AddQuest(ThirdQuestSo);
        }
    }


    private void AddQuest(QuestSO newQuestSo)
    {
        activeQuests.Add(newQuestSo);
        UIManager.Instance.newNotification($"New Quest(s) in quest log!");
    }


    private void UpdateQuestProgress(QuestSO questSo)
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


    public void addRelic(RelicSO relic)
    {
        QuestSO relicQuest = relic.getRelatedQuest();
        if (activeQuests.Contains(relicQuest) && !relicQuest.isCompleted)
        {
            UpdateQuestProgress(relicQuest);
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
