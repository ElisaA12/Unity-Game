using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proiettile : MonoBehaviour
{ 
    public Rigidbody rig;
    public float speed=50f;

    void Start () {
		
	}
	

	void Update () {
        rig.AddForce(transform.forward* speed);
	}
}
