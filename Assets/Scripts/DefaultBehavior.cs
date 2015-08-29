using UnityEngine;
using System.Collections;

public class DefaultBehavior : MonoBehaviour {

	public GameObject mine;
	public Texture num1;
	public Texture num2;
	public Texture num3;
	public Texture num4;
	public Texture num5;
	public Texture num6;


	GameObject newParentObject;
	
	string flag;

	int numOfCubes;
	int rows;
	int[] minesIndex;
	int[] numCubesIndex;

	#region UnityMethods
	void Start(){
		newParentObject=GameObject.FindGameObjectWithTag("Parent");

		numOfCubes=gameController.numOfCubes;
		rows=(int)(Mathf.Pow(numOfCubes,1f/3)); 

		minesIndex=newParentObject.GetComponent<gameController>().minesIndex;
		numCubesIndex=newParentObject.GetComponent<gameController>().numCubesIndex;

	}



	void OnMouseDown () {
		//Debug.Log (this.transform.position);
		int i=Vector3ToInt(this.transform.position);

		if(CheckInIndex(minesIndex,i)){

			GameObject go=Instantiate(mine,this.transform.position,new Quaternion(0,0,0,0)) as GameObject;
			Destroy(this.gameObject);
			go.transform.parent=newParentObject.transform;
			
		}else if(GetNumOfNumCubes(i)!=0){
			int numCount=GetNumOfNumCubes(i);

			Debug.Log(numCount);
			//set texture
			switch (numCount)
			{
			case 1:
				this.GetComponent<Renderer>().material.mainTexture=num1;
				break;
			case 2:
				this.GetComponent<Renderer>().material.mainTexture=num2;
				break;
			case 3:
				this.GetComponent<Renderer>().material.mainTexture=num3;
				break;
			case 4:
				this.GetComponent<Renderer>().material.mainTexture=num4;
				break;
			case 5:
				this.GetComponent<Renderer>().material.mainTexture=num5;
				break;
			case 6:
				this.GetComponent<Renderer>().material.mainTexture=num6;
				break;
			default:
				break;
			}
			
			
		}else{
			this.GetComponent<Renderer>().material.color=Color.white;

		}

		
	}

	#endregion

	#region UserMethods

	int Vector3ToInt(Vector3 cubePosition){
		return (int)(((cubePosition.x+rows+1)/2-1)*rows*rows+((cubePosition.y+rows+1)/2-1)*rows
		             +(cubePosition.z+rows+1)/2-1);
	}


	//the index in array?
	bool CheckInIndex(int[] array, int index){
		for(int i=0;i<array.Length;i++){
			//print (array[i]==index);
			if (array[i]==index){
				return true;
			}
		}
		return false;
	}

	int GetNumOfNumCubes(int i){

		int numCount=0;
		//check the surrounds
		if(i+9<=numOfCubes && CheckInIndex(minesIndex,i+9)){
				numCount++;
			}
		if(i+3<=numOfCubes && CheckInIndex(minesIndex,i+3)){
				numCount++;
			}
		if(i+1<=numOfCubes && CheckInIndex(minesIndex,i+1)){
				numCount++;
			}
		if(i-9>=0 && CheckInIndex(minesIndex,i-9)){
				numCount++;
			}
		if(i-3>=0 && CheckInIndex(minesIndex,i-3)){
				numCount++;
			}
		if(i-1>=0 && CheckInIndex(minesIndex,i-1)){
				numCount++;
			}

		return numCount;
	
	}

	void SetMinesIndex(int[] index){
		//minesIndex=index;
		minesIndex=newParentObject.GetComponent<gameController>().minesIndex;

	}
	void SetNumCubesIndex(int[] index){
		
		numCubesIndex=index;
	}




	#endregion
}
