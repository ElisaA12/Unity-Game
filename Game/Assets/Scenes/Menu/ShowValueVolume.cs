using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class ShowValueVolume : MonoBehaviour
{
    Text percentageText;
    // Start is called before the first frame update
    void Start()
    {
        percentageText = GetComponent<Text>();
    }
    public void textUpdate(float value)
    {
        percentageText.text = Mathf.RoundToInt(value * 100) + "%";
    }

    
}
