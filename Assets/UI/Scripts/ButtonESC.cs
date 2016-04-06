using UnityEngine;
using System.Collections;


public class ButtonESC : MonoBehaviour {
	public static System.Action PressESC;

	public void PressESCButton(){

		PressESC();
		Debug.Log("Press ESC");

	}
}
