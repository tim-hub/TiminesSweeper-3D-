using UnityEngine;
using System.Collections;

public class TestAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<AudioSource>().Play();
	}
}
