using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class ControllerWarning : ControllerManager
{
    // Controller Warning inspector texture variables
    public Texture2D _controllerWarningBackround;                              // Create slot in inspector to assign background image 
    public Texture2D _controllerWarningText;                                   // Create slot in inspector to assign warning text
    public Texture2D _controllerDetectedText;                                  // Create slot in inspector to show controller detected text

    // Variables for fading items on the warning screen
    public float _controllerWarningFadeValue;                                  // Define the fade value
    public float _landingScreenFadeSpeed = 0.45f;                              // Define the fade speed 

    private bool _controllerConditionsMet;                                     // Define if the controller conditions are met for game to continue

    // Start is called before the first frame update
    void Start()
    {
        _controllerWarningFadeValue = 1;                                        // Fade value equals 1 on start(visible)
        _controllerConditionsMet = false;                                       // Conditions have not been met on start up
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
