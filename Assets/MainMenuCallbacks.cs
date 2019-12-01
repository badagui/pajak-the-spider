using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuCallbacks : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level Design 1");
    }

    public void Options()
    {
        print("Load options scene");
        //SceneManager.LoadScene("");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Continue()
    {
        print("Continue Game");
        //SceneManager.LoadScene("");
    }
}
