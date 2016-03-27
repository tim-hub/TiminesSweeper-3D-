using UnityEngine;
using System.Collections;

public class CreitsManager : MonoBehaviour {

    public GameObject MenuCanvas;
    public GameObject CreditsCanvas;
	// Use this for initialization
    public void BackMenuCanvas(){

        MenuCanvas.SetActive(true);
        CreditsCanvas.SetActive(false);
    }
}
