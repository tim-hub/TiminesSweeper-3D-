using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Resume : MonoBehaviour {

	public StartOptions StartOptionComponent;

	public void ResumeGame(){

		int scene=PlayerPrefs.GetInt("lastScene",StartOptionComponent.sceneToStart);

		StartOptionComponent.sceneToStart=scene;

		StartOptionComponent.StartButtonClicked();
	}
}
