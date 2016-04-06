using UnityEngine;
using System.Collections;

public class LeanRotate : MonoBehaviour {
	
	public float SwipeThreshold=50f;
	public float SensitivityRotation=45f;
	public float RotateSpeed=3f; //rotate speed


	private float _y;
	private float _x;
	void OnEnable()
	{
		// Hook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe += OnFingerSwipe;
	}

	void OnDisable()
	{
		// Unhook into the OnSwipe event
		Lean.LeanTouch.OnFingerSwipe -= OnFingerSwipe;
	}

	void Start(){

		_x=0;
		_y=330;
	}

	void Update(){
//		float y = Mathf.Clamp(transform.rotation.y,-10,60);
//		transform.rotation=Quaternion.Euler(new Vector3(transform.rotation.x,y,transform.rotation.z));
		if(Input.touchCount>=1){
			float deltaX=(Input.touches[0].deltaPosition.x);


			if(Mathf.Abs(deltaX)>SwipeThreshold){


				if(deltaX<0){

					_y+=SensitivityRotation;

				}else if(deltaX>0){

					_y-=SensitivityRotation;
				}


			}

		}


		transform.rotation=Quaternion.Lerp
			(transform.rotation,Quaternion.Euler(transform.rotation.x,_y,transform.rotation.z),
				RotateSpeed*Time.deltaTime);
		


	}
	// Update is called once per frame
	void LateUpdate () {

		// 2 d rotate in touch screen

		// Get the center point of all touches
		var center = Lean.LeanTouch.GetCenterOfFingers();

		// This will rotate the current transform based on a multi finger twist gesture
		Lean.LeanTouch.RotateObjectRelative(transform, Lean.LeanTouch.TwistDegrees, center);

		// This will scale the current transform based on a multi finger pinch gesture
		//Lean.LeanTouch.ScaleObjectRelative(transform, Lean.LeanTouch.PinchScale, center);
	}



	public void OnFingerSwipe(Lean.LeanFinger finger){

		// Store the swipe delta in a temp variable
		var swipe = finger.SwipeDelta;

		if(swipe.magnitude>SwipeThreshold){

//			if (swipe.x < -Mathf.Abs(swipe.y))
//			{
//				Debug.Log( "You swiped left!");
//				_y+=SensitivityRotation;
//
//			}
//
//			if (swipe.x > Mathf.Abs(swipe.y))
//			{
//				_y-=SensitivityRotation;
//			}
//
//			if (swipe.y < -Mathf.Abs(swipe.x))
//			{
//				_x+=SensitivityRotation;
//			}
//
//			if (swipe.y > Mathf.Abs(swipe.x))
//			{
//				_x-=SensitivityRotation;
//			}
		}

	}
}
