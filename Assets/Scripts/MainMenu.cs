using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]                                                     // Add an audio source
public class MainMenu : MonoBehaviour
{

    private bool _ps4Controller;                                                            // Bool for if a PS4 controller is connected 
    private bool _xboxController;                                                           // Bool for if a Xbox controller is connected

    private string[] _mainMenuButtons = new string[]                                        // Create an array of main menu buttons
    {
        "_singlePlayer",
        "_versus",
        "_quit"
    };

    private MainMenuController _mainMenuController;                                         // Defines naming conventions for main menu controller

    private enum MainMenuController                                                         // Defines the states main menu can exist in
    {
        MainMenuFadeIn = 0,
        MainMenuIdle = 1,
        MainMenuFadeOut = 2
        
    }

    // Start is called before the first frame update
    void Start()
    {
        _ps4Controller = false;                                                             // PS4 controller is false on load
        _xboxController = false;                                                            // Xbox controller is false on load

        _mainMenuController = MainMenu.MainMenuController.MainMenuFadeIn;                   // State equals fade in on load

        StartCoroutine("MainMenuManager");                                                  // Start Main Menu manager on load
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator MainMenuManager()
    {
        while(true)
        {
            switch( _mainMenuController)
            {
                case MainMenuController.MainMenuFadeIn:
                    MainMenuFadeIn();
                    break;
                case MainMenuController.MainMenuIdle:
                    MainMenuIdle();
                    break;
                case MainMenuController.MainMenuFadeOut:
                    MainMenuFadeOut();
                    break;
            }

            yield return null;
        }
    }


    private void MainMenuFadeIn()
    {
        Debug.Log("MainMenuFadeIn");
    }

    private void MainMenuIdle()
    {
        Debug.Log("Main Menu is Idle");
    }

    private void MainMenuFadeOut()
    {
        Debug.Log("MainMenuFadeOut");
    }

    private void MainMenuButtonPress()
    {
        Debug.Log("Menu button has been pressed");
    }
}


