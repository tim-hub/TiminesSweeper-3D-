using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour ,IPointerClickHandler,IPointerEnterHandler,
IPointerExitHandler{

    public List<Vector3> Directions =new List<Vector3>(); //to define the line's directions
    //for different shapes of objects
    public LayerMask ElementMask;
    [Tooltip("bigger than 1 distance, less than 2 distances, distance configged in gamemanager")]
    public float RayLength=2.1f;
    public float WaitingSeconds=.5f;

//    [Tooltip("Set the amount as the same as the directions, and keep the order")]
    public List<GameObject> DifferentNumbers=new List<GameObject>();
    public GameObject BlankElement;
    public GameObject MineElement;
    public Material FlagMat;
    public Material DefaultMat;

    private bool _isAMine=false;
    private bool _isABlank=false;
    private bool _isSweepered=false;
    private bool _isFlagged=false;
    private bool _rightClick=false;

    private bool _isPointerOnTheObject;


    private int _ownNumber;

    public bool IsABlank{

        get {return _isABlank; }
 

    }


    public bool IsAMine{

        get {return _isAMine; }
        set { _isAMine = value; }


    }

    public bool IsSweepered{

        get { return _isSweepered; }
    }

    public bool IsFlagged{

        get{ return _isFlagged; }
    }

    void Awake(){

        //Debug.Log(gameObject.name + " " + _ownNumber);

    }


    void Start(){
        _ownNumber = GetHowManyMinesNear();

        if (_ownNumber == 0)
        {
            _isABlank = true;


        }




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
                _rightClick=true;

                if(!_isFlagged){

                    GetComponent<Renderer>().material=FlagMat;
                    _isFlagged = true;

                    _rightClick=false;

                }else{
                    GetComponent<Renderer>().material=DefaultMat;
                    _isFlagged = false;

                    _rightClick=false;

                }
               

               
            }



        }

        #endif

    }

    void SweeperToSendLine(Vector3 direction){
        //wait


        // cast a ray along the direction
        //to dig the elements near



        Ray ray = new Ray(transform.position, direction);
        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, RayLength, ElementMask))
        {
            GameObject hitObject = hit.collider.gameObject;
            ElementControl hitObjectElement = hitObject.GetComponent<ElementControl>();


            if ((hitObjectElement!=null)&&(!(hitObjectElement.IsAMine))
                && (!hitObjectElement.IsSweepered) &&(!hitObjectElement.IsFlagged))
            {



                // not a mine and not be sweepered

                // sweeper the elements near
                if (hitObjectElement.IsABlank)
                {
                    
                    hitObjectElement.ClickOnABlank();


                } else
                {
                    hitObjectElement.SweeperThisElement();


                }

            }

        }

       


    }


//    IEnumerator WaitForASecond(){
//
//        yield return new WaitForSeconds(WaitingSeconds);
//
//    }

    void SetFlagTexture(){
        Debug.Log(gameObject.name + "is setting the texture to a flag");



    }
    int GetHowManyMinesNear(){ // to calculate how many mines near this element
        int ownNumber=0;

        foreach (Vector3 direction in Directions)
        {


            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;


            if (Physics.Raycast(ray, out hit, RayLength, ElementMask))
            {
            

                if (hit.collider.gameObject.GetComponent<ElementControl>().IsAMine)
                {
                    ownNumber++;
                }

            }
        }
        return ownNumber;

    }


    void SweeperThisElement(){
        Debug.Log(gameObject.name + "has " + _ownNumber + " mines near");

        GameObject go =Instantiate(DifferentNumbers [_ownNumber - 1], transform.position,
            Quaternion.Euler(new Vector3(-90,0,0)+transform.rotation.eulerAngles)) 
            as GameObject;

        go.transform.parent = this.gameObject.transform.parent;
        Destroy(this.gameObject);

    }

    void ClickOnABlank(){

        Debug.Log(gameObject.name + "is a blank element");


        GameObject go=Instantiate(BlankElement, transform.position,
            this.transform.rotation) as GameObject;

        go.transform.parent = this.gameObject.transform.parent;
        Destroy(this.gameObject);


        //to sweeper the near
        for (int i = 0; i < Directions.Count; i++)
        {
           SweeperToSendLine(Directions [i]);


        }

    }



    public void OnPointerClick(PointerEventData eventData){ // add a if for whether it is a flag

        if (!((_rightClick) ||(_isFlagged)))
        {

            _isSweepered = true;
            Debug.Log("mouse pointer click on" + gameObject.name);

            if (_isAMine)
            {
                //you falied, and game over
                Debug.Log(gameObject.name + " is a mine, you failed!");
                GameObject go = Instantiate(MineElement, transform.position,
                               transform.rotation) as GameObject;

                go.transform.parent = this.gameObject.transform.parent;
                Destroy(this.gameObject);

            } else if (_ownNumber == 0)
            {

                ClickOnABlank();
                //this is a blank element, then send line to sweeper elements near 



            } else
            { //this is an element with mines near



                SweeperThisElement();
           

            }
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
