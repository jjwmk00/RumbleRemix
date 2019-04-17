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
    public float _controllerWarningFadeSpeed = .15f;                           // Define the fade speed 

    // Define if the controller conditions are met for game to continue
    private bool _controllerConditionsMet;                                     

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
            StartCoroutine("WaitForMainMenuToLoad");
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
        yield return new WaitForSeconds(2);                                      // Wait for this many seconds 

        _controllerConditionsMet = true;                                         // Controller conditions have been met
    }

    private void OnGUI()
    {
        
        // Draws the background based on screen height and width
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerWarningBackround);
        
        // Sets the color to Black (RGB)  with the alpha as the fade value
        GUI.color = new Color(1, 1, 1, _controllerWarningFadeValue);

        // If the controller hasnt been detected 
        if (_controllerDetected == false)
        {
            // Draws the warning text based on the screen height and width
            GUI.DrawTexture(new Rect((Screen.width / 4), (Screen.height / 4), (Screen.width / 2), (Screen.height / 2)), _controllerWarningText, ScaleMode.ScaleToFit);

        }

        // If a controller has been detcted
        if (_controllerDetected == true)
        {
            // Draw the controller detected text based on the screen width and height
            GUI.DrawTexture(new Rect((Screen.width/3), (Screen.height/4), (Screen.width/3), (Screen.height/8)), _controllerDetectedText);
        }
    }

}
