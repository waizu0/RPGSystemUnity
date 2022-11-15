using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a text package :)

public class PlaySongButton : MonoBehaviour
{
    public void PlaySong()
    {
        GameController _gc = GameObject.Find("Canvas").GetComponent<GameController>(); //Finds the game controller

        string _songName = gameObject.GetComponentInChildren<TextMeshProUGUI>().text; //Gets the song name from the button text
        
        _gc.PlaySong(_songName); //Plays the song
        
    }
}
