using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    //arrange the mines and other elements in game manager

    public static GameManager instance;
    public string NextLevel;

	public UISetText MinesText;
	public UISetText FlagsText;
	public UISetText TimingText;
	public UISetText HighText;

    public GameObject GameOverUI;
    public GameObject GameWinUI;
    public GameObject ParentOfElements;
    public GameObject ElementObject;

    public bool MouseInputing=true;


    public float DistanceOfTwoElements=2f;

    //please insure that mines quantity is lessthan element quantity
    public int ElementsQuantity=64;
    public int MinesQuantity=10;


    private int flagsCount = 0;
    private int rightFlagsCount=0;

    private List<Vector3> _elementPositions=new List<Vector3>(); 
    private List<int> _randomMinesPositionList = new List<int>();

	private float _runningTime=0f;
	private bool _pauseCounting=false;

    void Awake(){

        if(instance==null){ //do we have a game manager yet, if no
            instance=this;  //than this is the game manager
        }else if(instance!=this){ //is there is
            Destroy(gameObject); //than destroy this
        }


        Time.timeScale = 1f;
    }


	// Use this for initialization
	void Start () {

        MinesText.SetText(MinesQuantity);
        flagsCount = 0;
        FlagsText.SetText(flagsCount);
		HighText.SetText(PlayerPrefs.GetString("HighScoreIn"+SceneManager.GetActiveScene().buildIndex,0f.ToString()));

		_runningTime=0f;
		TimingText.SetText(( _runningTime));


        GameOverUI.SetActive(false);
        GameWinUI.SetActive(false);


		if(SceneManager.GetActiveScene().name=="LevelCustom"){


			ElementsQuantity=PlayerPrefs.GetInt("ElementsQuantity",125);
			MinesQuantity=PlayerPrefs.GetInt("MinesQuantity",32);
		}



        SetPositionMatrix();
       

        SetRandomMinesPosition();



        SetElementsPosition();
	
	}

	void Update(){

		if(!_pauseCounting){ 

			_runningTime+=Time.deltaTime;
			TimingText.SetText(_runningTime.ToString("F1"));
		}
	}



    void SetPositionMatrix(){
        int numberOfColumns =(int) Mathf.Pow(ElementsQuantity, (1f / 3));

        for (int i = 0; i < numberOfColumns; i++)
        {

            for (int j = 0; j < numberOfColumns; j++)
            {
                for (int k = 0; k < numberOfColumns; k++)
                {

                    _elementPositions.Add
                    (GetStartPosition()+new Vector3(i * DistanceOfTwoElements,
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

	
    Vector3 GetStartPosition(){

        int numberOfColumns =(int) Mathf.Pow(ElementsQuantity, (1f / 3));
        Vector3 startPos=Vector2.zero;

        float x = -(DistanceOfTwoElements)
            * (numberOfColumns-1) / 2f;

        startPos = new Vector3(x, x, x);

        return startPos;

    }

	void CheckWin(){

		if (flagsCount == MinesQuantity)
		{


			Debug.Log("Win");

			PlayerPrefs.SetString("LastScene",SceneManager.GetActiveScene().name);

			_pauseCounting=true;

			Invoke("GameWin",1f);


			float lastHighScore=float.Parse(PlayerPrefs.GetString("HighScoreIn"+SceneManager.GetActiveScene().buildIndex,0.ToString("F1")));
			if(lastHighScore==0 ){

				PlayerPrefs.SetString("HighScoreIn"+SceneManager.GetActiveScene().buildIndex,_runningTime.ToString("F1"));

			}else if(lastHighScore>_runningTime){

				PlayerPrefs.SetString("HighScoreIn"+SceneManager.GetActiveScene().buildIndex,_runningTime.ToString("F1"));
			}
		}
	}


	void GameWin(){


		GameWinUI.SetActive(true);
		Time.timeScale=0f;
	}

	void GameOverUIShow(){

		GameOverUI.SetActive(true);
		Time.timeScale=0f;
	}

    public void FlagOne(){

        flagsCount++;
        FlagsText.SetText(flagsCount);



    }

    public void FlagRightOne(){
        rightFlagsCount++;
        CheckWin();
    }

    public void UnFlagOne(){

        flagsCount--;
        FlagsText.SetText(flagsCount);



    }






    public void GameOver(){

        //show ui
		Invoke("GameOverUIShow",1f);
        
		_pauseCounting=true;

    }





}
