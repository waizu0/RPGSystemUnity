using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //Allows us to use EventSystems
using System.IO; //Allows us to use files
using UnityEngine.UI; //Allows us to use UI elements
using UnityEditor; //Allows us to use the editor


public class ImportMap : MonoBehaviour, IPointerClickHandler
{
    public GameController _gameController; //The game controller
    public GlobalManager _gm; //The global manager
    public Image _mapImage; //The image of the map

    void Start()
    {
        _mapImage = GetComponent<Image>(); //Finds the image of the map
        _gm = GameObject.Find("CampaignGlobalManager").GetComponent<GlobalManager>(); //Finds the global manager

        //Detect if exists a mapData.txt inside Campaign Data of the current campaign
        if (File.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data" + "/mapData.txt"))
        {
            
            //Read the mapData.txt file
            string[] mapData = File.ReadAllLines(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data" + "/mapData.txt");

            //Set the map name to the name of the map
            _gm._mapName = mapData[0] + ".png";

            Debug.Log("Map name: " + _gm._mapName); //Prints the map name to the console

            //Load the map image from the map folder inside the selected campaign on documents
            var storage = _mapImage.sprite = AssetDatabase.LoadAssetAtPath<Sprite>(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Maps/" + _gm._mapName);

            if(storage == null)
            {
               Debug.LogError("Map not found, the variable storage is " + storage); //Prints the error to the console
            }
            else
            {
                Debug.Log("Map found"); //Prints the map found to the console
            }
        }
    }

    

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        string path = EditorUtility.OpenFilePanel("Overwrite with png, jpg, or jpeg", "", "png,jpg,jpeg"); //Opens a file panel to select a file
        Texture2D texture = new Texture2D(2, 2); //Creates a new texture
        
        //if don't exist, create a subfolder on the campaign folder called 'Maps'
        if (!Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Maps"))
        {
            Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Maps");
        }

        string RandomMapName = System.Guid.NewGuid().ToString(); //Creates a random map name
        File.WriteAllBytes(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Maps/" + RandomMapName + ".png", File.ReadAllBytes(path)); //Writes the selected image to the file

        texture.LoadImage(File.ReadAllBytes(path)); //Loads the selected image into the texture

        _mapImage.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100f); //And finally, put this texture in the _mapImage

        _gm._mapName = RandomMapName + ".png";

        File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data" + "/mapData.txt", RandomMapName); //Writes the random map name to the file, it'll load the map when the campaign is loaded
    }
    
}
