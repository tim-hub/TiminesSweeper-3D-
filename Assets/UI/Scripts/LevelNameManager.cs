using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Text))]
public class LevelNameManager : MonoBehaviour {

	void Start(){
		GetComponent<Text>().text=SceneManager.GetActiveScene().name;

	}
}
