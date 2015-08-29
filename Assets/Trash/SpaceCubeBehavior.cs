using UnityEngine;
using System.Collections;

public class SpaceCubeBehavior : MonoBehaviour {

	public Shader shader;
	public Texture texture;
	public Color color;
	Renderer rend;



	//when click render the white material
	//thn destroy the self(in game controller and got delay)


	// Use this for initialization
	void Start () {
	
		rend=GetComponent<Renderer>();



	}
	
	// Update is called once per frame
	void Update () {




	}


	//this method belongs to mono behavior
	void OnMouseDown(){


		if(Input.GetMouseButtonDown(0)){
			//Destroy(this.gameObject);

			//rend.material=new Material(shader);   //this to change shader

			rend.material.mainTexture=texture;  //this to change texture
			//rend.material.color=color;  //this to change color

			Debug.Log ("new material");
		}

		if(Input.GetMouseButtonDown(1)){
		
		}

	}
}
