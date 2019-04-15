using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]                         // Add audio source when attaching the script
public class LandingScreen : MonoBehaviour
{
    // Landing Screen inspector variables
    public Texture2D _landingScreenBackground;                  // Create slot in inspector to assign landing screen background image
    public Texture2D _landingScreenText;                        // Create slot in inspector to assign landing screen text

    // Landing Screen audio inspector variables
    private AudioSource _landingScreenAudio;                    // Defines name for auido source component
    public AudioClip _landingScreenMusic;                       // Create slot in inspector to assign landing screen music

    // Variables for fading items on the landing screen
    private float _landingScreenFadeValue;                      // Defines the fade value
    private float _landingScreenFadeSpeed = 0.15f;              // Defines the fade speed

    private LandingScreenController _landingScreenController;   // Defines the naming conventions for the splash screen contollers

    // Defines the states for the splash screens
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

    private void StartCoroutine()
    {
        throw new NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LandingScreenFadeIn()
    {
        Debug.Log("LandingScreenFadeIn");
    }

    private void LandingScreenFadeOut()
    {
        Debug.Log("LandingScreenFadeOut");
    }
}
