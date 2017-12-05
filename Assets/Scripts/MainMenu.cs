using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void CreateGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneParameters.isHost = true;
    }

    public void JoinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneParameters.isHost = false;
    }

    public void OpenOptionMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
