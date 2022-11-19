using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a text package :)

public class PlaySongButton : MonoBehaviour
{

    public TextMeshProUGUI songNameText; //TMP text component
    public int _characLimit; //Max characters to display

    private void Awake()
     {
        songNameText = GetComponentInChildren<TextMeshProUGUI>(); //Get the TMP text component
    }

    public void PlaySong()
    {
        GameController _gc = GameObject.Find("Canvas").GetComponent<GameController>(); //Finds the game controller

        string _songName = gameObject.GetComponentInChildren<TextMeshProUGUI>().text; //Gets the song name from the button text
        
        _gc.PlaySong(_songName); //Plays the song
        
    }

    private void Update()
    {
        if (songNameText.text.Length > _characLimit) //If the song name is longer than the limit
        {
            songNameText.text = songNameText.text.Substring(0, _characLimit) + "..."; //Shorten the song name
        }    
    }
}
