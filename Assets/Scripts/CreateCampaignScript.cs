using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //TextMeshPro, a cool text package
using System.IO; //Allows us to use files

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


    //TypeCampaignName is called when the player types in the campaign name input field
    public void TypeCampaignName()
    {
        _campaignName = _campaignNameBox.text; //Sets the campaign name to the text in the input field
        //This void will be called when the user types in the campaign name input field, using the OnValueChanged() event

        _campaignNameBox.characterLimit = _characterLimitName; //Sets the character limit for the campaign name input field
        _campaignDescriptionBox.characterLimit = _characterLimitDescription; //Sets the character limit for the campaign description input field
    }

    void Awake()
    {
        // Directory.CreateDirectory(_campaignPath + "Sample Campaign"); //Create a sample folder for the campaigns, test only

    }

    public void Update()
    {
        _characterCountDescription = _campaignDescriptionBox.text.Length; //Sets the character count for the campaign description to the length of the text in the input field
        _characterCountDescriptionText.text = _characterCountDescription + "/" + _characterLimitDescription; //Sets the text to the character count and the character limit


        _characterCountName = _campaignNameBox.text.Length; //Sets the character count for the campaign name to the length of the text in the input field
        _characterCountNameText.text = _characterCountName + "/" + _characterLimitName; //Sets the text to the character count and the character limit
    }

    //Done is called when the player clicks the done button
    public void Done()
    {

        
        
        if (Directory.Exists(_campaignPath + _campaignName))
        {
            Debug.Log("Campaign already exists!");
        }
        else
        {
            Directory.CreateDirectory(_campaignPath + _campaignName); //Creates a folder for the campaign in the campaigns folder with the campaign name
        
            string campaignData = Directory.CreateDirectory(_campaignPath + _campaignName + "/Campaign Data").ToString(); //Creates a folder for the campaign data in the campaign folder
            
            
            string campaignDataPath = _campaignPath + _campaignName + "/Campaign Data/campaignData.txt"; //The path to the campaign data file

            Debug.Log("Campaign created!");
        }

    }
    
    
}
