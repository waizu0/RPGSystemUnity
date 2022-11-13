using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndMove : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /*This is a script that allows you to drag and move a game object.
    It can be used for the map, the character sheet, the inventory, etc.*/

    //The script must be attached to the game object that you want to drag and move

    public Vector3 _mousePosition; //The position of the mouse
    public bool _isDragging; //If the game object is being dragged
    float xPos, yPos; //The position of the game object
    Vector3 _originalPosition; //The original position of the game object
    Vector3 _originalScale; //The original scale of the game object

    void Start()
    {
        _originalScale = transform.localScale; //Sets the original scale of the game object
        _originalPosition = transform.position; //Sets the original position of the game object
    }

    //Using event systems, when the mouse is holding the object, the game object will be dragged
    public void OnPointerDown(PointerEventData pointerEventData)
    {
        _isDragging = true; //The game object is being dragged
    }

    //When the mouse is not holding the object, the game object will not be dragged
    public void OnPointerUp(PointerEventData pointerEventData)
    {
        _isDragging = false; //The game object is not being dragged
    }

    //If the game object is being dragged, the game object will follow the mouse
    void Update()
    {
        xPos = Input.mousePosition.x;
        yPos = Input.mousePosition.y;
            _mousePosition = Input.mousePosition; //The position of the mouse
            _mousePosition = Camera.main.ScreenToWorldPoint(_mousePosition); //The position of the mouse in world space
        if (_isDragging)
        {
            //Make the x rect transform position of the game object equal to the variable xPos
            transform.position = new Vector3(xPos, yPos, transform.position.z);
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            transform.position = _originalPosition; //Sets the position of the game object to the original position
        }

        //Debug if it's not being rendered on the screen
        if (transform.position.x > Screen.width || transform.position.x < 0 || transform.position.y > Screen.height || transform.position.y < 0)
        {
            transform.position = _originalPosition; //Sets the position of the game object to the original position
            transform.localScale = _originalScale; //Sets the scale of the game object to the original scale
        }
        
    }
    

    public void SetSize(float size)
    {
        if(size == 0.5f && transform.localScale != _originalScale/2f)
        {
            transform.localScale = _originalScale / 1.5f; //Sets the scale of the game object to half the original scale
            //No, 0.5x does not reduce the scale by half. It reduces it by 1.5x, 0.5x is too small and ugly, so I made it 1.5x. Sorry.
        }
        else if(size == 1f)
        {
            transform.localScale = _originalScale; //Sets the scale of the game object to the original scale
            //The 1x is the only true thing here lol
        }
        else if(size == 2f && transform.localScale != _originalScale * 2f)
        {

        
            transform.localScale = _originalScale * 1.5f; //Sets the scale of the game object to double the original scale
            //By the way, the 2x button is actually fake, it just makes the game object 1.5x bigger, 2x is too big to see anything, sorry!
        }
        
    }
}