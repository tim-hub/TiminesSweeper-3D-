using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Resume : MonoBehaviour {

	public StartOptions StartOptionComponent;

	public void ResumeGame(){

		int scene=PlayerPrefs.GetInt("LastScene",StartOptionComponent.sceneToStart);// set the int value when you win in a level

		StartOptionComponent.sceneToStart=scene;

		StartOptionComponent.StartButtonClicked();
	}
}
