using UnityEngine;
using System.Collections;

public class LeanRotate : MonoBehaviour {
	
	public float SwipeThreshold=50f;
	public float SensitivityRotation=45f;
	public float RotateSpeed=3f; //rotate speed

	private Quaternion newQuaternion;

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


		newQuaternion=transform.rotation;

	}

	void Update(){

		transform.rotation=Quaternion.Lerp
			(transform.rotation,newQuaternion,
				RotateSpeed*Time.deltaTime);


	}

	public void OnFingerSwipe(Lean.LeanFinger finger){

		// Store the swipe delta in a temp variable
		var swipe = finger.SwipeDelta;

		if(swipe.magnitude>SwipeThreshold){

			if (swipe.x < -Mathf.Abs(swipe.y))
			{
				Debug.Log( "You swiped left!");
				//_thisY+=SensitivityRotation;
				newQuaternion=Quaternion.AngleAxis(90,Vector3.up)*transform.rotation;

			}

			if (swipe.x > Mathf.Abs(swipe.y))
			{
				
				newQuaternion=Quaternion.AngleAxis(-90,Vector3.up)*transform.rotation;


			}

			if (swipe.y < -Mathf.Abs(swipe.x))
			{
				//_parentX+=SensitivityRotation;
				newQuaternion=Quaternion.AngleAxis(90,Vector3.right)*transform.rotation;
			}

			if (swipe.y > Mathf.Abs(swipe.x))
			{
				
				newQuaternion=Quaternion.AngleAxis(-90,Vector3.right)*transform.rotation;
			}
		}

	}
}
