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
    [SerializeField] GameObject pistola;
    public CinemachineVirtualCamera vaim;
    public CinemachineFreeLook vmain;

    Vector3 posizioneController;

    
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
       

        
        //JUMP
            if(!mJumping && Input.GetButtonDown("Jump"))
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
                    if(Physics.Raycast(transform.position, Vector3.down, out hit, .5f, LayerMask.GetMask("Terra")))
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
    
    
    }
}
