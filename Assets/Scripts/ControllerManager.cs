using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (_controllerDetected == true)                                        // If controller is detected 
        {
            return;                                                             // Do nothing and return
        }
    }

    private void OnGUI()
    {
       
    }
}
