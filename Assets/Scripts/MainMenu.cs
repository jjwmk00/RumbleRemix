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
    public float _mainMenuInputDelay = 0.4f;                                                       // Defines the input delay for the main menu

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

        // Detecting what kind of controller is connected
        string[]_joyStickNames = Input.GetJoystickNames();                                  // Storing joystick names in a string array 

        for(int _js = 0; _js < _joyStickNames.Length; _js ++)                               // Loop over the length of joystick names array
        {
            if (_joyStickNames[_js].Length == 0)                                            // If empty or no controller connected
            {
                return;                                                                     // Return and do nothing
            }

            if (_joyStickNames[_js].Length == 19)                                           // If its a PS4 controller
            {
                Debug.Log("PS4 Controller Connected");

                _ps4Controller = true;                                                      // Set PS4 controller
            }

            if (_joyStickNames[_js].Length == 33)                                           // If its a Xbox controller 
            {
                Debug.Log("Xbox Controller Connected");

                _xboxController = true;                                                     // Set Xbox controller to true
            }
        }



        // Create a delay between button presses
        if (_mainMenuInputTimer > 0)                                                        // If the input timer is greater than 0
        {    
            _mainMenuInputTimer -= 1f * Time.deltaTime;                                     // Decrease the timer 
        }


        
        // To stay in the range of each of the buttons
        // Can not move farther up than this button
        // SINGLE PLAYER BUTTON
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 0)                         // If input equals vertical(positive or up) and the selected button equals SINGLE PLAYER
        {
            return;                                                                         // Retrun and do nothing
        }

   
        // MOVING UP THE MENU
        // From VERSUS to SINGLE PLAYER
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 1)                         // If input equals vertical(positive or up) and the selected button equals VERSUS
        {
            // If the delay is in effect
            if(_mainMenuInputTimer > 0)                                                     // If the input timer is greater than 0
            {
                return;
            }

            // ELSE

            // Reset the delay
            _mainMenuInputTimer = _mainMenuInputDelay;                                      // Set the input timer equal to the input delay

            // Moved to the next button
            _selectedButton = 0;                                                            // Set selected button equal to the SINGLE PLAYER
            }

        // From QUIT to VERSUS
        if (Input.GetAxis("Vertical") > 0f && _selectedButton == 2)                         // If the input equals vertical(postitive or up) and the selected button equals QUIT
        {
            // If the delay is in effect
            if(_mainMenuInputTimer > 0)                                                     // If the input timer is greater than 0
            {
                return;
            }

            // ELSE

            // Reset the delay
            _mainMenuInputTimer = _mainMenuInputDelay;                                      // Set the input timer equal to the input delay

            // Moved to the next button
            _selectedButton = 1;                                                            // Set the selected button equal to VERSUS
        }

        // QUIT BUTTON
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 2)                         // If input equals vertical(negative or down) and the selected button equals QUIT
        {
            return;                                                                         // Return and do nothing
        }

        // MOVING DOWN THE MENU
        // From SINGLE PLAYER to VERSUS
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 0)                         // If input equals vertical(negative or down) and the selected button equals SINGLE PLAYER
        {
          // If the delay is in effect
          if(_mainMenuInputTimer > 0)                                                       // If the input timer is greater than 0
            {
                return;                                                                     // Return and do nothing
            }

            // ELSE

            // Reset the delay
            _mainMenuInputTimer = _mainMenuInputDelay;                                      // Set the input timer equal to the input delay

            // Moved to the next button
            _selectedButton = 1;                                                            // Set the selected button to VERSUS
        }

        // From VERSUS to QUIT
        if (Input.GetAxis("Vertical") < 0f && _selectedButton == 1)                         // If the input equals vertical(negative or down) and the selected button equals VERSUS
        {
            // If the delay is in effect
            if(_mainMenuInputTimer > 0)                                                     // If the input timer is greater than 0
            {
                return;
            }

            // ELSE

            // Reset the delay
            _mainMenuInputTimer = _mainMenuInputDelay;                                       // Set the input timer equal to the input delay 

            // Moved to the next button
            _selectedButton = 2;                                                             // Set the selected button to QUIT
        }

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

        GUI.FocusControl(_mainMenuButtons[_selectedButton]);                                       // Focus on the selected GUI button

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

    private void OnGUI()
    {

        if(Time.deltaTime >= _timeDelay && (Input.GetButton("Fire1")))                             // If time is greater than or equal to time delay AND equals "Fire1"
        {
            StartCoroutine("MainMenuButtonPress");                                                 // Start button press function
            _timeDelay = Time.deltaTime + _timeBetweenPress;                                       // Make time delay equal current time + time btw button presses
        }
        
        // Draw the background texture by the width and height of the screen
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _mainMenuBackground);

        // GUI color is equal to black transparency is the screen fade value
        GUI.color = new Color(1, 1, 1, _mainMenuFadeValue);

        // Draw the button group
        GUI.BeginGroup(new Rect(
            Screen.width / 2 - _mainMenuButtonWidth / 2,                                           // X position
            Screen.height / 1.5f,                                                                  // Y position
            _mainMenuButtonWidth,                                                                  // Width
            _mainMenuButtonHeight * 3 + _mainMenuGUIOffset                                         // Height
            ));

        
        // SINGLE PLAYER
        // Set a string to a GUI button
        GUI.SetNextControlName("_singlePlayer");
        // Create the single player button
        if (GUI.Button(new Rect(
            0, 0,                                                                                  // X & Y positon
            _mainMenuButtonWidth,                                                                  // Width
            _mainMenuButtonHeight),                                                                // Height
            "Single Player")){                                                                     // Name
            _selectedButton = 0;                                                                   // Selected button equals 0(Single Player)
            MainMenuButtonPress();                                                                 // Call button press function
        }

        // Set a string
       

        // End button group
        GUI.EndGroup();

        // Focus control over the buttons if a controller is connected
        if(_ps4Controller == true || _xboxController == true)                                       // If a PS4 controller or an Xbox controller
        {
            GUI.FocusControl(_mainMenuButtons[_selectedButton]);                                    // Focus equals main menu selected button
        }
    }
}


