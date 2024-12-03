using System;
using UnityEngine;

// This class handles player interactions with "The Keeper" NPC in the game.
// It manages dialogue, quest progression, and player proximity detection.
public class KeeperInteraction : MonoBehaviour
{
    // Tracks whether the player is within interaction range of The Keeper.
    private bool playerInRange = false;
    private bool hasPromptShown = false;

    // Dialogue sets for different stages of quest progression.
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

    // Tracks the current dialogue set and index.
    private string[] currentDialogueArray;
    private int currentDialogueIndex = 0;

    // Unity's Start method, initializes the dialogue array.
    private void Start()
    {
        UpdateDialogueArray(); // Set the initial dialogue based on quest progression.
    }

    // Unity's Update method, checks for player interaction.
    private void Update()
    {
        if (playerInRange && !hasPromptShown) // If the player is within range and the prompt hasn't been shown.
        {
            UIManager.Instance.newPrompt(this.gameObject, $"Press E to interact with the Keeper."); // Show the interaction prompt.
            hasPromptShown = true; // Mark the prompt as shown.
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.E)) // If the player presses 'E' while in range.
        {
            UIManager.Instance.showUI(UIManager.UI.Keeper); // Display the Keeper's UI.
            UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the prompt UI.
            GameManager.Instance.setGameState(GameManager.GameState.Pause); // Pause the game while interacting.
            hasPromptShown = false; // Reset the prompt so it can be shown again if necessary.
        }

        if (!playerInRange && hasPromptShown) // If the player moves out of range and the prompt was shown.
        {
            UIManager.Instance.hideUI(UIManager.UI.Prompt); // Hide the prompt UI.
            hasPromptShown = false; // Reset the prompt state for future interactions.
        }
    }

    // Detects when the player enters The Keeper's interaction range.
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object is the player.
        {
            playerInRange = true; // Mark the player as being in range.
            Debug.Log("Player in range of The Keeper."); // Log the event.
        }
    }

    // Detects when the player exits The Keeper's interaction range.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the object is the player.
        {
            playerInRange = false; // Mark the player as no longer being in range.
            Debug.Log("Player left the range of The Keeper."); // Log the event.
        }
    }

    // Handles player interaction with The Keeper.
    public void talkTo()
    {
        QuestManager.Instance.talkToKeeper(); // Notify the QuestManager of the interaction.
        StartDialogue(); // Begin the dialogue sequence.
        Debug.Log("Player interacted with Keeper and received or updated a quest."); // Log the interaction.
    }

    // Starts the dialogue sequence.
    private void StartDialogue()
    {
        UIManager.Instance.getDialogueUI().onNextDialogue += DialogueUI_OnNextDialogue; 
        // Subscribe to the dialogue progression event.
        DialogueUI_OnNextDialogue(this, EventArgs.Empty); // Trigger the first dialogue line.
    }

    // Handles the dialogue progression logic.
    private void DialogueUI_OnNextDialogue(object sender, EventArgs e)
    {
        UpdateDialogueArray(); // Update the dialogue array based on quest progression.

        if (currentDialogueIndex >= currentDialogueArray.Length) // Check if the dialogue has ended.
        {
            currentDialogueIndex = 0; // Reset the dialogue index.
            UIManager.Instance.getDialogueUI().onNextDialogue -= DialogueUI_OnNextDialogue; 
            // Unsubscribe from the dialogue progression event.
            UIManager.Instance.hideUI(UIManager.UI.Dialogue); // Hide the dialogue UI.
            GameManager.Instance.setGameState(GameManager.GameState.Play); // Resume the game.
        }
        else // If there are more dialogue lines to show.
        {
            UIManager.Instance.newDialogue(currentDialogueArray[currentDialogueIndex]); 
            // Display the next dialogue line.
            currentDialogueIndex++; // Move to the next line.
        }
    }

    // Updates the dialogue array based on completed quests.
    private void UpdateDialogueArray()
    {
        var completedQuests = QuestManager.Instance.getCompletedQuests(); // Get the list of completed quests.

        if (completedQuests.Count == 0) // If no quests are completed.
        {
            currentDialogueArray = preFirstQuestDialogue; // Use the pre-first quest dialogue.
        }
        else if (completedQuests.Contains(QuestManager.Instance.FirstQuest) &&
                 !completedQuests.Contains(QuestManager.Instance.SecondQuest)) 
        // If the first quest is completed but not the second.
        {
            currentDialogueArray = preSecondQuestDialogue; // Use the pre-second quest dialogue.
        }
        else if (completedQuests.Contains(QuestManager.Instance.SecondQuest) && !completedQuests.Contains(QuestManager.Instance.ThirdQuest))
        {
            currentDialogueArray = preThirdQuestDialogue; // Use the pre-third quest dialogue.
        }
        else if (completedQuests.Contains(QuestManager.Instance.ThirdQuest))
        {
            // Default fallback, uses pre-first quest dialogue if nothing else matches
            currentDialogueArray = preFirstQuestDialogue;
            Time.timeScale = 1.0f;
            SceneLoader.Instance.loadScene(SceneLoader.Scene.Ending);
        }
    }
}
