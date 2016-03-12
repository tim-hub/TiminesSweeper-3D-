using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    //arrange the mines and other elements in game manager

    public static GameManager instance;




    void Awake(){

        if(instance==null){ //do we have a game manager yet, if no
            instance=this;  //than this is the game manager
        }else if(instance!=this){ //is there is
            Destroy(gameObject); //than destroy this
        }



    }


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
