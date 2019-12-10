using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
       {
           SceneManager.LoadScene("LevelScene");
       }
   
       public void OpenCredits()
       {
           SceneManager.LoadScene("Credits");
       }
       
       public void QuitGame()
       {
           Application.Quit();
       }
       
       public void BackToMainMenu()
       {
           SceneManager.LoadScene("MainMenu");
       }
   }