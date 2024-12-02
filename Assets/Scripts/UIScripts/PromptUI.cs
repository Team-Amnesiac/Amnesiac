using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PromptUI : MonoBehaviour
{
    [SerializeField] private GameObject promptUI;
    [SerializeField] private TextMeshProUGUI promptTMP;
    [SerializeField] private const float promptShowTime = 5.0f;

    private void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Prompt, this);
        UIManager.Instance.hideUI(UIManager.UI.Prompt);
    }

    public void showPrompt(string message)
    {
        promptTMP.text = message;
        UIManager.Instance.showUI(UIManager.UI.Prompt);
        Invoke(nameof(hidePrompt), promptShowTime);
    }

    private void hidePrompt()
    {
        UIManager.Instance.hideUI(UIManager.UI.Prompt);
        promptTMP.text = "";
    }
}
