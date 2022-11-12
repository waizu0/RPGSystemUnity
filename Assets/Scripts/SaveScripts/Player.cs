using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary; //allows access to BinaryFormatter
using System.IO; //namespace we want to use when working with files on Operating System

public class Player : MonoBehaviour
{
    //LOAD SAVE
    bool haveSave = false;
    public float value;

    private void Awake()
    {
        string path = Path.Combine(Application.persistentDataPath, "saves"); //Detecta se o save existe

        if (File.Exists(path))
        {
            haveSave = true;
        }

        Object.DontDestroyOnLoad(this.gameObject);
        
    }


    public void SavePlayer()
    {
        print("Save");
        SaveSystem.SavePlayer(this); //what's the point of passing "this" through to SavePlayer - it's used later to find this.transform.position
    }
    public void LoadPlayer()
    {
        print("Load");
        PlayerData data = SaveSystem.LoadPlayer();
        value = data.value;
    }

    private void Update()
    {
            if (Input.GetKeyDown(KeyCode.T))
        {
            SavePlayer();
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            LoadPlayer();
        }
    }

}
    