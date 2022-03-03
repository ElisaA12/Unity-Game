using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class Pistola : MonoBehaviour
{
    
    bool mPistola=false;
    bool primoR=true;

    public GameObject VisualeFirst;   
    public GameObject pistola;
    public Animator MyAnimator;
    public Camera primaPersona;
    public Camera terzaPersona;
    public GameObject player;
    public GameObject Gun;
    public GameObject PistolaIst;
    public GameObject Caricatore;
    public GameObject RawImage;
    public GameObject visualePrima;
    public CinemachineFreeLook vmain;
    GameObject instancePistola;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Gun.GetComponent<RaycastShoot>().enabled = false;
        player.GetComponent<ThirdPersonController>().enabled = true;
        player.GetComponent<MovimentoPlayer>().enabled = false;
        MyAnimator= GetComponent<Animator>();
        terzaPersona.enabled=true;
        primaPersona.enabled=false;
        Caricatore.SetActive(false);
        RawImage.SetActive(false);

        PistolaIst.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pistola
        mPistola=Input.GetKeyDown(KeyCode.E);

        
        if(mPistola && primoR)
        {
            VisualeFirst.transform.position = VisualeFirst.transform.position + new Vector3(0, 2.80f, 0);
            vmain.enabled=false;
            terzaPersona.enabled=false; 
            primaPersona.enabled=true;
            Caricatore.SetActive(true);
            RawImage.SetActive(true);
            PistolaIst.SetActive(true);


            terzaPersona.GetComponent<CinemachineBrain>().enabled=false;
            player.GetComponent<ThirdPersonController>().enabled = false;
            player.GetComponent<MovimentoPlayer>().enabled = true; 
 
           Gun.GetComponent<RaycastShoot>().enabled = true;
             
           
            Vector3 posizione= visualePrima.transform.position;
            primaPersona.transform.position=posizione;
             primoR=false;
            mPistola=false;
            //MyAnimator.SetTrigger("Mira");
            

        }
        else if(mPistola && !primoR )
        {
            VisualeFirst.transform.position = VisualeFirst.transform.position + new Vector3(0, -2.80f, 0);

            vmain.enabled=true;
            terzaPersona.enabled=true; 
            primaPersona.enabled=false;
            Caricatore.SetActive(false);
            RawImage.SetActive(false);
            PistolaIst.SetActive(false);

            Gun.GetComponent<RaycastShoot>().enabled = false;
            terzaPersona.GetComponent<CinemachineBrain>().enabled=true;
            primoR=true;
            //MyAnimator.SetTrigger("PutAway");
            
                
            player.GetComponent<ThirdPersonController>().enabled = true;
            player.GetComponent<MovimentoPlayer>().enabled = false; 
            //vaim.enabled=false;
            
            //primaPersona.enabled=false;
            terzaPersona.enabled=true;
        }
    }
}
