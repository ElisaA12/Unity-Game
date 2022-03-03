using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PAUSA : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    public GUIStyle stileBottoni;
    private bool menuPrincipaleOn;
    // Start is called before the first frame update
    void Start()
    {
        menuPrincipaleOn = true;
        //nuovoGiocatore = new BaseGiocatore();
    }

    void OnGUI()
    {
        if (menuPrincipaleOn)
        {
            //creiamo un area di lavoro a meta' dello schermo sottratto meta' della grandezza del quale dovra' essere
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 50, 500, 500));

            if (GUILayout.Button("Resume", stileBottoni))
            {
                pauseMenuUI.SetActive(false);
                Time.timeScale = 1f;
                // m_audio.Stop();
            }
            /*
            if (GUILayout.Button("OPTIONS", stileBottoni)) //scompaiono tutti i bottoni e ne compagliono altri
            {
                menuPrincipaleOn = false;
                menuOpzioniOn = true;
            }*/
            if (GUILayout.Button("Restart", stileBottoni))
            {

                Application.LoadLevel("Mwnu");
            }    // m_audio.Stop();
            if (GUILayout.Button("QUIT", stileBottoni))
            {
                Application.Quit();
                Quit();
            }
            GUILayout.EndArea();

        }
        

    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }
}
