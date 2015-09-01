using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class gameController : MonoBehaviour {

	#region publicVars
    public static gameController gc;
	//link the objects
	public GameObject defaultCube;
	public GameObject parentObeject;
	public int numOfCubes=27; //the sum of cubes
	public int numOfMines=5; //sum of mines
    public int distance=2; //distance between cube and cube
	#endregion 


	#region privateVars

	private Vector3[] cubesArray;
	private Vector3[] minesArray; //use int as a symbol for mines

	private int rows;

	public int[] minesIndex;
	public int[] numCubesIndex;
	#endregion


	#region UnityMethods



	// Use this for initialization
	void Start () {
        gc=this;



		rows=(int)(Mathf.Pow(numOfCubes,1f/3));  //use 1f/3 not 1/3
		cubesArray=CreateCubesArray(numOfCubes);

		minesArray=new Vector3[]{new Vector3(-2f,-2f,-2f),new Vector3(0f,0f,0f),new Vector3(2,-2,-2),new Vector3(2,2,2),
			new Vector3(2,0,2)};

		// later create a method and then create this array randomly


		minesIndex=GetMinesIndex(minesArray);

        //ShowArray(minesIndex);

		numCubesIndex=GetNumCubesIndex(minesIndex);



		CreateMap(cubesArray,minesIndex,numCubesIndex);
	}




	
	// Update is called once per frame
	void Update () {




	}

	#endregion




	#region UserMethods
	void CreateMap(Vector3[] cubesArray,int[] mines, int[] nums){
		GameObject go;
		//int numCount=0;
		for (int i=0;i<numOfCubes;i++){
			go=Instantiate(defaultCube,cubesArray[i],new Quaternion(0,0,0,0)) as GameObject;
			go.transform.parent=parentObeject.transform;

		}
		

	}



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

	int[] GetMinesIndex( Vector3[] minesArray){
		int[] minesIndex=new int[minesArray.Length];
		
		for (int i=0;i<minesIndex.Length;i++){
			
			minesIndex[i]=Vector3ToInt(minesArray[i]);
			
			
		}
		
		return minesIndex;
		
		
	}
	
	int Vector3ToInt(Vector3 cubePosition){
		
		
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

    void ShowArray(int[] array){
        foreach(int a in array){
            Debug.Log(a);
        }
    }




	

	#endregion
}
