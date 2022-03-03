using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Gun : MonoBehaviour
{
    [SerializeField] string _nome="fucile1"; //nome identificativo
    public string GetNomeFucile(){ return this._nome;}
    //strig nome= player.GetComponentInChild<Gun>().GetNomeFucile();
    //da un altro script: GetCom... ovvero che questo script dara' in un figlio del player, Gun e' lo script da cui vogliamo la GetNomeFucile


    
    bool IsShooting, readyToShoot, reloading;//sto sparando, pronto a sparare, caricamento dei proiettili
    private int bulletsLeft;//countdown
    [SerializeField] int sizeCaricatore;
    

    [SerializeField] float tempoRicarica=1;
    [SerializeField] float TimeBetweenShot; //numero deve essere piccolo
    [SerializeField] float maxDistance=30;
    [SerializeField] float spread=5;
    [SerializeField] float danno=100;
    [SerializeField] float speed=100;
   


    //camera, cioe' una reference, per il punto da cui parte lo sparo ad esempio
    [SerializeField] Camera _playerCam;
    [SerializeField] LayerMask definedLayer;
    [SerializeField] TextMeshProUGUI bullets;   //voglio a video quanti proiettili restano
    [SerializeField] ParticleSystem FireEffect;
    [SerializeField] AudioSource audioSource;
    public GameObject proiettile;
    public GameObject cannaPistola;
    
    //public Camera primaPersona;
    //public CameraShake cameraShake;

    RaycastHit _rayhit;
    private void Start(){
        //inizializiamo le variabili
        IsShooting=false;
        readyToShoot=true;
        reloading=false;
        bulletsLeft=sizeCaricatore;
    }


   //vogliamo sapere ogni frame cosa vuole fare il giocatore
   void Update(){
       //GetKey true finche' non si riascia il pulsante
       //GetKeyDown true solo quando si preme una volta
       if( Input.GetKey(KeyCode.Mouse0)){//qundo premo i tasto sinistro del mouse
            IsShooting=true;
            GameObject clone= Instantiate(proiettile,_playerCam.transform.position,_playerCam.transform.rotation);
            
       }
   }
}/*    
        //ricarica
        //controllo se la carica non e' piena e che non stia relodando
        if ( Input.GetKeyDown(KeyCode.J) && bulletsLeft<sizeCaricatore && !reloading ){ 
            Reload();
            }


        //spara
        if( IsShooting && bulletsLeft>0 && !reloading && readyToShoot ){ 
            Shoot();
            //StartCoroutine(cameraShake.Shake(.15f,.4f));
            //CameraShake.Instance.ShakeOnce(5f, 4f, 1f,0, 5f);//forza, piu' il valore e' basso piu' il movimento 'e fluido, tempo di entrata, tempo di usita per non rendere il moevimento troppo improvviso  
        }
        
        IsShooting=false;
        bullets.text= bulletsLeft + "/" + sizeCaricatore;
       
    }

    void Reload(){
        reloading=true;
        Invoke("Reloading", tempoRicarica);
        reloading=false;
    }

    void Reloading(){
        bulletsLeft=sizeCaricatore;
    }

    void Shoot(){
        readyToShoot= false;

        float x= Random.Range(-spread,spread);
        float y=Random.Range(-spread,spread);
        //Vector3 direction= _playerCam.transform.forward + new Vector3(x,y,0);
        Ray ray = new Ray(transform.position, transform.forward);
        //RayCast, e' un raggio invisibile che parte da un punto che gli diciamo noi, con una certa direzione (.forward cioe' davanti a noi)
        //questo raggio colpisce un oggetto, le info del punto dell oggetto che andiamo a colpire saranno salvati in RaycastHit (out )
        //possiamo definire una massima distanza del raggio e un Layer su cui puoo' agire
        if(Physics.Raycast(ray, out _rayhit, maxDistance, definedLayer) )
        {
            Debug.Log(_rayhit.collider.name);
            
            //controllare che colpiamo effettivamente il nemico
            if(_rayhit.collider.tag=="Enemy"){
                _rayhit.collider.GetComponent<Enemy>().getDamage(danno);
            }
        }

        //quando sparo voglio che parta l animazione
        FireEffect.Play();
        audioSource.Play();
        

        bulletsLeft--;
        Invoke("ReadyToShoot", TimeBetweenShot);
        
    }

    void ReadyToShoot(){
        readyToShoot=true;
    }
}



/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Events;

public class Gun : MonoBehaviour
{
   
    [SerializeField] string _nome="fucile1"; //nome identificativo
    public string GetNomeFucile(){ return this._nome;}
    //strig nome= player.GetComponentInChild<Gun>().GetNomeFucile();
    //da un altro script: GetCom... ovvero che questo script dara' in un figlio del player, Gun e' lo script da cui vogliamo la GetNomeFucile


    
    bool IsShooting, readyToShoot, reloading;//sto sparando, pronto a sparare, caricamento dei proiettili
    bool pistola=false;
    private int bulletsLeft;//countdown
    private int putaway=1;//countdown
    [SerializeField] int sizeCaricatore;
    

    [SerializeField] float tempoRicarica=1;
    [SerializeField] float TimeBetweenShot; //numero deve essere piccolo
    [SerializeField] float maxDistance=50;
    [SerializeField] float spread=5;
    [SerializeField] float danno=100;
   


    //camera, cioe' una reference, per il punto da cui parte lo sparo ad esempio
    [SerializeField] Camera _playerCam;
    [SerializeField] LayerMask definedLayer;
    [SerializeField] TextMeshProUGUI bullets;   //voglio a video quanti proiettili restano
    [SerializeField] ParticleSystem FireEffect;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject proiettile;
    [SerializeField] SimpleCameraShake cmFreeCam;
    [SerializeField] shakeVirtual vaim;


    public float ShakeDuration = 0.3f;          // Time the Camera Shake effect will last
    public float ShakeAmplitude = 1.2f;         // Cinemachine Noise Profile Parameter
    public float ShakeFrequency = 2.0f;         // Cinemachine Noise Profile Parameter


    RaycastHit _rayhit;
    private void Start(){
        //inizializiamo le variabili
        IsShooting=false;
        readyToShoot=true;
        reloading=false;
        bulletsLeft=sizeCaricatore;


    }

  
    void Spara () {
        //Istanzia l'oggetto proiettile, creando una copia del PrefabOriginale impostando la posizione e la rotazione
        GameObject proiettilePist = GameObject.Instantiate(proiettile,transform.position,Quaternion.identity);
        
        
        Destroy(proiettilePist,3f);

    }

    bool PutAway(){
        bool verita;
        if((putaway%2)==0){
            verita=false;
        }
        else{
            verita=true;
        }
        putaway++;
        return verita;
    }

   //vogliamo sapere ogni frame cosa vuole fare il giocatore
   void Update(){
       // per pistola con colpi a ripetizione
       if(Input.GetKeyDown(KeyCode.R) && (pistola=PutAway()) ){
            
       }
       
       if((Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse0)) && pistola){//qundo premo i tasto sinistro del mouse
            IsShooting=true;
            Spara();
       }
       //GetKey true finche' non si riascia il pulsante
       //GetKeyDown true solo quando si preme una volta
        
        
        //ricarica
        //controllo se la carica non e' piena e che non stia relodando
        if ( Input.GetKey(KeyCode.Q) && bulletsLeft<sizeCaricatore && !reloading ){ Reload();}


        //spara
        if( IsShooting && bulletsLeft>0 && !reloading && readyToShoot ){ 
            Shoot();
            StartCoroutine(cmFreeCam.Shake());
            vaim.ShakeCamera(5f,.1f);
            //CameraShake.Instance.ShakeOnce(5f, 4f, 1f,0, 5f);//forza, piu' il valore e' basso piu' il movimento 'e fluido, tempo di entrata, tempo di usita per non rendere il moevimento troppo improvviso  
        }
        
        IsShooting=false;
        bullets.text= bulletsLeft + "/" + sizeCaricatore;
    }

    void Reload(){
        reloading=true;

        Invoke("Reloading", tempoRicarica);
    
        reloading=false;
    }

    void Reloading(){
        bulletsLeft=sizeCaricatore;
    }

    void Shoot(){
        readyToShoot= false;

        GameObject proiettilePist = GameObject.Instantiate(proiettile,transform.position,Quaternion.identity);
	
        float x= Random.Range(-spread,spread);
        float y=Random.Range(-spread,spread);
        Vector3 direction= _playerCam.transform.forward + new Vector3(x,y,0);

        //RayCast, e' un raggio invisibile che parte da un punto che gli diciamo noi, con una certa direzione (.forward cioe' davanti a noi)
        //questo raggio colpisce un oggetto, le info del punto dell oggetto che andiamo a colpire saranno salvati in RaycastHit (out )
        //possiamo definire una massima distanza del raggio e un Layer su cui puoo' agire
        if(Physics.Raycast(_playerCam.transform.position,direction, out _rayhit, maxDistance, definedLayer) )
        {
            Debug.Log(_rayhit.collider.name);
            //controllare che colpiamo effettivamente il nemico
            if(_rayhit.collider.tag=="Enemy"){
                _rayhit.collider.GetComponent<Enemy>().getDamage(danno);
            }
        }

        //quando sparo voglio che parta l animazione
        FireEffect.Play();
        audioSource.Play();
        

        bulletsLeft--;
        Invoke("ReadyToShoot", TimeBetweenShot);
        
    }

    void ReadyToShoot(){
        readyToShoot=true;
    }
}
*/