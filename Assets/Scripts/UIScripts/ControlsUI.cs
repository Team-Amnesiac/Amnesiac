using UnityEngine;
using UnityEngine.UI;


public class ControlsUI : MonoBehaviour
{
    [SerializeField] private Button     backButton;


    void Start()
    {
        // Initialize the backButton onClick listener 
        backButton.onClick.AddListener(backButtonOnClick);

        UIManager.Instance.setUI(UIManager.UI.Controls, this);
        // Hide the ControlsUI GameObject.
        UIManager.Instance.hideUI(UIManager.UI.Controls);
    }


    /***************************************************************************

        Function:       backButtonOnClick

        Description:    The onClick listener for the controlsButton.
                        Enables the controlsUI GameObject and disables
                        all child GameObjects of the MainMenuUI.

    ***************************************************************************/
    private void backButtonOnClick()
    {
        // Hide the ControlsUI GameObject.
        UIManager.Instance.hideUI(UIManager.UI.Controls);

        if (GameManager.Instance.getGameState() == GameManager.GameState.Title)
        {
            UIManager.Instance.showUI(UIManager.UI.MainMenu);
        }
        else
        {
            //UIManager.Instance.showUI(UIManager.UI.PauseMenu);
        }
    }
}
