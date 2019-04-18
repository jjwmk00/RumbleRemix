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
        _startingSinglePlayer = false;                                                      // Not starting singleplayer on start
        _startingVersus = false;                                                            // Not starting versus on start
        _quitGame = false;                                                                  // Not quitting game on start

        _ps4Controller = false;                                                             // PS4 controller is false on load
        _xboxController = false;                                                            // Xbox controller is false on load

        _mainMenuFadeValue = 0;                                                             // Set main menu value to 0

        _mainMenuAudio = GetComponent<AudioSource>();                                       // Get the main menu audio source
        _mainMenuAudio.volume = 0;                                                          // Set the main menu volume to 0 
        _mainMenuAudio.clip = _mainMenuMusic;                                               // Set the audio clip to be the main menu music
        _mainMenuAudio.loop = true;                                                         // Loop over the music clip w/o fade
        _mainMenuAudio.Play();                                                              // Play the main menu music

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

        _mainMenuAudio.volume += _mainMenuFadeSpeed * Time.deltaTime;                               // Increase the volume by the fade speed

        _mainMenuFadeValue += _mainMenuFadeSpeed * Time.deltaTime;                                  // Increase fade value by the fade speed

        // So we get 1 as the fade value 
        if (_mainMenuFadeValue > 1)                                                                 // If the fade value is greater than 1
        {
            _mainMenuFadeValue = 1;                                                                 // Set the value to 1
        }

        if (_mainMenuFadeValue == 1)                                                                // If the fade value is equal to 1
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuIdle;                         // Then make the sate idle
        }
    }

    private void MainMenuFadeOut()
    {
        Debug.Log("MainMenuFadeOut");

        _mainMenuAudio.volume -= _mainMenuFadeSpeed * Time.deltaTime;                               // Decrease the volume by the fade speed 

        _mainMenuFadeValue -= _mainMenuFadeSpeed * Time.deltaTime;                                  // Decrease the fade value by the fade speed
        
        // So we get 0 as the fade value
        if (_mainMenuFadeValue < 0)                                                                 // If the fade value is less than 0
        {
            _mainMenuFadeValue = 0;                                                                 // Set it to zero
        }

        // Starting single player mode
        if (_mainMenuFadeValue == 0 && _startingSinglePlayer == true)                               // If the fade value is 0 and starting single player mode
        {
            SceneManager.LoadScene("CharacterSelect");                                              // Load the character select screen
        }


    }

    private void MainMenuIdle()
    {
        Debug.Log("Main Menu is Idle");

        if (_startingSinglePlayer || _quitGame == true)                                             // If starting single player game or quitting game
        {
            _mainMenuController = MainMenu.MainMenuController.MainMenuFadeOut;                      // Make the state fade out
        }
    }

    

    private void MainMenuButtonPress()
    {
        Debug.Log("Main Menu Button has been pressed.");

        GUI.FocusControl(_mainMenuButtons[_selectedButton]);

        switch(_selectedButton)
        {
            // Single Player
            case 0:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);                             // Play button audio clip
                _startingSinglePlayer = true;                                                      // Starting Single Player mode
                break;
            // Versus
            case 1:
                _mainMenuAudio.PlayOneShot(_mainMenuStartButtonAudio);                             // Play button audio clip
                _startingVersus = true;                                                            // Start Versus mode
                break;
            // Quit Game
            case 2:
                _mainMenuAudio.PlayOneShot(_mainMenuQuitButtonAudio);                              // Play button audio clip
                _quitGame = true;                                                                  // Quit Game
                break;
        }
    }
}


