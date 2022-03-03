using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameControl : MonoBehaviour
{/*
    public static GameControl control;

    public float healt;
    public float experience;

    // Start is called before the first frame update
    void Start()
    {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }else if (contrl!= this)
        {
            Destry(gameObject);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.healt = healt;
        data.experience = experience;
        //aggiungo i dati nel file
        bf.Serialize(file, data);

        file.Close();


    }
    public void Load()
    {
        if(File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            BinaryFormater bf = new BinaryFormater();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            //Deserialized = scomponi il file e prendi. Da object a playerData
            PlayerData data =(PlayerData) bf.Deserialized(file);
            file.Close();

            //aggiorniamo
            healt = data.healt;
            experience = data.experience;
        }
    }*/
}
class PlayerData
{
    public float healt;
    public float experience;
}