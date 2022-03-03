using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ThirdPersonController : MonoBehaviour
{
    CharacterController controller;
    Animator MyAnimator;
    
    
    //public Visuale movimentoPrima;
    //public PlayerMovementInputController movimentoTerza;
    
    
    [SerializeField] Camera MyCamera;
    [SerializeField] float Speed= 4f;
    [SerializeField] float SprintSpeed=5f;
    [SerializeField] float AnimationBlendSpeed=5f;
    [SerializeField] float RotationSpeed=15f;
    [SerializeField] float JumpSpeed=0f;   
    [SerializeField] AudioSource audioSource;
    public CinemachineFreeLook vmain;

    Vector3 posizioneController;

    /*stepTimer: è un timer che se ha valore 0 indica che può essere riprodotto il suono
    di un passo, altrimenti significa che è appena stato riprodotto un passo.
    stepCool: indica quanti secondi devono passare prima che sia possibile riprodurre un altro passo.*/
    float stepTimer = 0.0f;
    float stepCool = 0.9f;
    float mSpeedY=0f;
    float gravita= -11.81f;
    float mDesiredRotation=0f;
    float mDesiredAnimationSpeed=0f;
    bool mSprinting=false;
    bool mJumping=false;

    IEnumerator Sleep(){
        yield return new WaitForSeconds(5);
    }

    

    // Start is called before the first frame update
    void Start()
    {
        
        controller = GetComponent<CharacterController>();
        MyAnimator= GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        float x= Input.GetAxisRaw("Horizontal");
        float z= Input.GetAxisRaw("Vertical");
        
        mSprinting= Input.GetKey(KeyCode.LeftShift);

        if ((Input.GetButton("Horizontal") | Input.GetButton("Vertical")) && !audioSource.isPlaying)
{
            audioStep();
        }
        if ((Input.GetButton("Horizontal") || Input.GetButton("Vertical")) && Input.GetKey(KeyCode.LeftShift) && !audioSource.isPlaying)
        {
            stepCool = 0.1f;
            audioStep();
        }
        else
        {
            stepCool = 0.9f;
        }
        /*gestione del timer. La funzione Time.deltaTime
restituisce semplicemente il valore in secondi che si impiega a completare un frame che
va sottratto a stepTimer. Sottraendo ad ogni frame Time.deltatime arriveremo ad un
momento in cui Steptimer arriverà a 0 e quindi sarà possibile riprodurre il suono.*/
        if (stepTimer > 0) stepTimer -= Time.deltaTime;
        if (stepTimer < 0) stepTimer = 0;

        //JUMP
        if (!mJumping && Input.GetButtonDown("Jump"))
            {
                mJumping=true;
                MyAnimator.SetTrigger("Jump");

                mSpeedY+=JumpSpeed;
            
            }
            if(!controller.isGrounded) 
            {
                mSpeedY += gravita * Time.deltaTime;
            }
            else if(mSpeedY<0)//se il personaggio sta cadendo
            {
                mSpeedY=0;
            }
            
            MyAnimator.SetFloat("SpeedJump", mSpeedY / JumpSpeed);

            if(mJumping && mSpeedY<0)
            {
                    RaycastHit hit;
                    if(Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Default")))
                    {
                        mJumping=false;
                        MyAnimator.SetTrigger("Land");
                    }
            }


    //movimento   
        Vector3 movimento = new Vector3(x,0,z).normalized;
        //per muovere il personaggio nella direzione dove punto il mouse
        Vector3 rotatedMovement = Quaternion.Euler(0, MyCamera.transform.rotation.eulerAngles.y,0) * movimento;
        Vector3 verticalMovement= Vector3.up *mSpeedY;
        controller.Move((verticalMovement +(rotatedMovement * (mSprinting ? SprintSpeed : Speed))) * Time.deltaTime);
        
        
        if(rotatedMovement.magnitude >0)
        {
            mDesiredRotation = Mathf.Atan2(rotatedMovement.x,rotatedMovement.z)* Mathf.Rad2Deg;
            mDesiredAnimationSpeed= mSprinting ? 1 : .5f;
        }
        else
        {
            mDesiredAnimationSpeed=0;
        }


        MyAnimator.SetFloat("Speed",Mathf.Lerp(MyAnimator.GetFloat("Speed"),mDesiredAnimationSpeed,AnimationBlendSpeed*Time.deltaTime));

    //rotazione 
        Quaternion currentRotation=transform.rotation;
        Quaternion targetRotation= Quaternion.Euler(0,mDesiredRotation,0);
        
        transform.rotation= Quaternion.Lerp(currentRotation,targetRotation,RotationSpeed *Time.deltaTime);


    }/*La funzione audiostep() riproduce il suono del passo e resetta lo stepTimer nel caso in cui
quest’ultimo abbia valore 0*/
    void audioStep()
    {
        if (stepTimer == 0)
        {
            audioSource.Play();
            stepTimer = stepCool;
        }
    }

    
}
