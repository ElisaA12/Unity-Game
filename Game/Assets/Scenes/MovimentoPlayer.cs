using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class MovimentoPlayer : MonoBehaviour
{

    [SerializeField] AudioSource audioSource;
    CharacterController controller;
    Vector3 velocitay;
    public Transform TerraCheck;
    public LayerMask TerraMask; //collisioni

    bool toccaterra;

    float gravita= -9.8f;
    float altezzasalto=5f;
    float distanzaTerra = 0.1f;
    float velocita= 5f;
    /*stepTimer: è un timer che se ha valore 0 indica che può essere riprodotto il suono
    di un passo, altrimenti significa che è appena stato riprodotto un passo.
    stepCool: indica quanti secondi devono passare prima che sia possibile riprodurre un altro passo.*/
    float stepTimer = 0.0f;
    float stepCool = 0.9f;

    public CinemachineFreeLook vmain;
    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float x= Input.GetAxis("Horizontal");
        float z= Input.GetAxis("Vertical");
        
        if ((Input.GetButton("Horizontal") | Input.GetButton("Vertical")) && !audioSource.isPlaying)
{
            audioStep();
        }
        if ((Input.GetButton("Horizontal") | Input.GetButton("Vertical"))&&Input.GetKey(KeyCode.LeftShift ) && !audioSource.isPlaying)
{
            stepCool = 0.1f;
            audioStep();
        }
        else
        {
            stepCool = 0.9f;
        }
        if (stepTimer > 0)  stepTimer -= Time.deltaTime;
        if (stepTimer < 0)  stepTimer = 0;

        


        Vector3 movimento = transform.right * x + transform.forward * z; //direzione che voggliamo dare al nostro player
        if(Input.GetKey(KeyCode.LeftShift)){
            velocita=15f;
        }
        controller.Move(movimento*Time.deltaTime * velocita);
        


        //creiamo una sfera intorno a TerraChek per vedere se tocca terra
        toccaterra= Physics.CheckSphere(TerraCheck.position, distanzaTerra, TerraMask);//posizione, raggio, maskera

        //controlliamo se l oggetto tocca o no
        if (velocitay.y<0 && toccaterra){
            velocitay.y=-10f;
        }
        //per il salto
        
        if(Input.GetButtonDown("Jump") && toccaterra ){
            velocitay.y= Mathf.Sqrt(gravita * altezzasalto * -2f);//altezza salto
         
        }

        velocitay.y += gravita * Time.deltaTime;
        controller.Move(velocitay *Time.deltaTime);

        
        
    }
    void audioStep()
        {
            if (stepTimer == 0)
            {
            audioSource.Play();
            stepTimer = stepCool;
            }
        }
}
