using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]                                                     // Add an audio source
public class MainMenu : MonoBehaviour
{

    public int _selectedButton = 0;                                                         // Defines selected GUI button
    public float _timeBetweenPress = 0.1f;                                                  // Delay inbetween button presses
    public float _timeDelay;

    public float _mainMenuInputTimer;                                                       // Defines the input time for the main menu
    public float _mainMenuInputDelay;                                                       // Defines the input delay for the main menu

    public Texture2D _mainMenuBackground;                                                   // Creates a slot in the inspector to assign main menu background

    private AudioSource _mainMenuAudio;                                                     // Naming for main menu audio component
    public AudioClip _mainMenuMusic;                                                        // Creates slot in the inpector to assign main menu music
    public AudioClip _mainMenuStartButtonAudio;                                             // Creates slot in the inspector to assign start button audio
    public AudioClip _mainMenuQuitButtonAudio;                                              // Creates slot in the inspector to assign quit button audio

    private float _mainMenuFadeValue;                                                       // Define the value of main menu fade
    private float _mainMenuFadeSpeed = 0.15f;                                               // Define the speed at which the main menu fades

    public float _mainMenuButtonWidth = 100f;                                               // Defines the main menu button height
    public float _mainMenuButtonHeight = 100f;                                              // Defines the main manu button width 
    public float _mainMenuGUIOffset = 10f;                                                  // Defines the main menu offset

    private bool _startingSinglePlayer;                                                     // Check if playing single player mode
    private bool _startingVersus;                                                           // Check if starting versus mode
    private bool _quitGame;                                                                 // Check if quitting game


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


