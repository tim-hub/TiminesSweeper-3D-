using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class ElementControl : MonoBehaviour ,IPointerClickHandler{

    public List<Vector3> Directions =new List<Vector3>(); //to define the line's directions
    //for different shapes of objects



    private bool _isMined;
    private bool _isFlagged;


    void Start(){




    }



    void SendALine(Vector3 direction){
        // cast a ray along the direction



    }


    public void OnPointerClick(PointerEventData eventData){




        for (int i = 0; i < Directions.Count; i++)
        {
            SendALine(Directions [i]);




        }

    }

}
