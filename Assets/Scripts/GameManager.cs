using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private void Awake()
    {
        Cursor.visible = false;                                                // Set the cursor to not be visible
        Cursor.lockState = CursorLockMode.Locked;                              // Lock the cursor
    }
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);                                               // Dont destroy the game object when loading new scenes
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
