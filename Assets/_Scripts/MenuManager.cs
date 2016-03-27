using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public string DefaultNextScene;

    public GameObject MenuCanvas;
    public GameObject CreditsCanvas;

	// Use this for initialization
	void Start () {
        CreditsCanvas.SetActive(false);
        MenuCanvas.SetActive(true);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 


    public void LoadNextScene(string sceneName){

        SceneManager.LoadScene(sceneName);

    }

    public void LoadNextScene(){

        SceneManager.LoadScene(DefaultNextScene);

    }

    public void QuitGame(){


        Application.Quit();
    }


    public void ShowCreditsCanvas(){

        MenuCanvas.SetActive(false);
        CreditsCanvas.SetActive(true);
    }

}
