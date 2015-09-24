using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

    public static GameControl control;

    public float playerHealth;
    public float playerExperience;

    //Save code on enable and disable if you want auto saving.

	// Use this for initialization
	void Awake () {
        if (control == null)
        {
            DontDestroyOnLoad(gameObject);
            control = this;
        }
        else if (control != this)
        {
            Destroy(gameObject);
        }
        
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);

    }

    [Serializable]
    class playerData
    {
        //Implement getter and setters later.
        public float playerHealth;
        public float playerExperience;



    }
}
