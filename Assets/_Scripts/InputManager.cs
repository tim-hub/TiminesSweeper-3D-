using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour {

    public GameObject ParentObject;

    public float Speed=3f; //move speed
    public float RotateSpeed=3f; //rotate speed


    //fild of view
    public float MinFov=15f;
    public float MaxFov=90f;
    public float SensitivityFox=10f;


    private float fov;


	// Use this for initialization
	void Start () {
        fov = Camera.main.fieldOfView;
	}
	
	// Update is called once per frame
	void Update () {
	
        #if UNITY_ANDROID

   



        #else

        //use mouse wheel to control dield view
        fov+=Input.GetAxis("Mouse ScrollWheel")*SensitivityFox*-1;
        fov=Mathf.Clamp(fov,MinFov,MaxFov);
        Camera.main.fieldOfView=fov;

        if (Input.GetMouseButton(1)){
           
            Debug.Log("Right Mouse Button Rotate");

           
            ParentObject.transform.RotateAround(Vector3.zero,new Vector3(-Input.GetAxis("Mouse Y"),
                -Input.GetAxis("Mouse X"),0f),
                RotateSpeed);


        }

        #endif


	}
}
