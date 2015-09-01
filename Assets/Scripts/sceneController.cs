using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class sceneController : MonoBehaviour {
	public GameObject parentObeject;
	public float speed=3f; //move speed
	public float rotateSpeed=3f; //rotate speed
	
	
	//fild of view
	public float minFov=15f;
	public float maxFov=90f;
	public float sensitivityFox=10f;


	private float fov;
	
	
	private Dictionary<KeyCode,Vector3> directions=new Dictionary<KeyCode,Vector3 >(){
		{KeyCode.W,Vector3.up},
		{KeyCode.S,Vector3.down},
		{KeyCode.A,Vector3.left},
		{KeyCode.D,Vector3.right}
		//{KeyCode.Q,Vector3.up},
		//{KeyCode.E,Vector3.down}
	};

	// Use this for initialization
	void Start () {
		fov=Camera.main.fieldOfView; //use fov to conrol camera field of view
	}
	
	// Update is called once per frame
	void Update () {
		//right button rotate
		if (Input.GetMouseButton(1)){
			
			
			
			parentObeject.transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"),-Input.GetAxis("Mouse X"),0).normalized
			                               *rotateSpeed,Space.Self);
			
			//camera.transform.position=parentObeject.transform.position+distance;
		}
		
		
		//use mouse wheel to control dield view
		fov+=Input.GetAxis("Mouse ScrollWheel")*sensitivityFox*-1;
		fov=Mathf.Clamp(fov,minFov,maxFov);
		Camera.main.fieldOfView=fov;

        //use mouse wheal to control the distance between cubes  (failed now)
//        fov+=Input.GetAxis("Mouse ScrollWheel")*sensitivityFox*-1;
//        fov=Mathf.Clamp(fov,1.2f,10f);
//        gameController.gc.distance=(int)fov;
		
		// move by mouse wheel
		if(Input.GetMouseButton(2)){
			
			parentObeject.transform.Translate(new Vector3(Input.GetAxis("Mouse X"),-Input.GetAxis("Mouse Y"),0).normalized
			                                  *speed*Time.deltaTime,Space.Self);
		}
		
		
		// move by keyboard
		foreach( KeyCode key in directions.Keys){
			
			if (Input.GetKey(key)){
				
				Debug.Log(key);
				
				parentObeject.transform.Translate(directions[key]*speed*Time.deltaTime,Space.World);
				
			}
			
		}


		//press r transform to default position
		if(Input.GetKeyDown(KeyCode.R)){
			parentObeject.transform.rotation=new Quaternion(0,0,0,0);
			parentObeject.transform.position=new Vector3(0,0,0);
		}
	}
}
