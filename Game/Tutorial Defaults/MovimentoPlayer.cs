using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovimentoPlayer : MonoBehaviour
{
    CharacterController controller;
    Animator MyAnimator;
    Vector3 velocitay;
    public Transform TerraCheck;
    public LayerMask TerraMask; //collisioni

    bool toccaterra;

    float gravita= -20.8f;
    float altezzasalto=5f;
    float distanzaTerra = 0.3f;
    float velocita= 10f;
    
    public CinemachineVirtualCamera vaim;
    public CinemachineFreeLook vmain;
    // Start is called before the first frame update
    void Start()
    {
        MyAnimator= GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x= Input.GetAxis("Horizontal");
        float z= Input.GetAxis("Vertical");
        Vector3 movimento = transform.right * x + transform.forward * z; //direzione che voggliamo dare al nostro player
        if(Input.GetKey(KeyCode.LeftShift)){
            velocita=20f;
        }
        controller.Move(movimento*Time.deltaTime * velocita);
        


        //creiamo una sfera intorno a TerraChek per vedere se tocca terra
        toccaterra= Physics.CheckSphere(TerraCheck.position, distanzaTerra, TerraMask);//posizione, raggio, maskera
     
        //controlliamo se l oggetto tocca o no
        if(velocitay.y<0 && toccaterra){
            velocitay.y=-10f;
        }
        //per il salto
        
        if(Input.GetButtonDown("Jump") && toccaterra ){
            velocitay.y= Mathf.Sqrt(gravita * altezzasalto * -2f);//altezza salto
         
        }

        velocitay.y += gravita * Time.deltaTime;
        controller.Move(velocitay *Time.deltaTime);

        
        
    }

}
