using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Cinemachine;

public class Pistola : MonoBehaviour
{
    
    bool mPistola=false;
    bool primoR=true;
    
       
    public GameObject pistola;
    public Animator MyAnimator;
    public Camera primaPersona;
    public Camera terzaPersona;
    public GameObject player;
    public GameObject Gun;
    public GameObject Caricatore;
    public GameObject RawImage;
    public GameObject visualePrima;
    public CinemachineFreeLook vmain;
    public ThirdPersonController movimentoTerza;
    public MovimentoPlayer movimentoPrima;
    GameObject instancePistola;
    
    
    // Start is called before the first frame update
    void Start()
    {
        Gun.GetComponent<Gun>().enabled = false;
        Gun.GetComponent<Shooter>().enabled = false;
        player.GetComponent<ThirdPersonController>().enabled = true;
        player.GetComponent<MovimentoPlayer>().enabled = false;
        MyAnimator= GetComponent<Animator>();
        terzaPersona.enabled=true;
        Caricatore.SetActive(false);
        RawImage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //pistola
        mPistola=Input.GetKeyDown(KeyCode.R);
        
        if(mPistola && primoR)
        {
            vmain.enabled=false;
            terzaPersona.enabled=false; 
            primaPersona.enabled=true;
            Caricatore.SetActive(true);
            RawImage.SetActive(true);
            terzaPersona.GetComponent<CinemachineBrain>().enabled=false;
            player.GetComponent<ThirdPersonController>().enabled = false;
            player.GetComponent<MovimentoPlayer>().enabled = true; 
            //Gun.GetComponent<Shooter>().enabled = true;
 
            Gun.GetComponent<Gun>().enabled = true;
             
            //movimentoTerza.enabled=false;
            //movimentoPrima.enabled=true;

            Vector3 posizione= visualePrima.transform.position;
            primaPersona.transform.position=posizione;
            
            Vector3 tempPos=transform.position;
            tempPos.y+=1.25f;
            instancePistola=Instantiate(pistola,tempPos,transform.rotation);            
            instancePistola.transform.parent = GameObject.Find("Gun").transform;
                       
            instancePistola.transform.Translate(new Vector3(-0.05f,0f,.5f));
            instancePistola.transform.Rotate(0f, -105f, 0.0f, Space.World);
            primoR=false;
            mPistola=false;
            MyAnimator.SetTrigger("Mira");
            
            //movimentoTerza.enabled=false;
            //movimentoPrima.enabled=true;
            //vaim.enabled=true;
            //primaPersona.enabled=true;

        }
        else if(mPistola && !primoR && instancePistola!=null)
        {
            vmain.enabled=true;
            terzaPersona.enabled=true; 
            primaPersona.enabled=false;
            Caricatore.SetActive(false);
            RawImage.SetActive(false);        
            //Gun.GetComponent<Shooter>().enabled = true;

            Gun.GetComponent<Gun>().enabled = false;
            terzaPersona.GetComponent<CinemachineBrain>().enabled=true;
            primoR=true;
            DestroyImmediate(instancePistola,true);
            MyAnimator.SetTrigger("PutAway");
            
                
            player.GetComponent<ThirdPersonController>().enabled = true;
            player.GetComponent<MovimentoPlayer>().enabled = false; 
            //vaim.enabled=false;
            
            //primaPersona.enabled=false;
            terzaPersona.enabled=true;
        }
    }
}
