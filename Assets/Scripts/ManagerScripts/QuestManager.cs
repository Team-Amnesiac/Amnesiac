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
        firstQuest.isCompleted = false;
        secondQuest.isCompleted = false;
        thirdQuest.isCompleted = false;
    }


    public void talkToKeeper()
    {
        if (playerReadyForFirstQuest())
        {
            addQuest(firstQuest);
        }
        else if (playerReadyForSecondQuest())
        {
            addQuest(secondQuest);
        }
        else if (playerReadyForThirdQuest())
        {
            addQuest(thirdQuest);
        }
    }


    public void addRelic(RelicSO relic)
    {
        QuestSO relicQuest = relic.getRelatedQuest();
        if (activeQuests.Contains(relicQuest))
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

    public QuestSO FirstQuest => firstQuest;
    public QuestSO SecondQuest => secondQuest;
    public QuestSO ThirdQuest => thirdQuest;

    public List<QuestSO> getActiveQuests()
    {
        return activeQuests;
    }


    public List<QuestSO> getCompletedQuests()
    {
        return completedQuests;
    }

    public QuestData SaveState()
    {
        return new QuestData
        {
            activeQuests = activeQuests.ConvertAll(q => q.questName),
            completedQuests = completedQuests.ConvertAll(q => q.questName)
        };
    }

    public void LoadState(QuestData data)
    {
        activeQuests = data.activeQuests.ConvertAll(name => FindQuestByName(name));
        completedQuests = data.completedQuests.ConvertAll(name => FindQuestByName(name));
    }

    private QuestSO FindQuestByName(string name)
    {
        return Resources.Load<QuestSO>($"Quests/{name}");
    }

    private bool playerReadyForFirstQuest()
    {
        return !activeQuests.Contains(firstQuest) &&
               !completedQuests.Contains(firstQuest);
    }

    private bool playerReadyForSecondQuest()
    {
        return completedQuests.Contains(firstQuest) &&
               !activeQuests.Contains(secondQuest) &&
               !completedQuests.Contains(secondQuest);
    }

    private bool playerReadyForThirdQuest()
    {
        return completedQuests.Contains(secondQuest) &&
               !activeQuests.Contains(thirdQuest) &&
               !completedQuests.Contains(thirdQuest);
    }
}
