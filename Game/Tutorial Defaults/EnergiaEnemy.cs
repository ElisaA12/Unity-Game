using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergiaEnemy : MonoBehaviour
{
    [SerializeField] int vita;
    [SerializeField] int vitaMax;
    public GameObject gameObject;

    [SerializeField] GUISkin skinEnergia;

    // Start is called before the first frame update
    void Start()
    {
        vita=vitaMax;
        gameObject= GetComponent<GameObject> ();
    }

    // Update is called once per frame
    void Update()
    {
        if(vita>vitaMax){
            vita=vitaMax;
        }else if(vita<=0){
            //Application.LoadLevel("");
            vita=0;
            Destroy(gameObject);
        }

    }
    void OnGUI(){
        //partendo dalla fine dello schermo, tolgo -10 per tgli un magine
        GUI.Box (new Rect(Screen.width- Screen.width/8 -10,10,Screen.width/8,20), "VITA" + vita + "/" + vitaMax);
    
        if(vita>0){
            GUI.skin= skinEnergia;
            GUI.Box(new Rect(Screen.width- Screen.width/8 -10,10, Screen.width/4*((float)vita/(float)vitaMax),20),"");
        }
    }
    public void getDamage(int danno){
        vita-=danno;
    }
}
