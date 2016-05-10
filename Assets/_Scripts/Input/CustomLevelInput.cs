using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomLevelInput : MonoBehaviour {


	public int RateBetweenElementsAndMines;
	public InputField MinesInput;
	public InputField ElementsInput;

	private int _mines;
	private int _elements;
	private int _accurateElements;


	void Start(){

		MinesInput.text=PlayerPrefs.GetInt("MinesQuantity",5).ToString();

		ElementsInput.text=PlayerPrefs.GetInt("ElementsQuantity",27).ToString();

		PlayerPrefs.SetInt("MinesQuantity",5);
		PlayerPrefs.SetInt("ElementsQuantity",27);

	}

	int GetAccurateNumber(int i){

		int numberOfColumns =(int) Mathf.Pow(i, (1f / 3));
		return (int)Mathf.Pow(numberOfColumns,3);
	}



	public void SetMinesQuantity(string str){

		if(int.TryParse(str,out _mines) &&_mines>=0){

			int elements=GetAccurateNumber(RateBetweenElementsAndMines*_mines);
			ElementsInput.text=elements.ToString();

		}else{

			MinesInput.ActivateInputField();
		}



	}




	public void EndInputMines(string str){


		if(int.TryParse(str,out _mines) &&_mines>=0){

			//GameManager.instance.MinesQuantity=_mines;

			PlayerPrefs.SetInt("MinesQuantity",_mines);

			int elements=GetAccurateNumber(RateBetweenElementsAndMines*_mines);
			ElementsInput.text=elements.ToString();


		}else{


			MinesInput.ActivateInputField();
		}

	}


	public void EndInputElements(string str){

		if(int.TryParse(str,out _elements) && GetAccurateNumber(_elements) > _mines) {

			//GameManager.instance.ElementsQuantity=GetAccurateNumber(_elements);

			PlayerPrefs.SetInt("ElementsQuantity",_elements);

			ElementsInput.text=GetAccurateNumber(_elements).ToString();

		}else{

			ElementsInput.ActivateInputField();
		}

	
	}


}
