using UnityEngine;

public class KeeperInteraction : MonoBehaviour
{
    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.Instance.showUI(UIManager.UI.Keeper); // Show Keeper UI
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

    public void TalkToKeeper()
    {
        QuestManager.Instance.TalkToKeeper();
        Debug.Log("Player interacted with Keeper and received a quest.");
    }

    ///
    /// Closes the Keeper UI and resumes gameplay.
    ///
    public void CloseKeeperUI()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        Time.timeScale = 1;
        Debug.Log("Keeper UI closed. Back to the game world.");
    }

    ///
    /// Opens the Shop UI and hides the Keeper UI.
    ///
    public void OpenShopUI()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        UIManager.Instance.showUI(UIManager.UI.Shop);
        Debug.Log("Shop UI opened, Keeper UI hidden.");
    }

    ///
    /// Opens the Collectibles UI and hides the Keeper UI.
    ///
    public void OpenCollectiblesUI()
    {
        UIManager.Instance.hideUI(UIManager.UI.Keeper);
        UIManager.Instance.showUI(UIManager.UI.Collectibles);
        Debug.Log("Collectibles UI opened, Keeper UI hidden.");
    }

    ///
    /// Returns to Keeper UI from Shop UI.
    ///
    public void BackToKeeperFromShop()
    {
        UIManager.Instance.hideUI(UIManager.UI.Shop);
        UIManager.Instance.showUI(UIManager.UI.Keeper);
        Debug.Log("Returned to Keeper UI from Shop UI.");
    }

    ///
    /// Returns to Keeper UI from Collectibles UI.
    ///
    public void BackToKeeperFromCollectibles()
    {
        UIManager.Instance.hideUI(UIManager.UI.Collectibles);
        UIManager.Instance.showUI(UIManager.UI.Keeper);
        Debug.Log("Returned to Keeper UI from Collectibles UI.");
    }
}
