using UnityEngine;
using System.Collections;

public class MineManager : MonoBehaviour {

    public GameObject exp;
    public float mineLife = 2f;

    void Awake(){

        Invoke("GameOver", mineLife);
        Destroy(this.gameObject,mineLife);



    }

    void GameOver(){
        Debug.Log("run game over from mine");


        Instantiate(exp, transform.position, transform.rotation);

        GameManager.instance.GameOver();

    }
}
