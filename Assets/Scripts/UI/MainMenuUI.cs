using UnityEngine;
using UnityEngine.UI;


public class MainMenuUI : MonoBehaviour
{
    // Button variables initialized in the Unity interface.
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject controlsUI;


    private void Awake()
    {
        // Initialize the playButton onClick listener.
        playButton.onClick.AddListener(playClick);
        // Initialize the controlsButton onClick listener.
        controlsButton.onClick.AddListener(controlsClick);
        // Initialize the exitButton onClick listener.
        exitButton.onClick.AddListener(exitClick);
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
    
        Function:       controlsClick

        Description:    The onClick listener for the controlsButton.
                        Enables the controlsUI GameObject and disables 
                        all child GameObjects of the MainMenuUI.

    ***************************************************************************/
    private void controlsClick()
    {
        // Enable the controlsUI GameObject.
        controlsUI.SetActive(true);

        // Disable the MainMenuUI GameObject.
        gameObject.SetActive(false);
    }


    /***************************************************************************

        Function:       exitClick

        Description:    The onClick listener for the exitButton.
                        Closes the application.

    ***************************************************************************/
    private void exitClick()
    {
        Application.Quit();
    }
}
