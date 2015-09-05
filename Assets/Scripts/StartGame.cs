using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StartGame : MonoBehaviour {
	public void ClickToStart(){
		Application.LoadLevel("main");

	}

	public void ClickToQuit(){
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#else
		Application.Quit();
		#endif
	}






	// Use this for initialization
//	void OnGUI () {
//
//        if(GUI.Button(new Rect(400,400,100,66),"Start")){
//            Application.LoadLevel("main");
//            //it seems that scripts between different scenes cannot connect
//
//        }
//
//        if(GUI.Button(new Rect(400,470,100,66),"Exit")){
//            Application.Quit();
//            
//        }
//	}
	
}
