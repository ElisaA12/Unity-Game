using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Visuale : MonoBehaviour
{
    public Transform player;
    Vector3 velocitay;
    public Transform TerraCheck;
    public LayerMask TerraMask; //collisioni

    bool toccaterra;
    CharacterController controller;

    float sensibilita=100f;

    float rotazione;
    // Start is called before the first frame update
    void Start()
    {
        //leviamo il mouse dalla visuale, lo blocchiamo al centro
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        controller = GetComponent<CharacterController>();
        //oppure per cambiare  l icona del mouse
        //Cursor.SetCursor(Resources.Load<Texture2D>("Imagines/cursore"),Vector2.zero,CursorMode.Auto);

    }

    // Update is called once per frame
    void Update()
    {
        
        float x= Input.GetAxis("Mouse X")* Time.deltaTime *sensibilita;
        float y= Input.GetAxis("Mouse Y")* Time.deltaTime*sensibilita;
        


        rotazione -= y; //ogni volta dobbiamo sottrarre y
        rotazione = Mathf.Clamp(rotazione, -60f,60f); //imponiamo di limiti alla rotazione

        transform.localRotation =Quaternion.Euler(rotazione, 0 , 0);

        player.Rotate(Vector3.up*x);

        Vector3 posizione= transform.position;
        
       /* if( Input.GetKeyDown(KeyCode.J)){
            if(!terzaPersona){
                transform.Translate(new Vector3(0f,2f,-4f));
                terzaPersona=true;
            }else{
                transform.Translate(new Vector3(0f,-2f,4f));
                terzaPersona=false;
            }
       
        }*/
}
}

