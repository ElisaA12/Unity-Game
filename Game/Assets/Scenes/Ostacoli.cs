using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ostacoli : MonoBehaviour
{

    private bool goUp;
    private bool attiva=false;
    public float speed = 1;
    public int id;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Attiva());
    }

    // Update is called once per frame
    void Update()
    {
        if (attiva)
        {
            if (goUp)
            {
                transform.position = transform.position + new Vector3(0, 0, speed * Time.deltaTime);
            }
            else
            {
                transform.position = transform.position - new Vector3(0, 0, speed * Time.deltaTime);
            }
        }

    }
    IEnumerator SwitchDirection()
    {
        while (gameObject.activeSelf)
        {


            yield return new WaitForSeconds(2f);
            goUp = !goUp;
        }
    }
    IEnumerator Attiva()
    {


        yield return new WaitForSeconds(id); //Attendi due secondi
        attiva = true;
        StartCoroutine(SwitchDirection());

    }

}
