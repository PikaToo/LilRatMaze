using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Quits game is escape is pressed.
    void Update()
    {
        if (Input.GetKey("escape")) {
            Application.Quit();
        }   
    }
}
