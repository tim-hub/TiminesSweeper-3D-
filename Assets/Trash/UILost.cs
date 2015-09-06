using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILost : MonoBehaviour {
    public Text score;
    public Text yourBestScore;
	// Use this for initialization
	void Start () {
        score.text="Your Last Score: "+PlayerPrefs.GetFloat("lastScore",0).ToString("0.0");
        yourBestScore.text="Your Highest Score: "+PlayerPrefs.GetFloat("highestScore",0).ToString("0.0");
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetKeyDown(KeyCode.S)){
            PlayerPrefs.SetFloat("lastScore",0);
            PlayerPrefs.SetFloat("highestScore",0);
        }
	
	}
}
