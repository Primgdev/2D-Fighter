using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartApp()
    {
        SceneManager.LoadScene(1); //load scene 1
    }

    public void QuitApp()
    {
        Application.Quit(); //quit the application
    }

    public void instruction()
    {
        SceneManager.LoadScene(6);
    }
}
