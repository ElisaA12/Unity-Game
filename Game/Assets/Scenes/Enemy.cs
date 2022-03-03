using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
    [SerializeField]
    NavMeshAgent agent;
    Transform targetToFollow;
    NavMeshPath path;
    EnergiaEnemy energia;
    bool visible;
    public Transform player;
    public float velocitaNemico;
    public float distanzaNemico;
    public float damageRate = 2f;
    private float nextDamage = 0.0f;
    public AudioSource m_audio;
    bool audio;
    


    void OnValidate()//per caricare questo agent se non c'e'
    {
        if (!agent)
        {
            agent = GetComponent<NavMeshAgent>();
        }
    }

    void Start()
    {
        energia = GetComponent<EnergiaEnemy>();
        energia.enabled = false;
        visible = false;
        audio = false;
    }

    void Update()
    {
        //ad ogni frame dovremo calcolare la distanza tra il nemico e il giocatore
        //(posizione dell'oggetto al quale attaccheremo lo script,posizione del player)

        float distanza = Vector3.Distance(transform.position, player.transform.position);

        if (distanza <= distanzaNemico)
        {
            if (!audio)
            {

                m_audio.PlayOneShot(m_audio.clip, 0.5f);
                audio = true;
            }
            transform.LookAt(player); //ovvero il nemico dovra' guardre il player
            transform.position = Vector3.Lerp(transform.position, player.transform.position, velocitaNemico / 1000);// (da, a, quanto tempo ci impiega/un coefficiente)
            energia.enabled = true;
            visible = true;
            //Attiva();

            //StartCoroutine(Attiva());
            
        }
        else
        {
            visible = false;
        }
        if (distanza <= 1f && Time.time >= nextDamage)
        {
            m_audio.Play();
            nextDamage = Time.time + damageRate;
            Energia health = player.GetComponent<Energia>();

            // Call the damage function of that script, passing in our gunDamage variable
            health.getDamage(3);


        }

    }




    public bool getVisible()
    {

        return visible;

    }
    IEnumerator Attiva()
    {

        yield return new WaitForSeconds(2); //Attendi 
        audio = true;

    }

}
