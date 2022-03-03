using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuPausa : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public static bool comandi = false;
    public GameObject pauseMenuUI;
    public GameObject comandiMenuUI;

    void Update()
    {
        if ( Input.GetKeyUp(KeyCode.Escape) && !comandi)
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
                
            }

        }
        
            if (Input.GetKeyUp(KeyCode.Q) && GameIsPaused)
            {
                Application.Quit();
                UnityEditor.EditorApplication.isPlaying = false;
            }
            if (Input.GetKeyUp(KeyCode.C) && GameIsPaused)
            {

                pauseMenuUI.SetActive(false);
                comandiMenuUI.SetActive(true);
                comandi = true;


            }
            if (comandi)
            {
                if (Input.GetKeyUp(KeyCode.Escape) && comandi)
                {
                    comandiMenuUI.SetActive(false);
                    pauseMenuUI.SetActive(true);
                    comandi = false;

                }
            }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        comandiMenuUI.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = true;


    }

}
