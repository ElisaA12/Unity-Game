using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energia : MonoBehaviour
{
    [SerializeField] int vita;
    [SerializeField] int vitaMax;

    [SerializeField] GUISkin skinEnergia;

    // Start is called before the first frame update
    void Start()
    {
        vita=vitaMax;
    }

    // Update is called once per frame
    void Update()
    {
        if(vita>vitaMax){
            vita=vitaMax;
        }else if(vita<=0){
            //Application.LoadLevel("");
            vita=0;
        }

    }
    void OnGUI(){
        GUI.Box (new Rect(10,10,Screen.width/4,20), "VITA" + vita + "/" + vitaMax);
    
        if(vita>0){
            GUI.skin= skinEnergia;
            GUI.Box(new Rect(10,10, Screen.width/4*((float)vita/(float)vitaMax),20),"");
        }
    }
}
