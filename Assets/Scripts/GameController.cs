using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a cool text package
using System.IO; //Allows us to use files
using UnityEngine.UI; //Allows us to use UI elements

public class GameController : MonoBehaviour
{
    public int _d20Roll; //The result of the d20 roll
    public TextMeshProUGUI _d20RollText; //The text that displays the d20 roll
    public AudioSource _gameSource; //The sound that plays when the d20 is rolled
    public AudioClip _diceClip; //The sound that plays when the d20 is rolled
    public string _folderPath; //The path to the folder that contains the campaign data
    public GlobalManager _gm; //The global manager
    public GameObject foldersPanel; //The panel that displays the folders
    public Transform _folderButton; //The button that displays the folder name
    
    public Sprite[] _dices; //The sprites of the dices
    public int[] _diceMaxValues; //The max values of the d20, D20 = 20, D12 = 12, D10 = 10, D8 = 8, D6 = 6, D4 = 4
    public Button _diceButton; //The button that rolls the d20
    public int _currentDiceLimit; //The current limit of the dice

    private void Awake()
     {
        _gm = GameObject.Find("CampaignGlobalManager").GetComponent<GlobalManager>(); //Finds the global manager
        _gameSource = GetComponent<AudioSource>(); //Finds the audio source
        _folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign; //Sets the folder path to the path to the folder that contains the campaign data
    }

    void Start()
    {
       InstantiateSubFolders(); //Instantiates the subfolders
    }

    public void RollD20()
    {
        _d20Roll = Random.Range(1, _currentDiceLimit+1); //Rolls a d20
        _d20RollText.text = _d20Roll.ToString(); //Sets the text to the result of the d20 roll
        Debug.Log("Roll: " + _d20Roll); //Prints the result of the d20 roll to the console
        _gameSource.PlayOneShot(_diceClip); //Plays the dice sound
    }

    public void InstantiateSubFolders()
    {

       for(int i = 0; i < Directory.GetDirectories(_folderPath).Length; i++)
       {

            //Get the name of the subfolder
            string subFolderName = Directory.GetDirectories(_folderPath)[i].Substring(Directory.GetDirectories(_folderPath)[i].LastIndexOf("/") + 1);
        string subFolderNameWithoutCampaignName = subFolderName.Replace(_gm._selectedCampaign, ""); //Removes the campaign name from the subfolder name
           Transform _newFolderButton = Instantiate(_folderButton, transform.position, Quaternion.identity); //Instantiates the folder button
              _newFolderButton.SetParent(foldersPanel.transform, false); //Sets the parent of the new folder button to the folders panel
                _newFolderButton.GetComponentInChildren<TextMeshProUGUI>().text = subFolderNameWithoutCampaignName; //Sets the text of the new folder button to the name of the subfolder
       }
    }


    public void setMaxValue(int value)
    {
      //0 = D20, 1 = D12, 2 = D10, 3 = D8, 4 = D6, 5 = D4
         
         _currentDiceLimit = _diceMaxValues[value]; //Sets the current dice limit to the max value of the dice
         _diceButton.GetComponent<Image>().sprite = _dices[value]; //Sets the sprite of the dice button to the sprite of the dice
    }

}
