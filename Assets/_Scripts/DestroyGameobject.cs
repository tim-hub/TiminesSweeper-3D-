using UnityEngine;
using System.Collections;

public class DestroyGameobject : MonoBehaviour {
	public float Duration;

	// Use this for initialization
	void Start () {
		Destroy(this.gameObject,Duration);
	}
	

}
