using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField]
    Transform camTransform;

    [SerializeField]
    LineRenderer lineRenderer;

    [SerializeField]
    float distance = 100;

    [SerializeField]
    float projectileForce = 100;

    LayerMask hittableMask;
    LayerMask mapMask;
    void Start()
    {
        hittableMask = (1 << LayerMask.NameToLayer("Terra"));
        //mapMask = (1 << LayerMask.NameToLayer("Map")) | hittableMask;
    }
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, distance, mapMask, QueryTriggerInteraction.Ignore))
        {
            lineRenderer.SetPosition(1, lineRenderer.transform.InverseTransformPoint(hit.point));
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(camTransform.position, camTransform.forward, out hit, distance, hittableMask, QueryTriggerInteraction.Ignore))
            {
                Rigidbody colRigidbody = hit.collider.gameObject.GetComponent<Rigidbody>();
                if (!colRigidbody) { return; }
                colRigidbody.isKinematic = false;
                colRigidbody.AddForceAtPosition(camTransform.forward * projectileForce, hit.point, ForceMode.Impulse);

                Debug.LogError("Hai colpito: " + hit.collider.gameObject);
            }
        }
    }

}
