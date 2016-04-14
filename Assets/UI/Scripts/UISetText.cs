using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class UISetText : MonoBehaviour {
	


	public void SetText(int i){

		GetComponent<Text>().text=i.ToString();
	}

	public void SetText(){

		GetComponent<Text>().text=0.ToString();

	}

	public void SetText(string str){

		GetComponent<Text>().text=str;

	}
	public void SetText(float f){

		GetComponent<Text>().text=f.ToString();

	}
}
