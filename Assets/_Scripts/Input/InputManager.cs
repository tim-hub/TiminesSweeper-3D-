using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameObject ParentObject;
	public LayerMask ElementMask;
	public float LongPressTime=.5f;

    public float Speed=3f; //move speed
    


	//reotation
	public float SwipeThreshold=50f;
	public float HeldThreshold=.4f;
	public float SensitivityRotation=45f;
	public float RotateSpeed=3f; //rotate speed

	private Quaternion newQuaternion;

    //fild of view
    public float MinFov=15f;
    public float MaxFov=90f;
    public float SensitivityFox=10f;


    private float fov;

	private float _timePressed=0f;
	private float _timeLastPress=0f;



	void OnEnable()
	{
		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;

		Lean.LeanTouch.OnFingerTap+= OnFingerTap;


		Lean.LeanTouch.OnFingerHeldDown += OnFingerHeldDown;


		Lean.LeanTouch.OnPinch +=OnPinch;
	}

	void OnDisable()
	{
		// Unhook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
		Lean.LeanTouch.OnFingerTap-= OnFingerTap;


		Lean.LeanTouch.OnFingerHeldDown -= OnFingerHeldDown;


		Lean.LeanTouch.OnPinch -=OnPinch;
	}

	// Use this for initialization
	void Start () {
		Lean.LeanTouch.Instance.HeldThreshold=HeldThreshold;

        fov = Camera.main.fieldOfView;
		Debug.Log("lean rotate");

		newQuaternion=transform.rotation;
	}
	
	void Update(){
		

		if(Input.GetMouseButtonDown(1)){
			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition),out hit,
				Vector3.Distance(Camera.main.transform.position,transform.position),ElementMask)){

				Debug.Log("long press to hit "+hit.collider.name);

				hit.collider.gameObject.GetComponent<ElementControl>().FlagThisElement();

			}


		}

		//basio keyboard input
		if(Input.GetKeyDown(KeyCode.A)){

			RotateLeft();
		}
		if(Input.GetKeyDown(KeyCode.D)){

			RotateRight();
		}
		if(Input.GetKeyDown(KeyCode.W)){

			RotateUp();
		}
		if(Input.GetKeyDown(KeyCode.S)){

			RotateDown();
		}


	}

	void LateUpdate() 
		
	{
		//scale
		// Does the main camera exist?
		if (Camera.main != null)
		{

			//use mouse wheel to control dield view
			fov+=Input.GetAxis("Mouse ScrollWheel")*SensitivityFox*-1;

			fov=Mathf.Clamp(fov,MinFov,MaxFov);
			Camera.main.fieldOfView=fov;
		}


		//rotate

		transform.rotation=Quaternion.Lerp
			(transform.rotation,newQuaternion,
				RotateSpeed*Time.deltaTime);
		

	}
	#region to the element
	void FlagItOn(Vector3 position){


		if(Input.touchCount==1){ // to avoid flag on scale

			RaycastHit hit;

			if(Physics.Raycast(Camera.main.ScreenPointToRay(position),out hit,
				Vector3.Distance(Camera.main.transform.position,transform.position),ElementMask)){

				Debug.Log("long press to hit "+hit.collider.name);

				hit.collider.gameObject.GetComponent<ElementControl>().FlagThisElement();

			}
		}
	}

	void SweeperItOn(Vector3 position){

		RaycastHit hit;

		if(Physics.Raycast(Camera.main.ScreenPointToRay(position),out hit,
			Vector3.Distance(Camera.main.transform.position,transform.position),ElementMask)){

			Debug.Log("long press to hit "+hit.collider.name);

			hit.collider.gameObject.GetComponent<ElementControl>().SweeperThisOne();

		}

	}

	#endregion
	#region rotate

	void RotateLeft(){
		newQuaternion=Quaternion.AngleAxis(90,Vector3.up)*transform.rotation;

	}
	void RotateRight(){

		newQuaternion=Quaternion.AngleAxis(-90,Vector3.up)*transform.rotation;
	}

	void RotateDown(){

		newQuaternion=Quaternion.AngleAxis(90,Vector3.right)*transform.rotation;
	}
	void RotateUp(){

		newQuaternion=Quaternion.AngleAxis(-90,Vector3.right)*transform.rotation;
	}
	#endregion

	#region On Finger


	public void OnFingerSwipe(Lean.LeanFinger finger){

		// Store the swipe delta in a temp variable
		var swipe = finger.SwipeDelta;

		if(swipe.magnitude>SwipeThreshold  ){

			if (swipe.x < -Mathf.Abs(swipe.y))
			{
				

				Debug.Log( "You swiped left!");

				RotateLeft();



			}

			if (swipe.x > Mathf.Abs(swipe.y))
			{

				RotateRight();

			}

			if (swipe.y < -Mathf.Abs(swipe.x))
			{
				//_parentX+=SensitivityRotation;
				RotateDown();
			}

			if (swipe.y > Mathf.Abs(swipe.x))
			{
				RotateUp();

			}
		}

	}

	public void OnFingerTap(Lean.LeanFinger finger){

		SweeperItOn(finger.ScreenPosition);

	}

	public void OnFingerHeldDown(Lean.LeanFinger finger){


		FlagItOn(finger.ScreenPosition);
	}




	public void OnPinch(float scale){

		if (Lean.LeanTouch.PinchScale > 0.0f)
		{
			// Scale the FOV based on the pinch scale
			Camera.main.fieldOfView /= Lean.LeanTouch.PinchScale/1f;

			// Make sure the new FOV is within our min/max
			fov = Mathf.Clamp(Camera.main.fieldOfView, MinFov, MaxFov);
		}

		// set fov
		fov=Mathf.Clamp(fov,MinFov,MaxFov);
		Camera.main.fieldOfView=fov;

	}
	#endregion
}
