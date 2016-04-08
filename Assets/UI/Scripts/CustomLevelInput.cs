﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CustomLevelInput : MonoBehaviour {


	public int RateBetweenElementsAndMines;
	public InputField MinesInput;
	public InputField ElementsInput;

	private int _mines;
	private int _elements;
	private int _accurateElements;


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


	public void SetElementQuantity(string str){

	


	}


	public void EndInputMines(string str){


		if(int.TryParse(str,out _mines) &&_mines>=0){

			GameManager.instance.MinesQuantity=_mines;

			int elements=GetAccurateNumber(RateBetweenElementsAndMines*_mines);
			ElementsInput.text=elements.ToString();


		}else{


			MinesInput.ActivateInputField();
		}

	}


	public void EndInputElements(string str){

		if(int.TryParse(str,out _elements) && GetAccurateNumber(_elements) > _mines) {

			GameManager.instance.ElementsQuantity=GetAccurateNumber(_elements);


			ElementsInput.text=GetAccurateNumber(_elements).ToString();


		}else{


			ElementsInput.ActivateInputField();
		}

	
	}

	public void FinishSetting(){



	}
}
