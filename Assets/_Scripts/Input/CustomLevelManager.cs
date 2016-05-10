using UnityEngine;
using System.Collections;

public class CustomLevelManager : MonoBehaviour {
	void Awake(){


		Time.timeScale=0f;
	}

	public void StartGame(){


		Time.timeScale=1f;
	}
}
