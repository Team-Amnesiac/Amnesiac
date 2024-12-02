using System;
using UnityEngine;

public class KeeperInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    // Dialogue sets for different quest stages
    [SerializeField] private string[] preFirstQuestDialogue = {
        "Hey, You Must Be Thinking How Strange Of A Place You Have Waken Up In.",
        "As You Can See, The World Is Gone, And There Is No One Around, Except For Vile Creatures.",
        "However, We Got Each Other Now. But I Must Request You That There Are Matters That Are More Concerning",
        "In Case You Didn't Know, There Is A Codex-Relic Lying Somewhere In This Place, That Might Help Clear Things In Your Mind.",
        "Search For The Object That Resembles A Book And I Can Translate The Knowledge To Enrich Your Mind As To What Happened To Our World."
    };
    [SerializeField] private string[] preSecondQuestDialogue = {
        "Well Done! Seems Like You Found The First Codex-Relic Book",
        "Let's See What We Have Here.",
        "It seems that the world has been tarnished by the evil actions, but why?",
        "I am sorry to say this, but you have to find one more relic.",
        "This relic might be lost in the dark castle in the world called Noryx.",
        "Be mindful of stocking your inventory, as your journey in Noryx can be rather arduous.",
    };
    [SerializeField] private string[] preThirdQuestDialogue = {
        "Well Done Amnesiac! You have done well to bring back this Second Codex-Relic Book.",
        "Let's See What This Reveals.",
        "From What We Have Gathered, It seems like these evil actions were wished upon by the very people of this world.",
        "As Such, there were several evil benefactors that thrived on providing people means to destory themselves.",
        "As A Result, the people who seemed to have self-destructive tendencies were inclined to continue down this path.",
        "Which Ravaged Our World As A Result.",
        "This Relic Revealed A Lot. However, I will need you to find the final piece.",
        "This Final Piece Should Be Inside The Desert World Of Loikart.",
        "Don't Let The Harsh Environment Taunt You And Please Come Back In One Piece!"
    };

    private string[] currentDialogueArray;
    private int currentDialogueIndex = 0;

    private void Start()
    {
        UpdateDialogueArray();
    }


    private void Update()
    {
        if (playerInRange)
        {
            UIManager.Instance.newPrompt($"Press E to interact with the Keeper.");
        }
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.showUI(UIManager.UI.Keeper); // Show Keeper UI
            GameManager.Instance.setGameState(GameManager.GameState.Pause);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Player in range of The Keeper.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("Player left the range of The Keeper.");
        }
    }

    public void talkTo()
    {
        QuestManager.Instance.talkToKeeper(); // Handle quest progression
        StartDialogue();
        Debug.Log("Player interacted with Keeper and received or updated a quest.");
    }

    private void StartDialogue()
    {
        UIManager.Instance.getDialogueUI().onNextDialogue += DialogueUI_OnNextDialogue;
        DialogueUI_OnNextDialogue(this, EventArgs.Empty);
    }

    private void DialogueUI_OnNextDialogue(object sender, EventArgs e)
    {
        // Update dialogue array for the next interaction
        UpdateDialogueArray();

        if (currentDialogueIndex >= currentDialogueArray.Length)
        {
            currentDialogueIndex = 0;
            UIManager.Instance.getDialogueUI().onNextDialogue -= DialogueUI_OnNextDialogue;
            UIManager.Instance.hideUI(UIManager.UI.Dialogue);
            GameManager.Instance.setGameState(GameManager.GameState.Play);

        }
        else // Continue dialogue
        {
            // Show the next dialogue line
            UIManager.Instance.newDialogue(currentDialogueArray[currentDialogueIndex]);
            currentDialogueIndex++;
        }
    }

    private void UpdateDialogueArray()
    {
        var completedQuests = QuestManager.Instance.getCompletedQuests();

        if (completedQuests.Count == 0)
        {
            // Before any quests are completed, used pre-first quest dialogue
            currentDialogueArray = preFirstQuestDialogue;
        }
        else if (completedQuests.Contains(QuestManager.Instance.FirstQuest) &&
                 !completedQuests.Contains(QuestManager.Instance.SecondQuest))
        {
            // After completing the first quest but before the second
            currentDialogueArray = preSecondQuestDialogue;
        }
        else if (completedQuests.Contains(QuestManager.Instance.SecondQuest))
        {
            // After completing the second quest
            currentDialogueArray = preThirdQuestDialogue;
        }
        else
        {
            // Default fallback, uses pre-first quest dialogue if nothing else matches
            currentDialogueArray = preFirstQuestDialogue;
        }
    }
}
