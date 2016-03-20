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

            Debug.Log("Right Mouse Button");

            ParentObject.transform.Rotate(new Vector3(0f,-Input.GetAxis("Mouse X"),0f).normalized
                *RotateSpeed,Space.Self);
            Debug.Log(Input.GetAxis("Mouse X"));
            //camera.transform.position=parentObeject.transform.position+distance;
        }
        #endif


	}
}
