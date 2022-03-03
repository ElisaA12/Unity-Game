using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energia : MonoBehaviour
{
    public int vita;
    public int vitaMax;

    public GUISkin skinEnergia;
    public GameObject GameOver;

    // Start is called before the first frame update
    void Start()
    {
        vita = vitaMax;
        GameOver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (vita > vitaMax)
        {
            vita = vitaMax;
        }
        else if (vita <= 0)
        {
            vita = 0;
            GameOver.SetActive(true);
            StartCoroutine(Over());
        }

    }
    void OnGUI()
    {
        //rect(10 pixel di distanza da sx, distanza dall'alto,larghezza, altezza)
        GUI.Box(new Rect(10, 10, Screen.width / 4, 20), "VITA" + vita + "/" + vitaMax);

        if (vita > 0)
        {
            GUI.skin = skinEnergia;
            GUI.Box(new Rect(10, 10, Screen.width / 4 * ((float)vita / (float)vitaMax), 20), "");
        }
    }
    public void getDamage(int danno)
    {
        vita -= danno;
    }

    public int getVita()
    {
        return vita;
    }
    IEnumerator Over()
    {
        yield return new WaitForSeconds(2); //Attendi due secondi
        Application.LoadLevel("Menu");

    }
}
