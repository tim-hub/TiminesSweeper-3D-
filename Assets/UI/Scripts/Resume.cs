using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class Resume : MonoBehaviour {

	public SceneControl SceneCtrl;

	public void ResumeGame(){




		string sceneName=PlayerPrefs.GetString("LastScene", SceneCtrl.NextScene);


		SceneCtrl.LoadScene(sceneName);
	}
}
