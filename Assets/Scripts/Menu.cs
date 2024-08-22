using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    void Start ()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    } 
    public void OnPlayButton ()
    {
        SceneManager.LoadScene(1);
    }
    
    public void OnQuitButton ()
    {
        Application.Quit();
    }

}
