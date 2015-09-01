using UnityEngine;
using System.Collections;

public class DefaultBehavior : MonoBehaviour {

	public GameObject mine;
    public GameObject explosionAnim;
    public Texture defaultTexture;
	public Texture num1;
	public Texture num2;
	public Texture num3;
	public Texture num4;
	public Texture num5;
	public Texture num6;
    public Texture flag;


	GameObject newParentObject;
	
	

	int numOfCubes;
	int rows;
	int[] minesIndex;
	int[] numCubesIndex;

    bool flagShowAlready;

   


	#region UnityMethods
	void Start(){
        flagShowAlready=false;
    

		newParentObject=GameObject.FindGameObjectWithTag("Parent");

		numOfCubes=gameController.gc.numOfCubes;
		rows=(int)(Mathf.Pow(numOfCubes,1f/3)); 

		minesIndex=newParentObject.GetComponent<gameController>().minesIndex;
		numCubesIndex=newParentObject.GetComponent<gameController>().numCubesIndex;

	}

    void Update(){

    
    }

    void OnMouseOver(){
        Debug.Log("Mouse Over");
        if(Input.GetMouseButtonDown(1)){

            if(!flagShowAlready){
                Debug.Log("right click 1");
                this.GetComponent<Renderer>().material.mainTexture=flag;
            
                flagShowAlready=true;
            }else{
                Debug.Log("right click twice");
                this.GetComponent<Renderer>().material.mainTexture=defaultTexture;

                flagShowAlready=false;
            }

        }
 
    } 
        


	void OnMouseDown () {
		Debug.Log (this.transform.localPosition);
		int i=Vector3ToInt(this.transform.localPosition);

        if(!flagShowAlready){
    		if(CheckInIndex(minesIndex,i)){

    			GameObject go=Instantiate(mine,this.transform.position,new Quaternion(0,0,0,0)) as GameObject;
    			Destroy(this.gameObject);
    			go.transform.parent=newParentObject.transform;
                Destroy(go,1f);

                Instantiate(explosionAnim,this.transform.position,new Quaternion(0,0,0,0));

                   

    			
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
                this.GetComponent<Renderer>().material.mainTexture=null;
              

                //this.transform.localScale=new Vector3(0.5f,0.5f,0.5f);
                Destroy(this.gameObject,0.8f);
                
    		}
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


    /// <summary>
    /// get the number of surrounding  num cubes
    /// </summary>
    /// <returns>The number of number cubes.</returns>
    /// <param name="i">The index.</param>
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


    void Explosion(){
        //audio


        //anim

    
    }



	#endregion
}
