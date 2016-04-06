using UnityEngine;
using System.Collections;

public class LeanRotate : MonoBehaviour {

	
	// Update is called once per frame
	void LateUpdate () {
		// Get the center point of all touches
		var center = Lean.LeanTouch.GetCenterOfFingers();

		// This will rotate the current transform based on a multi finger twist gesture
		Lean.LeanTouch.RotateObjectRelative(transform, Lean.LeanTouch.TwistDegrees, center);

		// This will scale the current transform based on a multi finger pinch gesture
		//Lean.LeanTouch.ScaleObjectRelative(transform, Lean.LeanTouch.PinchScale, center);
	}
}
