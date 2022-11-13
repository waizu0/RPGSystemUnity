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
        Texture2D texture = new Texture2D(2, 2); //Creates a new texture


        /*When the game starts, the map will load the last used map, we know what the last used map is because
        in the Maps folder in Documents/RPG Engine/Campaigns/Campaign Data, there is a txt file
        called mapData.txt, it contains the name of the last used PNG file, we must import it from
        the Maps folder in Documents/RPG Engine/Campaigns/Campaign Data/Maps/, and set as the image of the _mapImage*/
        
        //The path to the mapData.txt file, Documents/RPG Engine/Campaigns/[_gm._selectedCampaign]/Campaign Data/mapData.txt
        
        string mapDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data/mapData.txt";
        string image = File.ReadAllText(mapDataPath) + ".png"; //The name of the last used PNG file
        texture.LoadImage(File.ReadAllBytes(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Maps/" + image));
        
        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100);
        _mapImage.sprite = sprite; //Sets the image of the _mapImage to the last used map
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


        //Make that the mapData.txt file contains the name of the last used PNG file
        string mapDataPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data/mapData.txt";
        File.WriteAllText(mapDataPath, RandomMapName);

    }
    
}
