using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class CollectibleSetEntryController : MonoBehaviour
{
    [SerializeField] private CollectibleSetSO collectibleSet;
    [SerializeField] private TextMeshProUGUI  collectibleTMP;
    [SerializeField] private Button[] spriteButtons;


    public void setCollectibleSet(CollectibleSetSO collectibleSet)
    {
        this.collectibleSet = collectibleSet;
        updateSetText();
    }

    private void updateSetText()
    {
        CollectibleManager.Set setType = collectibleSet.getSetType();
        int collectedCount = CollectibleManager.Instance.getCollectedCount(setType);
        collectibleTMP.text =
            $"{setType.ToString()}: {collectedCount} / {CollectibleManager.Instance.getSetSize(setType)}";

        foreach (Button button in spriteButtons)
        {
            if (collectedCount == 0)
            {
                return;
            }

            button.interactable = true;
            collectedCount--;
        }
    }
}
