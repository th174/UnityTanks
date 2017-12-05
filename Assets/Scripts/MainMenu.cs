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

    public void JoinGame(TMPro.TextMeshProUGUI text)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        SceneParameters.isHost = false;
        SceneParameters.serverHostName = text.text ;
    }

    public void OpenOptionMenu()
    {

    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
