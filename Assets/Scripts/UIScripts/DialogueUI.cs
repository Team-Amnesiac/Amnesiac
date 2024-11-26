using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dialogueTMP;
    public event EventHandler onNextDialogue;


    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.Dialogue, this);
        UIManager.Instance.hideUI(UIManager.UI.Dialogue);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            onNextDialogue?.Invoke(this, EventArgs.Empty);
        }
    }


    public void newDialogue(string message)
    {
        dialogueTMP.text = message;
    }
}
