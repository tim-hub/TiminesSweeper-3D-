using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour ,IPointerClickHandler,IPointerEnterHandler,
IPointerExitHandler{

    public List<GameObject> Directions =new List<GameObject>(); //to define the line's directions
    //for different shapes of objects
    public LayerMask ElementMask;
    [Tooltip("bigger than 1 distance, less than 2 distances, distance configged in gamemanager")]
    public float RayLength=2.1f;
    public float WaitingSeconds=.5f;

    public GameObject ShootingParticls;
	public GameObject AudioEffect;

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




    void Start(){
        _ownNumber = GetHowManyMinesNear();
        InitThisElement();
        _isSweepered = false;
        _isFlagged = false;

        _isPointerOnTheObject = false;




    }


    void InitThisElement(){
        

        if (_ownNumber == 0)
        {
            _isABlank = true;


        }


    }

    void Update(){
        #if UNITY_ANDROID
        // long press


        #else
        //the mouse input


        if (_isPointerOnTheObject){ //pointer on the object


            if (Input.GetMouseButtonDown(1) ) //right click
            {

                PlayClickAudio();

                _rightClick=true;

                if(!_isFlagged){

                    GetComponent<Renderer>().material=FlagMat;
                    _isFlagged = true;
                   
                    GameManager.instance.FlagOne();

                    if(_isAMine){

                        GameManager.instance.FlagRightOne();
                    }



                    StartCoroutine(SetRightClickFalse()); //use this corountine to avoid to cancle flag to sweeper

                }else{
                    GetComponent<Renderer>().material=DefaultMat;
                    _isFlagged = false;

                    GameManager.instance.UnFlagOne();


                    StartCoroutine(SetRightClickFalse());

                }

            }

        }

        #endif

    }

    IEnumerator SetRightClickFalse(){
        yield return new WaitForSeconds(WaitingSeconds);

        _rightClick = false;

    }


    void PlayClickAudio(){
        
		Instantiate(AudioEffect,transform.position,transform.rotation);
		// the audio will be destroyed
	

    }



    void SweeperToSendParticle(GameObject direction){

		Debug.Log("send particle to sweeper");

        Vector3 startPosition = transform.position + 
            (direction.transform.position-transform.position) * transform.localScale.x / 2;
        Quaternion startRotation = transform.rotation;

        GameObject ps = Instantiate
            (ShootingParticls, startPosition, startRotation)
            as GameObject;

        ps.transform.LookAt(direction.transform.position);
       
        ps.transform.parent = transform.parent.transform;


		//#if UNITY_WEBGL
		// to fix the weired problem in webgl
		// OnParticleCollision does not work


		//Debug.Log("Start to Sweep");


//		RaycastHit hit;
//
//		if(Physics.Raycast(transform.position,(direction.transform.position-transform.position),out hit,ElementMask)){
//
//			Debug.Log("ray hit"+hit.collider.gameObject.name);
//			GameObject hitObject = hit.collider.gameObject;
//
//
//			ElementControl hitObjectElement = hitObject.GetComponent<ElementControl>();
//
//
//			if ((hitObjectElement != null)
//				&& (!(hitObjectElement.IsAMine))
//				&& (!hitObjectElement.IsSweepered)
//				&& (!hitObjectElement.IsFlagged))
//			{
//
//				// not a mine and not be sweepered
//
//				// sweeper the elements near
//				if (hitObjectElement.IsABlank)
//				{
//
//					hitObjectElement.ClickOnABlank();
//
//				} else //a number, with mines near
//				{
//					hitObjectElement.SweeperThisElement();
//				}
//
//			}
	//	}


		//#endif
    }


    int GetHowManyMinesNear(){ // to calculate how many mines near this element
        int ownNumber=0;

        foreach (GameObject direction in Directions)
        {


            Ray ray = new Ray(transform.position, direction.transform.position-transform.position);
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

    #region public

    public void SweeperThisElement(){
        Debug.Log(gameObject.name + "has " + _ownNumber + " mines near");
        //set the number texture


        GameObject go =Instantiate(DifferentNumbers [_ownNumber - 1], transform.position,
            transform.rotation*Quaternion.AngleAxis(90f,Vector3.left)) //rotation for the texture
            as GameObject;

        go.transform.parent = this.gameObject.transform.parent;
        Destroy(this.gameObject);

    }

    public void ClickOnABlank(){

        Debug.Log(gameObject.name + "is a blank element");


        GameObject go=Instantiate(BlankElement, transform.position,
            this.transform.rotation) as GameObject;

        go.transform.parent = this.gameObject.transform.parent;
        Destroy(this.gameObject);


        //to sweeper the near
        for (int i = 0; i < Directions.Count; i++)
        {


            SweeperToSendParticle(Directions [i]);


        }

    }



    public void OnPointerClick(PointerEventData eventData){ // add a if for whether it is a flag
       


        if (!((_rightClick) ||(_isFlagged) ))
        {

            PlayClickAudio();

            _isSweepered = true;



            Debug.Log("mouse pointer click on" + gameObject.name);

            if (_isAMine) //keep check mine firstly
            {

              

                //you falied, and game over
                Debug.Log(gameObject.name + " is a mine, you failed!");
                GameObject go = Instantiate(MineElement, transform.position,
                    transform.rotation) as GameObject;

                go.transform.parent = this.gameObject.transform.parent;
                Destroy(this.gameObject);

            } else if (_isABlank)
            {
				Debug.Log("click on as blank");
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

    #endregion
}
