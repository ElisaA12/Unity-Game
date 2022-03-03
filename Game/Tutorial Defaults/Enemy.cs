using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Enemy : MonoBehaviour
{
   /*
    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, whatIsPlayer;

    
    //salute nemico
    [SerializeField] private float healt= 100;

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject proiettile;

    //States
    public float sightRange,attackRange;
    public bool playerInSightRange, playerInAttackRange;

    void Awake()
    {
        player=GameObject.Find("Ellen").transform;
        agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {   
        playerInSightRange= Physics.CheckSphere(transform.position, sightRange,whatIsPlayer);
        playerInAttackRange= Physics.CheckSphere(transform.position, attackRange,whatIsPlayer);
        
        if(!playerInSightRange && !playerInAttackRange){ Patroling();}
        if(playerInSightRange && !playerInAttackRange){ ChasePlayer();}
        
        
        
    }

    void Patroling()
    {
        if(!walkPointSet){ SearchWalkPoint();}

        if(walkPointSet){ agent.SetDestination(walkPoint);}

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //wlakpoint reached
        if(distanceToWalkPoint.magnitude <1f){
            walkPointSet=false;
        }
    }

    void SearchWalkPoint()
    {
        //calcola random point in range
        float randomZ= Random.Range(-walkPointRange, walkPointRange);
        float randomX= Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x+ randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet=true;
        }
    }

    void ChasePlayer()
    {
        agent.SetDestination(player.position);

    }

    void AttackPlayer()
    {
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked)
        {
            //attack code
            Rigidbody rb= Instantiate(proiettile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);

            alreadyAttacked=true;
            Invoke(nameof(ResetAttack),timeBetweenAttacks);
        }
    }

    void ResetAttack(){
        alreadyAttacked=false;
    } 
/*
    
    public void getDamage(float damage){
        healt-=damage; 
        //se e' morto il nemico
        if(healt<=0){
            Destroy(gameObject,0.5f);
        }

  
    }
    */
    public Transform player;
    public float velocitaNemico;
    public float distanzaNemico;


    void Start(){

    }

    void Update(){
        //ad ogni frame dovremo calcolare la distanza tra il nemico e il giocatore
        //(posizione dell'oggetto al quale attaccheremo lo script,posizione del player)
        float distanza= Vector3.Distance(transform.position, player.transform.position);

        if(distanza <= distanzaNemico){
            transform.LookAt(player); //ovvero il nemico dovra' guardre il player
            transform.position= Vector3.Lerp(transform.position, player.transform.position, velocitaNemico/1000);// (da, a, quanto tempo ci impiega/un coefficiente)
        
        }
    
    
    }

    public void getDamage(float damage){

    }

   

  
}
