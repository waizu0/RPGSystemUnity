using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //saveable in a file
public class PlayerData //not deriving from Monobehaviour
{
    public float value;

    //creating a constructor for our class? //act as a set of functions for our class PlayerData
    //this method below will automatically be invoked when an instance of this class is created.
    public PlayerData(Player player) //takes data from "UIScript"
    {
        value = player.value;

    }
}