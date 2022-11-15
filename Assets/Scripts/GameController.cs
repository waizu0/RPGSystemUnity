using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a cool text package
using System.IO; //Allows us to use files
using UnityEngine.UI; //Allows us to use UI elements
using SFB; //Standalone File Browser, a cool file browser package

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

    public bool _isMaster; //Is the player the master of the campaign?
    public bool _isPlayer; //Is the player a player in the campaign?
    public string _campaignName; //The name of the campaign
    public TextMeshProUGUI _campaignNameText; //The text that displays the campaign name

    public Transform _songButtonPrefab; //The song button prefab
    public GameObject _songContent; //The content of the scroll view, we'll use this to instantiate the song buttons
    public AudioClip _currentSong; //The current song that is playing, imported
    public bool _RandomizeSongs; //Should the songs be randomized?

    public GameObject _randomizeButton; //The randomize button
    public List<string> _songNames = new List<string>(); //The names of the songs
    public int _currentSongIndex; //The index of the song
    public string _currentSongName; //The name of the current song


   void Update()
    {
        //if the current song isn't null, and ended, play the next song
         if (_currentSong != null && !_gameSource.isPlaying)
         {
               PlayNextSong();
         }

         CurrentPlayingIndex();

    }


    private void Awake()
     {
        _gm = GameObject.Find("CampaignGlobalManager").GetComponent<GlobalManager>(); //Finds the global manager


        
         _campaignName = _gm._selectedCampaign; //Sets the campaign name to the campaign name in the global manager
         _campaignNameText.text = _campaignName; //Sets the text that displays the campaign name to the campaign name
        _gameSource = GetComponent<AudioSource>(); //Finds the audio source
        _folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign; //Sets the folder path to the path to the folder that contains the campaign data
    }

    void Start()
    {
       InstantiateSubFolders(); //Instantiates the subfolders
       InstantiateAllSongs(); //Instantiates all the songs
       _currentSongName = _songNames[0]; //Sets the current song name to the first song name
    }

    public void CurrentPlayingIndex()
    {
        
            for (int i = 0; i < _songNames.Count; i++)
            {
                if (_currentSongName == _songNames[i])
                {
                    _currentSongIndex = i;
                }
            }
        
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
         _d20RollText.text = _currentDiceLimit.ToString(); //Sets the text of the d20 roll to the max value of the dice
    }


    //////////////////////////////////PLAYLIST STUFF////////////////////////////////////

    public void ImportSong()
    {

         string backSlash = System.IO.Path.DirectorySeparatorChar.ToString(); //We can't just use \ to remove the last character of a string, so we have to use this
         string path = StandaloneFileBrowser.OpenFilePanel("Open File", "", "mp3", false)[0]; //Opens the file browser and gets the path to the file, only mp3 files are allowed
         
         string songName = path.Substring(path.LastIndexOf(backSlash) + 1); //Gets the name of the song
         songName = songName.Replace(".mp3", ".mp3"); //Removes the .mp3 from the song name


         Transform _newSongButton = Instantiate(_songButtonPrefab, transform.position, Quaternion.identity); //Instantiates the song button
         _newSongButton.SetParent(_songContent.transform, false); //Sets the parent of the new song button to the song content

         WWW www = new WWW("file:///" + path); //Creates a new WWW object
         _currentSong = www.GetAudioClip(false, true); //Sets the current song to the song that was imported
         _newSongButton.GetComponentInChildren<TextMeshProUGUI>().text = songName; //Sets the text of the new song button to the name of the song

         _gameSource.clip = _currentSong; //Sets the clip of the audio source to the current song

         if(!_gameSource.isPlaying)
         {
             _gameSource.Play();
         }

         File.Copy(path, _folderPath + "/Songs/" + songName); //Copies the song to the songs folder


    }

    public void StopSong()
    {
         _gameSource.Pause(); //Pauses the song
    }

    public void ContinueSong()
    {
         _gameSource.UnPause(); //Unpauses the song
    }

    public void RandomOrder()
    {
         _gameSource.loop = false; //Sets the loop of the audio source to false
        
         List<string> songs = new List<string>();
         foreach (string song in Directory.GetFiles(_folderPath + "/Songs/")) //This foreach loop gets all the songs in the songs folder
         {
             songs.Add(song); //Adds the song to the list
         }

         string randomSong = songs[Random.Range(0, songs.Count)]; //Gets a random song from the list
         WWW www = new WWW("file:///" + randomSong); //Creates a new WWW object
         _currentSong = www.GetAudioClip(false, true); //Sets the current song to the song that was imported
         _gameSource.clip = _currentSong; //Sets the clip of the audio source to the current song
         _gameSource.Play(); //Plays the song

         _RandomizeSongs = !_RandomizeSongs; //Toggles the randomize songs bool
         _randomizeButton.GetComponentInChildren<TextMeshProUGUI>().color = _RandomizeSongs ? Color.green : Color.white; //Sets the color of the randomize button to green if the randomize songs bool is true, otherwise it sets it to white
    }

    public void InstantiateAllSongs()
    {
         //This void will instantiate all music buttons in the music panel
         string backSlash = "/"; //I use it as a variable, because if i change from / to \, it will be so much easier to change
         string songName = ""; //The name of the song

         //This foreach loop gets all the songs in the songs folder
         foreach (string song in Directory.GetFiles(_folderPath + "/Songs/"))
         {
             Transform _newSongButton = Instantiate(_songButtonPrefab, transform.position, Quaternion.identity); //Instantiates the song button
             _newSongButton.SetParent(_songContent.transform, false); //Sets the parent of the new song button to the song content

             songName = song.Substring(song.LastIndexOf(backSlash) + 1); //Gets the name of the song
             songName = songName.Replace(".mp3", ""); //Removes the .mp3 from the song name
             _newSongButton.GetComponentInChildren<TextMeshProUGUI>().text = songName; //Sets the text of the new song button to the name of the song
             print("Song: " + songName);
            _songNames.Add(songName); //Adds the song name to the list of song names

         }


    }

    public void PlayNextSong()
    {
         _gameSource.Stop(); //Stops the current song

         if(_RandomizeSongs)
         {
             RandomOrder(); //Plays a random song
         }
         else
         {
            
            //If the list ended, play the 0 song
            if(_currentSongIndex == _songNames.Count - 1)
            {
                _currentSongIndex = 0;
            }
            else
            {
               _currentSongIndex+=1; //If the list didn't end, play the next song by increasing the index by 1
                WWW www = new WWW("file:///" + _folderPath + "/Songs/" + _songNames[_currentSongIndex] + ".mp3"); //Plays the next song
            }
         }
    }

    public void PlaySong(string song)
      {
            _gameSource.Stop(); //Stops the current song
   
            string path = _folderPath + "/Songs/" + song + ".mp3"; //Gets the path to the song
            WWW www = new WWW("file:///" + path); //Creates a new WWW object
            _currentSong = www.GetAudioClip(false, true); //Sets the current song to the song that was imported
            _gameSource.clip = _currentSong; //Sets the clip of the audio source to the current song
            _gameSource.Play(); //Plays the song
      }

      public void PlayStart()
      {
         if(_RandomizeSongs)
         {
             RandomOrder(); //Plays a random song
         }
         else
         {
             //Play the element 0 of the list
               WWW www = new WWW("file:///" + _folderPath + "/Songs/" + _songNames[0] + ".mp3"); //Plays the first song in the list
         }
      }
}
