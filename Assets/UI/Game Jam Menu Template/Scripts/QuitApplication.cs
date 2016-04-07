using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class QuitApplication : MonoBehaviour {

	void Quit()
	{
		//If we are running in a standalone build of the game
	#if UNITY_STANDALONE
		//Quit the application
		Application.Quit();
	#endif

		//If we are running in the editor
	#if UNITY_EDITOR
		//Stop playing the scene
		UnityEditor.EditorApplication.isPlaying = false;
	#endif



	}

	public void Exit(){

		if(SceneManager.GetActiveScene().buildIndex==0){
			Quit();

		}else{

			SceneManager.LoadScene(0);
			Destroy(this.gameObject);//because ui is do not destroy auto
		}
	}
}
