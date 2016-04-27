using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class QuitApplication : MonoBehaviour {


	public SceneControl SceneCtrl;

	void Quit()
	{
		//If we are running in a standalone build of the game

		//Quit the application
		Application.Quit();
	

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

			//SceneManager.LoadScene(0);
			SceneCtrl.LoadScene("0Menu");
			//Destroy(this.gameObject);//because ui is do not destroy auto
		}
	}
}
