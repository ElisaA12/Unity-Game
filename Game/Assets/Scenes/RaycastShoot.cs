using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RaycastShoot : MonoBehaviour
{

    public int gunDamage = 1;                                            // Set the number of hitpoints that this gun will take away from shot objects with a health script
    public float fireRate = 0.25f;                                        // Number in seconds which controls how often the player can fire
    public float weaponRange = 50f;                                        // Distance in Unity units over which the player can fire
    public float hitForce = 200f;                                        // Amount of force which will be added to objects with a rigidbody shot by the player
    public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
    public int sizeCaricatore;
    public TextMeshProUGUI bullets;   //voglio a video quanti proiettili restano


    private bool reloading;
    private float tempoRicarica = 2;
    public Camera fpsCam;                                                // Holds a reference to the first person camera
    private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);    // WaitForSeconds object used by our ShotEffect coroutine, determines time laser line will remain visible
    public AudioSource gunAudio;                                        // Reference to the audio source which will play our shooting sound effect
    private LineRenderer laserLine;                                        // Reference to the LineRenderer component which will display our laserline

   
    private float nextFire;                                                // Float to store the time the player will be allowed to fire again, after firing
    private int bulletsLeft;//countdown

    void Start () 
    {
        // Get and store a reference to our LineRenderer component
        laserLine = GetComponent<LineRenderer>();

        reloading = false;
        bulletsLeft = sizeCaricatore;
    }


    void Update () 
    {

        bullets.text = bulletsLeft + "/" + sizeCaricatore;
        //ricarica
        //controllo se la carica non e' piena e che non stia relodando
        if (Input.GetKeyDown(KeyCode.J) && bulletsLeft < sizeCaricatore && !reloading )
        {
            Reload();
        }


        // Check if the player has pressed the fire button and if enough time has elapsed since they last fired
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire && !reloading && bulletsLeft <= sizeCaricatore && bulletsLeft != 0) 
        {
            nextFire = Time.time + fireRate;

            StartCoroutine (ShotEffect());

            Vector3 rayOrigin = fpsCam.ViewportToWorldPoint (new Vector3(0.5f, 0.5f, 0.0f));

            RaycastHit hit;

            laserLine.SetPosition (0, gunEnd.position);

            if (Physics.Raycast (rayOrigin, fpsCam.transform.forward, out hit, weaponRange))
            {
               
                laserLine.SetPosition (1, hit.point);

                EnergiaEnemy health = hit.collider.GetComponent<EnergiaEnemy>();
                if(hit.collider.name == "Sphere" || hit.collider.tag == "Sfera"){
                    Destroy(hit.transform.gameObject);
                    Debug.Log("sfera");
                }
               
                        if (health != null)
                {
                    health.getDamage (gunDamage);
                }

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce (-hit.normal * hitForce);
                }
            }
            else
            {
                // If we did not hit anything, set the end of the line to a position directly in front of the camera at the distance of weaponRange
                laserLine.SetPosition (1, rayOrigin + (fpsCam.transform.forward * weaponRange));
            }
            bulletsLeft--;
        }
    }


    private IEnumerator ShotEffect()
    {
        // Play the shooting sound effect
        gunAudio.Play ();

        // Turn on our line renderer
        laserLine.enabled = true;

        //Wait for .07 seconds
        yield return shotDuration;

        // Deactivate our line renderer after waiting
        laserLine.enabled = false;
    }
    void Reload()
    {
        reloading = true;
        Invoke("Reloading", tempoRicarica);
        reloading = false;
    }

    void Reloading()
    {
        bulletsLeft = sizeCaricatore;
    }
}
