using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a cool text package

public class GameController : MonoBehaviour
{
    public int _d20Roll; //The result of the d20 roll
    public TextMeshProUGUI _d20RollText; //The text that displays the d20 roll
    public AudioSource _gameSource; //The sound that plays when the d20 is rolled
    public AudioClip _diceClip; //The sound that plays when the d20 is rolled

    public void RollD20()
    {
        _d20Roll = Random.Range(1, 21); //Rolls a d20
        _d20RollText.text = _d20Roll.ToString(); //Sets the text to the result of the d20 roll
        Debug.Log("Roll: " + _d20Roll); //Prints the result of the d20 roll to the console
        _gameSource.PlayOneShot(_diceClip); //Plays the dice sound
    }

}
