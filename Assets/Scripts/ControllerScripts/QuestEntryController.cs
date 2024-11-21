using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestEntryController : MonoBehaviour
{
    [SerializeField] private QuestSO quest;
    [SerializeField] private TextMeshProUGUI questTMP;


    public void setQuest(QuestSO quest)
    {
        this.quest = quest;
        updateQuestText();
    }

    private void updateQuestText()
    {
        questTMP.text = $"{quest.questType.ToString()}: {quest.questName}";
    }
}
