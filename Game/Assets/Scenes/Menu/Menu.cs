using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    public GUIStyle stileBottoni;
    //public GUIStyle stileBottoni;
    //opzioni
    public GameObject Guida;

    public AudioSource m_audio;

    private bool menuPrincipaleOn;
    private bool menuOpzioniOn;
    // Start is called before the first frame update
    void Start()
    {
        m_audio.Play();
        menuPrincipaleOn = true;
        menuOpzioniOn = false;
        //nuovoGiocatore = new BaseGiocatore();
    }

    void OnGUI()
    {
        if (menuPrincipaleOn)
        {
            //creiamo un area di lavoro a meta' dello schermo sottratto meta' della grandezza del quale dovra' essere
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 550, (Screen.height / 2) - 50, 500, 500));

            //creiamo i bottoni

            if (GUILayout.Button("START GAME", stileBottoni))
            {

                Application.LoadLevel("SampleScene");
                // m_audio.Stop();
            }

            if (GUILayout.Button("GUIDE", stileBottoni)) //scompaiono tutti i bottoni e ne compagliono altri
            {
                menuPrincipaleOn = false;
                menuOpzioniOn = true;
            }
            if (GUILayout.Button("QUIT", stileBottoni))
            {
                Application.Quit();
                Quit();
            }
            GUILayout.EndArea();

        }
        if (menuOpzioniOn)
        {
            Guida.SetActive(true);
            GUILayout.BeginArea(new Rect((Screen.width / 2) - 550, (Screen.height / 2) + 200, 500, 500));
            if (GUILayout.Button("INDIETRO", stileBottoni))
            {
                menuPrincipaleOn = true;
                menuOpzioniOn = false;

                Guida.SetActive(false);
            }

            GUILayout.EndArea();
        }

    }
    public void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
    }

}
