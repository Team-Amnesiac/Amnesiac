using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestEntryController : MonoBehaviour
{
    [SerializeField] private QuestSO _questSo;
    [SerializeField] private TextMeshProUGUI questTMP;


    public void setQuest(QuestSO questSo)
    {
        this._questSo = questSo;
        updateQuestText();
    }

    private void updateQuestText()
    {
        questTMP.text = $"{_questSo.questType.ToString()}: {_questSo.questName}";
    }
}
