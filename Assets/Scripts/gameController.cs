using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	#region publicVars
    public static gameController gc;
	//link the objects
	public GameObject defaultCube;
	public GameObject parentObeject;
    public GameObject youWin;
    public GameObject youLost;
    public List<GameObject> cubesList;
	public int numOfCubes=27; //the sum of cubes
	public int numOfMines=5; //sum of mines
    public int distance=2; //distance between cube and cube

    
    public int[] minesIndex;
    public int[] numCubesIndex;
    public int[] spaceCubesIndex;

    public bool startGame;
    public bool stopGame;
    

	public Text scoreText;
    public Text minesText;
    public int minesTextNum;
    public Text spaceCubesText;
    public int spacesTextNum;
    public Text cubesText;
    public int cubesTextNum;
    public Canvas canvasESCMenu;

    public Text highestScoreText;

    public float highestScore;
    public int numOfMinesMined=0;
	#endregion 


	#region privateVars

	private Vector3[] cubesArray;
	private Vector3[] minesArray; //use int as a symbol for mines

	private int rows;

    bool escapeOnce=false;
    bool minesClean=false;// whether mines are clean

	float runningTime=0f;


    float basicScore=0f;

	#endregion


	#region UnityMethods



	// Use this for initialization
	void Start () {
        gc=this;
        //when start game, did not show the esc menu
        canvasESCMenu.enabled=false;

        //us this to set the default highest score
        if(PlayerPrefs.GetFloat("highestScore")!=null){
            highestScore=PlayerPrefs.GetFloat("highestScore");
        }else{
            Debug.Log("First time to store a score");
            highestScore=0;
        }


        youWin.GetComponent<MeshRenderer>().enabled=false;
        youLost.GetComponent<MeshRenderer>().enabled=false;


		rows=(int)(Mathf.Pow(numOfCubes,1f/3));  //use 1f/3 not 1/3
		cubesArray=CreateCubesArray(numOfCubes);


//		minesArray=new Vector3[]{new Vector3(-2f,-2f,-2f),new Vector3(0f,0f,0f),new Vector3(2,-2,-2),new Vector3(2,2,2),
//			new Vector3(2,0,2)}; //this is an original mines position

	


		//minesIndex=GetMinesIndex(minesArray);

        //minesIndex=new int[] { 0, 2, 1,3,4 };
        minesIndex=RandomMinesPosition(5);


		numCubesIndex=GetNumCubesIndex(minesIndex);

        spaceCubesIndex=GetSpaceCubes();

		CreateMap(cubesArray,minesIndex,numCubesIndex);

        minesTextNum=numOfMines;
        spacesTextNum=spaceCubesIndex.Length;
        cubesTextNum=numOfCubes;


	}




	
	// Update is called once per frame
	void Update () {
        //when stop the game
        if (stopGame){
            highestScore=runningTime;
            SetHighestScore(highestScore);
            
            youLost.GetComponent<MeshRenderer>().enabled=true;
            Invoke("Restart",3);
            //Time.timeScale=0.25f;
            //StartCoroutine(StopGame());

        }
        if (numOfMinesMined==numOfMines){
            minesClean=true;
        }

        if(minesClean){
            stopGame=true;
            youWin.GetComponent<MeshRenderer>().enabled=true;
            Invoke("Restart",3);
            //Time.timeScale=0.25f;
            //StartCoroutine(StopGame());
        }


        //ui text show
        minesText.text="MINES: "+minesTextNum;
        spaceCubesText.text="SPACE CUBES: "+spacesTextNum;
        cubesText.text="ALL CUBES: "+cubesTextNum;
        highestScoreText.text="BEST: "+highestScore.ToString("0.0");;


        if(!startGame ){
            Time.timeScale=0;
        }else if(!stopGame){
            Time.timeScale=1;
        }


		runningTime+= Time.deltaTime;
        scoreText.text="TIME: "+(runningTime).ToString("0.0");


        if(Input.GetMouseButtonDown(0)||Input.GetMouseButtonDown(1)){
            GetComponents<AudioSource>()[0].Play();
            //SetHighestScore(12);
        }

        if(!stopGame&&Input.GetKeyDown(KeyCode.Escape) ){
            Debug.Log("Press escape");
            if(escapeOnce==false){
                startGame=false;
                GetComponents<AudioSource>()[1].Pause();
                escapeOnce=true;
                canvasESCMenu.enabled=true;

            }else{
                GetComponents<AudioSource>()[1].Play();
                startGame=true;
                escapeOnce=false;
                canvasESCMenu.enabled=false;
            }
        }




	}

	#endregion

    #region startStop
    void Restart(){
        Application.LoadLevel(Application.loadedLevel);
    }



    #endregion


	#region UserMethods
    /// <summary>
    /// Creates the matrix
    /// </summary>
    /// <param name="cubesArray">Cubes array.</param>
    /// <param name="mines">Mines.</param>
    /// <param name="nums">Nums.</param>
	void CreateMap(Vector3[] cubesArray,int[] mines, int[] nums){
		GameObject go;
		//int numCount=0;
		for (int i=0;i<numOfCubes;i++){
			go=Instantiate(defaultCube,cubesArray[i],new Quaternion(0,0,0,0)) as GameObject;

			go.transform.parent=parentObeject.transform;
            cubesList.Add(go);
		}
		

	}
    /// <summary>
    /// Randoms the mines position.
    /// </summary>
    /// <returns>The mines position.</returns>
    /// <param name="sum">Sum.</param>
    int[] RandomMinesPosition(int sum){

        int[] arr = new int[sum];
        int j = 0;
        //表示键和值对的集合。 se hashable to store key and values
        Hashtable hashtable = new Hashtable();
        System.Random rm = new System.Random();
        for (int i = 0; hashtable.Count < sum; i++)
        {
            //返回一个小于所指定最大值的非负随机数  return a number < max but >0
            int nValue = rm.Next(numOfCubes);
            //containsValue(object value)   是否包含特定值
            if (!hashtable.ContainsValue(nValue) && nValue != 0)
            {
                //把键和值添加到hashtable add key value to hashtable
                hashtable.Add(nValue, nValue);
                //Debug.Log(i);
                arr[j] = nValue;
                
                j++;
            }
        }
        int temp;
        //最多做n-1趟排序  sort array
        for (int i = 0; i < arr.Length - 1; i++)
        {
            //对当前无序区间score[0......length-i-1]进行排序(j的范围很关键，这个范围是在逐步缩小的)
            for (j = 0; j < arr.Length - i - 1; j++)
            {
                if (arr[j] > arr[j + 1])
                {
                    temp = arr[j];
                    arr[j] = arr[j + 1];
                    arr[j + 1] = temp;
                }
            }
        }
        
        return arr;


    }

    /// <summary>
    /// Gets the space cubes index
    /// </summary>
    /// <returns>The space cubes.</returns>
    int[] GetSpaceCubes(){
        List<int> tmpList=new List<int>();
        for (int i=0;i<numOfCubes;i++){
            if(!(ArrayIndex(minesIndex,i))&&!(ArrayIndex(numCubesIndex,i))){
                tmpList.Add(i);
            }

        }


        int[] tmpArr=new int[tmpList.Count];
        for (int i=0;i<tmpList.Count;i++){
            tmpArr[i]=tmpList[i];
        }
        return tmpArr;
    }

    /// <summary>
    /// check the index whethe is in the array
    /// </summary>
    /// <returns><c>true</c>, if index was arrayed, <c>false</c> otherwise.</returns>
    /// <param name="arr">Arr.</param>
    /// <param name="index">Index.</param>
    bool ArrayIndex(int[] arr, int index){
        for(int i=0;i<arr.Length;i++){

            if(arr[i]==index){
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Creates the cubes array.
    /// </summary>
    /// <returns>The cubes array.</returns>
    /// <param name="num">Number.</param>
	private Vector3[] CreateCubesArray(int num){
		int i =0;

		Vector3[] cubesArray=new Vector3[num];
		for (int x=-(rows-1);x<rows;x+=distance){
			for (int y=-(rows-1);y<rows;y+=distance){
				for (int z=-(rows-1);z<rows;z+=distance){
					cubesArray[i]=new Vector3(x,y,z);
					i++;

				}
			}

		}

		return cubesArray;
	
	}


	/// <summary>
	/// Gets the index of the number cubes.
	/// </summary>
	/// <returns>The number cubes index.</returns>
	/// <param name="minesIndex">Mines index.</param>

	int[] GetNumCubesIndex(int[] minesIndex){
		List <int> tmp=new List<int>();
		for(int i=0;i<minesIndex.Length;i++){
			if(!(minesIndex[i]+rows*rows>numOfCubes )){
				
				tmp.Add(minesIndex[i]+rows*rows);
				
			}
			
			if(!(minesIndex[i]-rows*rows<0)){
				tmp.Add(minesIndex[i]-rows*rows);
			}
			
            if(!(minesIndex[i]+rows>numOfCubes)){
				
				tmp.Add(minesIndex[i]+rows);
				
			}
			
			if(!(minesIndex[i]-rows<0)){
				tmp.Add(minesIndex[i]-rows);
			}
			
            if(!(minesIndex[i]+1>numOfCubes )){
				
				tmp.Add(minesIndex[i]+1);
				
			}
			
			if(!(minesIndex[i]-1<0)){
				tmp.Add(minesIndex[i]-1);
			}
		}
		
		// remove the same index
		for(int i=0;i<tmp.Count;i++){
			for (int j=i+1;j<tmp.Count;j++){
				if (tmp[i]==tmp[j]){
					tmp.RemoveAt(j);
				}
			}
			
		}

		//change list to int[] array
		int[] tmpArray=new int[tmp.Count];
		for(int i=0;i<tmpArray.Length;i++){
			tmpArray[i]=tmp[i];
		}


		return tmpArray;

	
	}

//	Vector3 GetSpaceCubesIndex(Vector3[] cubesArray,Vector3 minesArray){
//		
//
//		
//	}
    /// <summary>
    /// Gets the index of the mines.
    /// </summary>
    /// <returns>The mines index.</returns>
    /// <param name="minesArray">Mines array.</param>
	int[] GetMinesIndex( Vector3[] minesArray){
		int[] minesIndex=new int[minesArray.Length];
		
		for (int i=0;i<minesIndex.Length;i++){
			
			minesIndex[i]=Vector3ToInt(minesArray[i]);
			
			
		}
		
		return minesIndex;
		
		
	}
	/// <summary>
    /// Vector3s to int.
    /// </summary>
    /// <returns>The to int.</returns>
    /// <param name="cubePosition">Cube position.</param>
	public int Vector3ToInt(Vector3 cubePosition){
		
		
		//return (int)(((cubePosition.x+rows+1)/2-1)*rows*rows+((cubePosition.y+rows+1)/2-1)*rows
		//	+(cubePosition.z+rows+1)/2-1);

        return (int)(((cubePosition.x+distance)/distance*rows*rows)+(cubePosition.y+distance)/distance*rows+
            (cubePosition.z+distance)/distance);
		
		//this is just like to count number, translate the vector3 to a number
		//for example 
		//27 cubes
		//(-2,-2,-2)  0*9+0*3+0*1=0
		//(-2,
		
	}
    /// <summary>
    /// Ints to vector.
    /// </summary>
    /// <returns>The to vector.</returns>
    /// <param name="index">Index.</param>
    public Vector3 IntToVector(int index){
    
        return new Vector3(index/(rows*rows)*distance-distance,(index%(rows*rows))/rows*distance-distance,
                           (index%(rows*rows))%rows*distance-distance);
    }
    /// <summary>
    /// Shows the array. this is a test methond
    /// </summary>
    /// <param name="array">Array.</param>
    void ShowArray(int[] array){
        foreach(int a in array){
            Debug.Log(a);
        }
    }

    /// <summary>
    /// Sets the highest score.
    /// </summary>
    /// <param name="score">Score.</param>
    void SetHighestScore(float score){
        PlayerPrefs.SetFloat("lastScore",score);
//        Debug.Log("already set highest score");
//        Debug.Log(PlayerPrefs.GetFloat("score"));
        if(score>0  &&PlayerPrefs.GetFloat("highestScore",0)!=0 && score<PlayerPrefs.GetFloat("highestScore")){
            PlayerPrefs.SetFloat("highestScore",score);

        }


    }
    /// <summary>
    /// Stops the game.
    /// </summary>
    /// <returns>The game.</returns>
    IEnumerator StopGame(){
        yield return new WaitForSeconds(1.5f);

        startGame=false;
    }
    
    
    #endregion


    #region UI

    public void ClickResume(){
		Debug.Log("Clicl Resume Button");
        GetComponents<AudioSource>()[1].Play();
        startGame=true;
        escapeOnce=false;
        canvasESCMenu.enabled=false;
    }
    
    public void ClickLeave(){
		Debug.Log("Click Leave Button");
        Application.LoadLevel("start");
    }

    #endregion
}
