using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour ,IPointerClickHandler,IPointerEnterHandler,
IPointerExitHandler{

    public List<Vector3> Directions =new List<Vector3>(); //to define the line's directions
    //for different shapes of objects
    public LayerMask ElementMask;


    private bool _isAMine=false;
    private bool _isSweepered;
    private bool _isFlagged;


    private bool _isPointerOnTheObject;


    private int _ownNumber;

    public bool IsAMine{

        get {return _isAMine; }
        set { _isAMine = value; }


    }
    void Awake(){

        _ownNumber = GetHowManyMinesNear();
        //Debug.Log(gameObject.name + " " + _ownNumber);

    }


    void Start(){

        _isSweepered = false;
        _isFlagged = false;

        _isPointerOnTheObject = false;


    }

    void Update(){
        #if UNITY_ANDROID
        // long press


        #else
        //the mouse input


        if (_isPointerOnTheObject){ //pointer on the object


            if (Input.GetMouseButtonDown(1)) //right click
            {
                _isFlagged = true;


            }



        }

        #endif

    }

    void ClickToSendLine(Vector3 direction){
        // cast a ray along the direction
        //to dig the elements near

        Ray ray = new Ray(transform.position, direction);





       


    }


    void SetFlagTexture(){
        Debug.Log(gameObject.name + "is setting the texture to a flag");



    }
    int GetHowManyMinesNear(){ // to calculate how many mines near this element
        int ownNumber=0;

        foreach (Vector3 direction in Directions)
        {


            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, 100f, ElementMask))
            {
            

                if (hit.collider.gameObject.GetComponent<ElementControl>().IsAMine)
                {
                    ownNumber++;
                }

            }
        }
        return ownNumber;

    }


    public void OnPointerClick(PointerEventData eventData){

        Debug.Log("mouse pointer click on" + gameObject.name);

        if (_isAMine)
        {
            //you falied, and game over


        } else if (_ownNumber == 0)
        {
            Debug.Log(gameObject.name + "is a blank element");




            //this is a blank element, then send line to sweeper elements near 


            for (int i = 0; i < Directions.Count; i++)
            {
                ClickToSendLine(Directions [i]);




            }
        } else
        { //this is an element with mines near

//            for (int i = 1; i++; i <= Directions.Count)
//            {
//                
//
//
//
//            }

            //do not use a for loop, but use a string with a number inside to get the right texture

            Debug.Log(gameObject.name + "has " + _ownNumber + " mines near");



        }
    }

    public void OnPointerEnter(PointerEventData eventData){
        //Debug.Log("mouse pointer enter on" + gameObject.name);


        _isPointerOnTheObject = true;

    }


    public void OnPointerExit(PointerEventData eventData){
        _isPointerOnTheObject = false;



    }
}
