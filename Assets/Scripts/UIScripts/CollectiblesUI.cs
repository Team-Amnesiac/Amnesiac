using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectiblesUI : MonoBehaviour
{
    [SerializeField] private GameObject trophySetEntryPrefab;
    [SerializeField] private GameObject activeSetContent;
    [SerializeField] private GameObject completedSetContent;
    [SerializeField] private Button     exitButton;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Collectibles, this);
        UIManager.Instance.hideUI(UIManager.UI.Collectibles);
        exitButton.onClick.AddListener(exitButtonOnClick);
    }


    public void prepareCollectiblesShow()
    {
        prepareActiveCollectiblesShow();
        prepareCompletedCollectiblesShow();
    }


    public void prepareCollectiblesHide()
    {
        prepareActiveCollectiblesHide();
        prepareCompletedCollectiblesHide();
    }


    private void prepareActiveCollectiblesShow()
    {
        foreach (CollectibleSetSO activeSet in CollectibleManager.Instance.getActiveSets())
        {
            if (activeSet.getSetType() == CollectibleManager.Set.Trophy)
            {
                GameObject obj = Instantiate(trophySetEntryPrefab, activeSetContent.transform);
                CollectibleSetEntryController controller = obj.GetComponent<CollectibleSetEntryController>();

                controller.setCollectibleSet(activeSet);
            }
            else
            {
                Debug.Log("INVALID COLLECTIBLE SET IN IN ACTIVE SET");
            }
        }
    }


    private void prepareCompletedCollectiblesShow()
    {
        foreach (CollectibleSetSO completedSet in CollectibleManager.Instance.getCompletedSets())
        {
            if (completedSet.getSetType() == CollectibleManager.Set.Trophy)
            {
                GameObject obj = Instantiate(trophySetEntryPrefab, completedSetContent.transform);
                CollectibleSetEntryController controller = obj.GetComponent<CollectibleSetEntryController>();

                controller.setCollectibleSet(completedSet);
            }
            else
            {
                Debug.Log("INVALID COLLECTIBLE SET IN IN ACTIVE SET");
            }
        }
    }


    private void prepareActiveCollectiblesHide()
    {
        foreach (Transform child in activeSetContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void prepareCompletedCollectiblesHide()
    {
        foreach (Transform child in completedSetContent.transform)
        {
            Destroy(child.gameObject);
        }
    }


    private void exitButtonOnClick()
    {
        UIManager.Instance.hideUI(UIManager.UI.Collectibles);
        UIManager.Instance.showUI(UIManager.UI.Keeper);
    }
}
