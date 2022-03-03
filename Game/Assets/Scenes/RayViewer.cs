using UnityEngine;
using System.Collections;

public class RayViewer : MonoBehaviour {
    //per creare il laser dalla pistola all oggetto
    public float weaponRange = 50f;                       
    private Camera fpsCam;                               

    void Start () 
    {
        fpsCam = GetComponentInParent<Camera>();
    }


    void Update () 
    {
        Vector3 lineOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0.0f));

        Debug.DrawRay(lineOrigin, fpsCam.transform.forward * weaponRange, Color.red);
    }
}