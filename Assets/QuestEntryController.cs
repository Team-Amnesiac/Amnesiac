using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestEntryController : MonoBehaviour
{
    [SerializeField] private Quest quest;
    [SerializeField] private TextMeshProUGUI questTMP;


    public void setQuest(Quest quest)
    {
        this.quest = quest;
        updateQuestText();
    }

    private void updateQuestText()
    {
        questTMP.text = $"{quest.questType.ToString()}: {quest.questName}";
    }
}
