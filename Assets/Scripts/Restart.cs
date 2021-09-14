using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public void restartApp()
    {
        SceneManager.LoadScene(1); //load scene 1
    }

    public void requitApp()
    {
        Application.Quit(); //quit the application
    }
}
