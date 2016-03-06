using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour ,IPointerClickHandler,IPointerEnterHandler,
IPointerExitHandler{

    public List<Vector3> Directions =new List<Vector3>(); //to define the line's directions
    //for different shapes of objects
    public LayerMask ElementMask;


    private bool _isAMine;
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


    }


    void Start(){

        _isSweepered = false;
        _isFlagged = false;

        _isPointerOnTheObject = false;


    }

    void Update(){
        #if UNITY_ANDROID



        #else

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

        Ray ray = new Ray(transform.position, direction);




        //set the texture by the own number
        GetComponent<Renderer>().sharedMaterial.mainTexture;


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




        for (int i = 0; i < Directions.Count; i++)
        {
            ClickToSendLine(Directions [i]);




        }

    }

    public void OnPointerEnter(PointerEventData eventData){

        _isPointerOnTheObject = true;

    }


    public void OnPointerExit(PointerEventData eventData){
        _isPointerOnTheObject = false;



    }
}
