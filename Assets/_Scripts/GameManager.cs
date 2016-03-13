using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour {

    //arrange the mines and other elements in game manager

    public static GameManager instance;

    public GameObject ParentOfElements;
    public GameObject ElementObject;


    public float DistanceOfTwoElements=2f;

    //please insure that mines quantity is lessthan element quantity
    public int ElementsQuantity=64;
    public int MinesQuantity=10;


    private List<Vector3> _elementPositions=new List<Vector3>(); 
    private List<int> _randomMinesPositionList = new List<int>();

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
       

        SetRandomMinesPosition();



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

    void SetRandomMinesPosition(){

        int i=0;

        while(i<MinesQuantity){
            int tmp = Random.Range(0, ElementsQuantity-1);

            if (!_randomMinesPositionList.Contains(tmp))
            {

                _randomMinesPositionList.Add(tmp);
                i++;



            }



        }



    }

    void SetElementsPosition(){
       


        //set position


        for (int i = 0; i < _elementPositions.Count; i++)
        {

            GameObject element = Instantiate
                (ElementObject, _elementPositions[i],Quaternion.identity)as GameObject;
            element.transform.parent = ParentOfElements.transform;


            if (_randomMinesPositionList.Contains(i))
            {

                element.GetComponent<ElementControl>().IsAMine = true;

            }

        }




    }


    void PrintMatrix(){

        foreach (Vector3 pos in _elementPositions)
        {

            Debug.Log(pos);


        }


    }

    void PrintRandomMinesList(){

        foreach (int i in _randomMinesPositionList)
        {

            Debug.Log(i);

        }


    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
