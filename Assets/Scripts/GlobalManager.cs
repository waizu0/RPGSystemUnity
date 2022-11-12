using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }


    public string _selectedCampaign; //The name of the selected campaign

    [TextArea(5, 6)]
    public string _selectedCampaignDescription; //The description of the selected campaign
}
