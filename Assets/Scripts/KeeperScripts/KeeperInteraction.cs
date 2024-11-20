using UnityEngine;

public class KeeperInteraction : MonoBehaviour
{
    public GameObject keeperUI;
    public GameObject questsUI;
    public GameObject shopUI;
    public GameObject collectiblesUI;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            OpenKeeperUI();
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

    private void OpenKeeperUI()
    {
        keeperUI.SetActive(true);
        PauseGame();
    }

    public void CloseKeeperUI()
    {
        keeperUI.SetActive(false);
        ResumeGame();
    }

    //to be implemented: dialogue boxes with keeper with the ability to exit conversation that would return player to the keeperUI
    public void OpenTalkUI()
    {
        keeperUI.SetActive(true);
    }

    public void OpenShopUI()
    {
        keeperUI.SetActive(false);
        shopUI.SetActive(true);
    }

    public void OpenCollectiblesUI()
    {
        keeperUI.SetActive(false);
        collectiblesUI.SetActive(true);
    }

    public void ReturnToKeeperUI()
    {
        shopUI.SetActive(false);
        collectiblesUI.SetActive(false);
        keeperUI.SetActive(true);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        Debug.Log("Game Paused");
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        Debug.Log("Game Resumed");
    }
     public void TalkToKeeper()
    {
        QuestManager.Instance.TalkToKeeper();
        Debug.Log("Player interacted with Keeper and received a quest.");
    }

}
