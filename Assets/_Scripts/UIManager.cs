using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class UIManager : MonoBehaviour {

    public Text MinesNum;

    public Text FlagsNum;




    public void SetMines( int i){
        MinesNum.text = i.ToString();
    }

    public void SetFlags(int i){
        FlagsNum.text = i.ToString();

    }
}
