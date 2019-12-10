using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject panel;
    [SerializeField] GameObject UIpanel;
    
    public void StartGame()
       {
           Time.timeScale = 1;
           SceneManager.LoadScene("LevelScene");
       }
   
       public void OpenCredits()
       {
           panel.SetActive(true);
       }
       
       public void CloseCredits()
       {
           panel.SetActive(false);
       }
       
       public void OpenPauseMenu()
       {
           Time.timeScale = 0;
           panel.SetActive(true);
           UIpanel.SetActive(false);
       }
       
       public void ClosePauseMenu()
       {
           panel.SetActive(false);
           UIpanel.SetActive(true);
           Time.timeScale = 1;
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