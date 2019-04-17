using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Script loads Scene filled with test
/// to be rendered  on initilization of the game
/// </summary>

[RequireComponent(typeof(AudioSource))]                         // Add audio source when attaching the script
public class LandingScreen : MonoBehaviour
{
    // Landing Screen inspector variables
    public Texture2D _landingScreenBackground;                  // Create slot in inspector to assign landing screen background image
    public Texture2D _landingScreenTitleText;                   // Create slot in inspector to assign landing screen title text
    public Texture2D _landingScreenSubText;                     // Create slot in inspector to assing landing screen subtext

    // Landing Screen audio inspector variables
    private AudioSource _landingScreenAudio;                    // Defines name for auido source component
    public AudioClip _landingScreenMusic;                       // Create slot in inspector to assign landing screen music

    // Variables for fading items on the landing screen
    private float _landingScreenFadeValue;                      // Defines the fade value
    private float _landingScreenFadeSpeed = 0.35f;              // Defines the fade speed

    private LandingScreenController _landingScreenController;   // Defines the naming conventions for the splash screen contollers

    // Defines the states for the landing screen
    private enum LandingScreenController
    {
        LandingScreenFadeIn = 0,
        LandingScreenFadeOut = 1
    }

    private void Awake()
    {
        _landingScreenFadeValue = 0;                            // Fade value equals zero on startup
    }

    // Start is called before the first frame update
    void Start()
    {
        // Only usable with a gamepad
        Cursor.visible = false;                                 // Set cursor visible state to false
        Cursor.lockState = CursorLockMode.Locked;               // Lock the cursor

        // Landing Screen Music
        _landingScreenAudio = GetComponent<AudioSource>();      // Landing Screen audio equals the audio source

        _landingScreenAudio.volume = 0;                         // Audio volume equals zero on startup
        _landingScreenAudio.clip = _landingScreenMusic;         // Audio clip equals the splash screen music
        _landingScreenAudio.loop = true;                        // Set audio to loop 
        _landingScreenAudio.Play();                             // Play audio

        _landingScreenController = LandingScreen.LandingScreenController.LandingScreenFadeIn; // Fade in on startup

        StartCoroutine("LandingScreenManager");                 // Start LandingScreenManager function
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Handles the cases for whether the Landing screen fades in or out
    private IEnumerator LandingScreenManager()
    {
        while(true)
        {
            switch (_landingScreenController)
            {
                case LandingScreenController.LandingScreenFadeIn:
                    LandingScreenFadeIn();
                    break;
                case LandingScreenController.LandingScreenFadeOut:
                    LandingScreenFadeOut();
                    break;
            }
            yield return null;
        }
    }

    private void LandingScreenFadeIn()
    {
        Debug.Log("LandingScreenFadeIn");

        _landingScreenAudio.volume += _landingScreenFadeSpeed * Time.deltaTime; // Increase volume by fade speed
        _landingScreenFadeValue += _landingScreenFadeSpeed * Time.deltaTime;    // Increase fade value by the fade speed 
        
        // To make sure we get the precise value of 1
        if(_landingScreenFadeValue > 1)                                         // If fade value is greater than 1
        {
            _landingScreenFadeValue = 1;                                        // Then set fade value to one                   
        }
        
        if(_landingScreenFadeValue == 1)                                        // If fade value equals one
        {
            _landingScreenController = LandingScreen.LandingScreenController.LandingScreenFadeOut;  // Set splash screen controller to equal landing screen fade out
        }
    }

    private void LandingScreenFadeOut()
    {
        Debug.Log("LandingScreenFadeOut");

        _landingScreenAudio.volume -= _landingScreenFadeSpeed * Time.deltaTime; // Decrease voulme by fade speed
        _landingScreenFadeValue -= _landingScreenFadeSpeed * Time.deltaTime;    // Decrease fade value by the fade speed

        // To make sure we get the precise value of 0
        if(_landingScreenFadeValue < 0)                                         // If the value is less than 0
        {
            _landingScreenFadeValue = 0;                                        // Then set the value to 0
        }

        // To load the next scene after fade out
        if(_landingScreenFadeValue == 0)                                        // If fade value equals 0
        {
            SceneManager.LoadScene("ControllerWarning");                        // Load the controller warning scene
        }
    }

    // Draws texture on screen based on screen height and width
    private void OnGUI()
    {
        // Draws the background
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _landingScreenBackground);

        GUI.color = new Color(1, 1, 1, _landingScreenFadeValue);                // GUI color is equal to black transparency is the screen fade value

        // Draws the Title
        GUI.DrawTexture(new Rect((Screen.width/4), (Screen.height/3), (Screen.width / 2), (Screen.height / 5)), _landingScreenTitleText, ScaleMode.ScaleToFit);

        // Draws the sub text
        GUI.DrawTexture(new Rect(0, 0, (Screen.width / 8), (Screen.height / 11)), _landingScreenSubText, ScaleMode.ScaleToFit);
    }
}
