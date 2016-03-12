using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //arrange the mines and other elements in game manager

    public static GameManager instance;

    public GameObject ParentOfElements;
    public GameObject ElementObject;


    public float DistanceOfTwoElements=2f;
    public int ElementsQuantity=64;


    private List<Vector3> _elementPositions=new List<Vector3>(); 

    void Awake(){

        if(instance==null){ //do we have a game manager yet, if no
            instance=this;  //than this is the game manager
        }else if(instance!=this){ //is there is
            Destroy(gameObject); //than destroy this
        }



    }


	// Use this for initialization
	void Start () {
        SetPositionMatrix();

        SetElementsPosition();
	
	}

    void SetPositionMatrix(){
        int cows =(int) Mathf.Pow(ElementsQuantity, (1f / 3));

        for (int i = 0; i < cows; i++)
        {

            for (int j = 0; j < cows; j++)
            {
                for (int k = 0; k <= cows; k++)
                {

                    _elementPositions.Add
                    (new Vector3(i * DistanceOfTwoElements,
                        j * DistanceOfTwoElements,
                        k * DistanceOfTwoElements));

                }

            }


        }
    }

    void SetElementsPosition(){
        foreach (Vector3 pos in _elementPositions)
        {

            GameObject element = Instantiate(ElementObject, pos,Quaternion.identity)as GameObject;
            element.transform.parent = ParentOfElements.transform;


        }



    }


    void PrintMatrix(){

        foreach (Vector3 pos in _elementPositions)
        {

            Debug.Log(pos);


        }


    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
