using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class OpenChest : MonoBehaviour
{
    public GameObject ChestClose;
    public GameObject ChestOpen;
    public AudioSource m_audio;
    public GameObject Win;
    // Start is called before the first frame update
    void Start()
    {
        ChestClose.SetActive(true);
        ChestOpen.SetActive(false);
        Win.SetActive(false);
    }
        
        //aprire il tesoro
        private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            m_audio.Play();
            StartCoroutine(Disattiva(ChestClose));
            StartCoroutine(Attiva(ChestOpen));
            Win.SetActive(true);

        }
    }
    IEnumerator Attiva(GameObject gameObject)
    {

        yield return new WaitForSeconds(1); //Attendi due secondi
        gameObject.SetActive(true); //Disabilita il gameObject

    }
    IEnumerator Disattiva(GameObject gameObject)
    {

        yield return new WaitForSeconds(1); //Attendi due secondi
        gameObject.SetActive(false); //Disabilita il gameObject

    }
}
