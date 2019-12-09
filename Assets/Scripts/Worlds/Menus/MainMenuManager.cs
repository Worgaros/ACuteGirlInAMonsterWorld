using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("CollectibleObjectTestScene");
    }

    public void OpenCredits()
    {
        SceneManager.LoadScene("Credits");
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
