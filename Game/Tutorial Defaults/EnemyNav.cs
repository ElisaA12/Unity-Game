using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNav : MonoBehaviour
{
    
    public Transform player;
    public float distanzaNemico;
    public UnityEngine.AI.NavMeshAgent agent;
    public GameObject EnergiaEnemy;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        EnergiaEnemy.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //ad ogni frame dovremo calcolare la distanza tra il nemico e il giocatore
        //(posizione dell'oggetto al quale attaccheremo lo script,posizione del player)
        Vector3 posizioneIniziale=transform.position;
        float distanza= Vector3.Distance(transform.position,player.transform.position);
        if(distanza<=distanzaNemico){
            //non servira' dire che il nemico deve guardare il giocatore ec... ma pensera' a tutto unity
            agent.SetDestination(player.position);
            EnergiaEnemy.SetActive(true); //barra energia nemico
        }else{
            agent.SetDestination(posizioneIniziale);
            EnergiaEnemy.SetActive(false);
        }
    }
}
