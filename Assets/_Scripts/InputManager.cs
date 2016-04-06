using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameObject ParentObject;

    public float Speed=3f; //move speed
    public float RotateSpeed=3f; //rotate speed
	public float SwipeThreshold=50f;

    //fild of view
    public float MinFov=15f;
    public float MaxFov=90f;
    public float SensitivityFox=10f;


    private float fov;

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

	// Use this for initialization
	void Start () {
        fov = Camera.main.fieldOfView;
	}
	


	void LateUpdate()
	{
		// Does the main camera exist?
		if (Camera.main != null)
		{
			#if UNITY_ANDROID
			// Make sure the pinch scale is valid
			if (Lean.LeanTouch.PinchScale > 0.0f)
			{
				// Scale the FOV based on the pinch scale
				Camera.main.fieldOfView /= Lean.LeanTouch.PinchScale/1f;

				// Make sure the new FOV is within our min/max
				Camera.main.fieldOfView = Mathf.Clamp(Camera.main.fieldOfView, MinFov, MaxFov);
			}

			#else
			//use mouse wheel to control dield view
			fov+=Input.GetAxis("Mouse ScrollWheel")*SensitivityFox*-1;
			fov=Mathf.Clamp(fov,MinFov,MaxFov);
			Camera.main.fieldOfView=fov;
			#endif
		}






//		#if UNITY_ANDROID
//
//
//
//
//
//		#else
//
//		if (Input.GetMouseButton(1)){
//
//			Debug.Log("Right Mouse Button Rotate");
//
//
//			ParentObject.transform.RotateAround(Vector3.zero,new Vector3(-Input.GetAxis("Mouse Y"),
//				-Input.GetAxis("Mouse X"),0f),
//				RotateSpeed);
//
//
//		}
//		#endif


	}

	void FixedUpdate(){


		float y = Mathf.Clamp(transform.rotation.y,-10,60);
		transform.rotation=Quaternion.Euler(new Vector3(transform.rotation.x,y,transform.rotation.z));

	}


	public void OnFingerSwipe(Lean.LeanFinger finger){

		// Store the swipe delta in a temp variable
		var swipe = finger.SwipeDelta;

		if(swipe.magnitude>SwipeThreshold){

			if (swipe.x < -Mathf.Abs(swipe.y))
			{
				Debug.Log( "You swiped left!");

			}

			if (swipe.x > Mathf.Abs(swipe.y))
			{
				
			}

			if (swipe.y < -Mathf.Abs(swipe.x))
			{
				
			}

			if (swipe.y > Mathf.Abs(swipe.x))
			{
				
			}
		}

	}
}
