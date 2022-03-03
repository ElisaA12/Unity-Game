using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spistola : MonoBehaviour
{
    [SerializeField] GameObject pistola;
    [SerializeField] CharacterController controller;

    Vector3 posizioneController;
    bool primoR=true;

    //void Spara () {
        //Istanzia l'oggetto proiettile, creando una copia del PrefabOriginale impostando la posizione e la rotazione
      //  GameObject proiettile = GameObject.Instantiate(PrefabOriginale,transform.position,Quaternion.identity);
	//}

    // Start is called before the first frame update
    void Start()
    {
        controller=GetComponent<CharacterController>();
        Instantiate(pistola);
    }

    // Update is called once per frame
    void Update()
    {
        posizioneController= controller.transform.position;

        if(Input.GetKey(KeyCode.R) && primoR)
        {
            Instantiate(pistola, posizioneController*Time.deltaTime,Quaternion.identity );
        }else if(Input.GetKey(KeyCode.R) && !primoR)
        {
            Destroy(pistola,1f);
        }
    }
}
