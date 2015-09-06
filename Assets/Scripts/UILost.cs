using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UILost : MonoBehaviour {
    public Text score;
    public Text yourBestScore;
	// Use this for initialization
	void Start () {
        score.text=PlayerPrefs.GetFloat("score").ToString("0.0");
        yourBestScore.text=PlayerPrefs.GetFloat("score").ToString("0.0");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
