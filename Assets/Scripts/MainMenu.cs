using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]                                                     // Add an audio source
public class MainMenu : MonoBehaviour
{

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
        
    }

    // Update is called once per frame
    void Update()
    {
        
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


