using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// Sync sound value from option panel to pause panle.
/// </summary>
public class SyncSoundValue : MonoBehaviour {



	public void SetSliderValue(Slider slider){


		GetComponent<Slider>().value=slider.value;

	}
}
