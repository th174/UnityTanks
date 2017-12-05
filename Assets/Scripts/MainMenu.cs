using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public InputField hostname;


    public void CreateGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneParameters.isHost = true;
    }

    public void JoinGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneParameters.isHost = false;
        SceneParameters.serverHostName = hostname.text ;
    }

    public void OpenOptionMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
