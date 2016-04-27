using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneControl : MonoBehaviour {

	//public static SceneControl instance=null;
	public CameraFade FadeCtrl;
	public string NextScene;
	[Tooltip("Same As Fade Time")]
	public float LoadingTime;

	private AsyncOperation loadAsync;

//	void Awake(){
//		if(instance!=null){
//
//			Destroy(this);
//		}else{
//			instance=this;
//		}
//
//
//	}


	IEnumerator StartLoading(float time	){
		FadeCtrl.FadeOut(time,true);
		yield return new WaitForSeconds(time);

		loadAsync.allowSceneActivation=true;

	}


	private IEnumerator LoadAsync(string nextScene){


		loadAsync= SceneManager.LoadSceneAsync(nextScene);
		loadAsync.allowSceneActivation=false;

		#if UNITY_EDITOR //just for debug
		while(!loadAsync.isDone){

			yield return loadAsync.isDone;
			Debug.Log("loading progress"+ loadAsync.progress);

		}

		Debug.Log("load done");
		#endif

	}


	public void LoadScene(){

		LoadScene(NextScene);
	}

	public void LoadScene(string nextScene){

		LoadScene(nextScene,LoadingTime);
	}


	public void LoadScene(string nextScene, float waitingTime){

		StartCoroutine(StartLoading(waitingTime));
		StartCoroutine(LoadAsync(nextScene));

	}

	public void Restart(){

		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

	}

	public void Quit()
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
			LoadScene("0Menu");
			//Destroy(this.gameObject);//because ui is do not destroy auto
		}
	}



}
