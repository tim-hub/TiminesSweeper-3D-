using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameObject ParentObject;

    public float Speed=3f; //move speed
    


    //fild of view
    public float MinFov=15f;
    public float MaxFov=90f;
    public float SensitivityFox=10f;


    private float fov;



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





}
