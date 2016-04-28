using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AudioRemember : MonoBehaviour {

	public Slider MusicSlider;
	public Slider EffectSlider;

	// Use this for initialization
	void Start () {
		MusicSlider.value=PlayerPrefs.GetFloat("MusicVolume",MusicSlider.maxValue);
		EffectSlider.value=PlayerPrefs.GetFloat("EffectVolume",EffectSlider.maxValue);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void SetMusicValue(float f){


		PlayerPrefs.SetFloat("MusicVolume",f);

	}


	public void SetEffectValue(float f){


		PlayerPrefs.SetFloat("EffectVolume",f);

	}


}
