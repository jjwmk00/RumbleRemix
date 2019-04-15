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
    // Start is called before the first frame update
    void Start()
    {
        
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
