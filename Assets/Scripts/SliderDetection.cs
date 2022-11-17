using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SliderDetection : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    Slider _slider;
    public bool IsSliderClicked = false;

    void Start()
    {
        _slider = GetComponent<Slider>();
    }

     public void OnPointerDown(PointerEventData eventData)
     {
         IsSliderClicked = true;
     }
 
     public void OnPointerUp(PointerEventData eventData)
     {
         IsSliderClicked = false;
     }

     void Update()
     {
         if (IsSliderClicked)
         {
             Debug.Log("Slider is clicked");
         }
     }
 }
 
    

