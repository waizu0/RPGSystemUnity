using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; //Allows us to use EventSystems
using TMPro; //TextMeshPro, a cool text package
using System.IO; //Allows us to use files

public class CampaignButtons : MonoBehaviour, IPointerClickHandler
{

    GlobalManager _gm; //The global manager
    TextMeshProUGUI _campaignNameText; //The text that displays the campaign name
    GameObject _selectedCampaignPanel; //The panel that displays the selected campaign
    CreateCampaignScript _createCampaignScript; //The create campaign script

    void Start()
    {
        _gm = GameObject.Find("CampaignGlobalManager").GetComponent<GlobalManager>(); //Finds the global manager
        _campaignNameText = GetComponentInChildren<TextMeshProUGUI>(); //Finds the text that displays the campaign name
        _selectedCampaignPanel = GameObject.Find("SelectedCampaignPanel"); //Finds the panel that displays the selected campaign
        _createCampaignScript = GameObject.Find("Canvas").GetComponent<CreateCampaignScript>(); //Finds the create campaign script
    }

    //Detect if a click occurs
    public void OnPointerClick(PointerEventData pointerEventData)
    {
        _gm._selectedCampaign = _campaignNameText.text; //Sets the selected campaign to the name of the campaign button
        Debug.Log("Selected campaign: " + _gm._selectedCampaign); //Prints the selected campaign to the console
        // _selectedCampaignPanel.SetActive(true); //Activates the selected campaign panel
        _gm._selectedCampaignDescription = File.ReadAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/RPG Engine/Campaigns/" + _gm._selectedCampaign + "/Campaign Data" + "/campaignData.txt"); //Sets the selected campaign description to the description of the selected campaign
        

       
        
               
    }
}
