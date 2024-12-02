using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

// This class manages the UI representation of a single quest entry in the quest system.
// It updates the displayed quest details based on the assigned quest data.

public class QuestEntryController : MonoBehaviour
{
    // Reference to the Quest Scriptable Object associated with this entry.
    [SerializeField] private QuestSO quest;
    // Reference to the TextMeshProUGUI component used to display the quest's name and type.
    [SerializeField] private TextMeshProUGUI questTMP;

    // Assigns a quest to this entry and updates its displayed details.
    public void setQuest(QuestSO quest)
    {
        this.quest = quest; // Set the quest data for this entry.
        updateQuestText(); // Update the UI text to reflect the quest's details.
    }

    // Updates the quest text displayed in the UI.
    private void updateQuestText()
    {
        questTMP.text = $"{quest.questType.ToString()}: {quest.questName}"; 
        // Format the quest's type and name for display in the UI.
    }
}
