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
    public float _controllerWarningFadeSpeed = 0.45f;                          // Define the fade speed 

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
        if (_controllerDetected == true)                                        // If the controller has been detected
        {
            StartCoroutine("WaitForMenuToLoad");
        }

        if (_controllerConditionsMet == false)                                  // If the controller conditions have not been met
        {
            return;                                                             // Return and do nothing 
        }

        if (_controllerConditionsMet == true)                                   // If the controller conditions have been met
        {
            _controllerWarningFadeValue -= 
                _controllerWarningFadeSpeed * Time.deltaTime;                   // Decrease Fade value
        }

        // So we don't get a negative number
        if (_controllerWarningFadeValue < 0)                                    // If the fade value is less than 0
        {
            _controllerWarningFadeValue = 0;                                    // Set it to equal 0
        }

        if (_controllerWarningFadeValue == 0)                                   // If the fade value is equal to zero 
        {
            _startUpFinished = true;                                            // Startup has been finished
            SceneManager.LoadScene("MainMenu");                                 // Load the next scene
        }
    }

    private IEnumerator WaitForMainMenuToLoad()
    {
        yield return new WaitForSeconds(4);                                      // Wait for this many seconds 

        _controllerConditionsMet = true;                                         // Controller conditions have been met
    }
}
