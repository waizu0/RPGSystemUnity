using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a cool text package
using System.IO; //Allows us to use files
using UnityEngine.SceneManagement; //Allows us to use scenes
using UnityEngine.UI; //Allows us to use UI elements

public class CreateCampaignScript : MonoBehaviour
{
    //This script will manage the creation of a new campaign for the RPG engine :)

    public string _campaignName; //The name of the campaign

    [TextArea(5, 6)]
    public string _campaignDescription; //The description of the campaign

    public TMP_InputField _campaignNameBox; //The input field for the campaign name
    public TMP_InputField _campaignDescriptionBox; //The input field for the campaign description

    string _campaignPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/"; //The path to the campaigns folder

    public int _characterLimitDescription = 126; //The character limit for the campaign description
    public int _characterLimitName = 20; //The character limit for the campaign name

    public int _characterCountDescription; //The character count for the campaign description
    public int _characterCountName; //The character count for the campaign name

    public TextMeshProUGUI _characterCountDescriptionText; //The text that displays the character count for the campaign description
    public TextMeshProUGUI _characterCountNameText; //The text that displays the character count for the campaign name
    public TextMeshProUGUI _campaignExistsText; //The text that displays if the campaign already exists
    
    public GameObject _content; //The content of the scroll view, we'll use this to instantiate the campaign buttons
    public Transform _campaignButton; //The campaign button prefab

    public GlobalManager _gm; //The global manager

    ///////Selected the campaign start
    public TextMeshProUGUI _selectedCampaignNameText; //The text that displays the selected campaign
    public TextMeshProUGUI _selectedCampaignDescriptionText; //The text that displays the selected campaign description
    public GameObject _selectedCampaignPanel; //The panel that displays the selected campaign
    ///////Selected the campaign end
    
    public Button _doneButton; //The done button
    public GameObject _campaignListDisplayGameObject; //The game object that displays the campaign list
    


    //TypeCampaignName is called when the player types in the campaign name input field
    public void TypeCampaignName()
    {
        _campaignName = _campaignNameBox.text; //Sets the campaign name to the text in the input field
        //This void will be called when the user types in the campaign name input field, using the OnValueChanged() event

        _campaignNameBox.characterLimit = _characterLimitName; //Sets the character limit for the campaign name input field
        _campaignDescriptionBox.characterLimit = _characterLimitDescription; //Sets the character limit for the campaign description input field
    }

    //TypeCampaignDescription is called when the player types in the campaign description input field
    public void TypeCampaignDescription()
    {
        _campaignDescription = _campaignDescriptionBox.text; //Sets the campaign description to the text in the input field
        //This void will be called when the user types in the campaign description input field, using the OnValueChanged() event
    }

    void Awake()
    {
        // Directory.CreateDirectory(_campaignPath + "Sample Campaign"); //Create a sample folder for the campaigns, test only
        _gm = GameObject.Find("CampaignGlobalManager").GetComponent<GlobalManager>(); //Finds the global manager

    }

    public void Update()
    {
        _characterCountDescription = _campaignDescriptionBox.text.Length; //Sets the character count for the campaign description to the length of the text in the input field
        _characterCountDescriptionText.text = _characterCountDescription + "/" + _characterLimitDescription; //Sets the text to the character count and the character limit


        _characterCountName = _campaignNameBox.text.Length; //Sets the character count for the campaign name to the length of the text in the input field
        _characterCountNameText.text = _characterCountName + "/" + _characterLimitName; //Sets the text to the character count and the character limit
      

    
        if (_campaignNameBox.isFocused && Input.GetKeyDown(KeyCode.Tab))
        {
            /*If the _campaignNameBox is focused and the player presses the tab key,
             the _campaignDescriptionBox will be focused*/
            _campaignDescriptionBox.Select(); //Focuses the _campaignDescriptionBox
        }

        //Just for testing purposes, when pressing the space bar, the load campaigns function will be called
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     LoadCampaigns();
        // }

        LoadACampaign();

        if (_campaignName == "")
        {
            //This will turn the button not interactable if the campaign name is empty
            _doneButton.interactable = false;
        }
        else
        {
            //But if the campaign name is not empty, the done button will be interactable
            _doneButton.interactable = true;
        }

    }

    //Done is called when the player clicks the done button
    public void Done()
    {

        
        
        if (Directory.Exists(_campaignPath + _campaignName))
        {
            Debug.Log("Campaign already exists!"); //If the campaign already exists, log it, just for dev stuff!
            _campaignExistsText.gameObject.SetActive(true); //Sets the campaign exists text to active
        }
        else
        {

            _campaignExistsText.gameObject.SetActive(false); //Sets the campaign exists text to inactive
            Directory.CreateDirectory(_campaignPath + _campaignName); //Creates a folder for the campaign in the campaigns folder with the campaign name
        
            string campaignData = Directory.CreateDirectory(_campaignPath + _campaignName + "/Campaign Data").ToString(); //Creates a folder for the campaign data in the campaign folder
            
            
            string campaignDataPath = _campaignPath + _campaignName + "/Campaign Data/campaignDescription.txt"; //The path to the campaign data file
            File.WriteAllText(campaignDataPath, _campaignDescription); //Creates a file for the campaign data and writes the campaign description to it

            Debug.Log("Campaign created!");
            LoadCampaigns(); //Loads the campaigns
        }

    }
    
    //LoadCampaigns is called when the player clicks the load campaigns button
    public void LoadCampaigns()
    {
        int numberOfCampaigns = Directory.GetDirectories(_campaignPath).Length; //Gets the number of folders in the campaigns folder 

        //Then it'll create a for loop that will create a button for each campaign
        for (int i = 0; i < numberOfCampaigns; i++)
        {
            

            //Instantiate the campaign button
            Transform campaignButton = Instantiate(_campaignButton, _content.transform.position, Quaternion.identity); //Instantiates the campaign button
            campaignButton.SetParent(_content.transform); //Sets the parent of the campaign button to the content

            string campaignName = Directory.GetDirectories(_campaignPath)[i].Substring(_campaignPath.Length); //Gets the name of the campaign
            campaignButton.GetComponentInChildren<TextMeshProUGUI>().text = campaignName; //Sets the text of the campaign button to the campaign name
            campaignButton.name = campaignName; //Sets the name of the campaign button to the campaign name


        }
        
    }

    //LoadACampaign is called when the player clicks a campaign button
    public void LoadACampaign()
    {
       
       if(_gm._selectedCampaign != "")
       {
         _selectedCampaignPanel.SetActive(true); //Sets the selected campaign panel to inactive
        _selectedCampaignNameText.text = _gm._selectedCampaign; //Sets the selected campaign name text to the campaign name
        _selectedCampaignDescriptionText.text = _gm._selectedCampaignDescription; //Sets the selected campaign description text to the campaign description
       }
    }

    public void Return(){

        //Void called when the return button is clicked, it'll destroy the campaign list display game object, so when load again, it'll create a new one
        GameObject[] options = GameObject.FindGameObjectsWithTag("Options"); //Finds all the options
        foreach (GameObject option in options)
        {
            Destroy(option); //Destroys the options 
        }
    }
    
}
