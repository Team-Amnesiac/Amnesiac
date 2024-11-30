using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    // Button variables initialized in the Unity interface.
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button exitButton;


    void Start()
    {
        UIManager.Instance.setUI(UIManager.UI.MainMenu, this);
        // Initialize the playButton onClick listener.
        playButton.onClick.AddListener(playClick);
        // Initialize the controlsButton onClick listener.
        controlsButton.onClick.AddListener(controlsButtonOnClick);
        // Initialize the exitButton onClick listener.
        exitButton.onClick.AddListener(exitButtonOnClick);
    }


    /***************************************************************************

        Function:       playClick

        Description:    The onClick listener for the playButton.
                        Loads the next scene.

    ***************************************************************************/
    private void playClick()
    {
        // TODO: LOAD THE (LOADING_SCENE / FIRST_LEVEL_SCENE)
    }


    /***************************************************************************
    
        Function:       controlsButtonOnClick

        Description:    The onClick listener for the controlsButton.
                        Enables the controlsUI GameObject and disables 
                        all child GameObjects of the MainMenuUI.

    ***************************************************************************/
    private void controlsButtonOnClick()
    {
        // Enable the controlsUI GameObject.
        UIManager.Instance.showUI(UIManager.UI.Controls);

        // Disable the MainMenuUI GameObject.
        UIManager.Instance.hideUI(UIManager.UI.MainMenu);
    }


    /***************************************************************************

        Function:       exitButtonOnClick

        Description:    The onClick listener for the exitButton.
                        Closes the application.

    ***************************************************************************/
    private void exitButtonOnClick()
    {
        Application.Quit();
    }
}
