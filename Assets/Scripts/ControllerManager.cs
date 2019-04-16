using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script detects whether it is an Xbox or PS4 controller
/// Shows a popup if cannot detect an above controller 
/// Or none at all
/// </summary>

public class ControllerManager : MonoBehaviour
{
    public Texture2D _controllerNotDetected;                                    // Create slot in inspector to assign controller warning text

    public bool _ps4Controller;                                                 // Create a bool for when a PS4 Controller is connected
    public bool _xboxController;                                                // Create a bool for when a Xbox Controller is connected
    public bool _controllerDetected;                                            // Create a bool for when a controller is connected

    public static bool _startUpFinished;                                        // Create a bool for when start up is finished

    private void Awake()
    {
        _ps4Controller = false;                                                 // Set PS4 controller to false on load
        _xboxController = false;                                                // Set Xbox controller to false on load
        _controllerDetected = false;                                            // Set Controller Detected to false on load

        _startUpFinished = false;                                               // Start up finished is set to false on load
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);                                                // Dont destroy game object when loading a new scene
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void LateUpdate()
    {
        string[] _joyStickNames = Input.GetJoystickNames();                     // Get joystick names and store them in an array

        for (int _js = 0; _js < _joyStickNames.Length; _js++)                   // Increse counter by one based on the legth of the joystick names array
        {
            if (_joyStickNames [_js].Length == 19)                              // If joystick names is equal to 19 then its a PS4 Controller
            {
                _ps4Controller = true;                                          // Set PS4 controller to true
                _controllerDetected = true;                                     // Controller has been detected
            }

            if (_joyStickNames [_js].Length == 33)                              // If joystic names is equal to 19 then its a Xbox Controller
            {
                _xboxController = true;                                         // Set Xbox controller to true
                _controllerDetected = true;                                     // Controller has been detected
            }

            if (_joyStickNames [_js].Length != 0)                               // If joystick names are not equal to 0
            {   
                return;                                                         // Return and do nothing
            }

            if (string.IsNullOrEmpty(_joyStickNames [_js]))                     // If sting array is empty then controller is not connected
            {
                _controllerDetected = false;                                    // Set controller detected to false
            }
        }
    }

    private void OnGUI()

    {
        // If startup finished is false
        if (_startUpFinished == false )                                          
        {
            return;                                                             // Return and do nothing
        }

        // If the controller is detected
        if (_controllerDetected == true)                                         
        {
            return;                                                             // Return and do nothing
        }

        // If the controller is not detected
        if (_controllerDetected == false)                                        
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), _controllerNotDetected);      
        }
    }
}
