using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NuovoGiocatore : MonoBehaviour
{
    private BaseGiocatore nuovoGiocatore;
    private string NomeGiocatore= "inserisci nome";
    // Start is called before the first frame update
    void Start()
    {
        nuovoGiocatore = new BaseGiocatore();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnGUI()
    {
        GUILayout.BeginArea(new Rect((Screen.width / 2) - 250, (Screen.height / 2) - 250, 500, 500));

        NomeGiocatore = GUILayout.TextArea(NomeGiocatore);
        if (GUILayout.Button("Crea"))
        {
            nuovoGiocatore.NomeGiocatore = NomeGiocatore;
        }
        if (GUILayout.Button("Indietro"))
        {
        }

        GUILayout.EndArea();
    }
}
