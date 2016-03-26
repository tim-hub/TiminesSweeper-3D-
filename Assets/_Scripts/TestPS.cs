using UnityEngine;
using System.Collections;

public class TestPS : MonoBehaviour {

    void OnParticleCollision(GameObject other) {

        Debug.Log("touch other object");

        other.gameObject.SetActive(false);
        this.gameObject.SetActive(false);


    }
}
