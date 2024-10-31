using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject previousUI;


    private void Awake()
    {
        // Initialize the backButton onClick listener 
        backButton.onClick.AddListener(backClick);

        // Hide the ControlsUI game object.
        gameObject.SetActive(false);
    }


    /***************************************************************************

        Function:       backClick

        Description:    The OnClick listener for the controlsButton.
                        Enables the controlsUI GameObject and disables
                        all child GameObjects of the MainMenuUI.

    ***************************************************************************/
    private void backClick()
    {
        // Hide the ControlsUI game object.
        gameObject.SetActive(false);
        // Show the previous UI game object.
        previousUI.SetActive(true);
    }
}
