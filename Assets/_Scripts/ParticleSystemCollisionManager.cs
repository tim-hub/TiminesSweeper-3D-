using UnityEngine;
using System.Collections;

public class ParticleSystemCollisionManager : MonoBehaviour {

    void Awake(){

        Destroy(this.gameObject,1f);

    }

    void OnParticleCollision(GameObject other) {

       // Debug.Log("Particle Collision on " + other.name);

       
        Destroy(this.gameObject);


        if (other.layer == 8) //8 is elements layer
        {
            



            GameObject hitObject = other;


            ElementControl hitObjectElement = hitObject.GetComponent<ElementControl>();


            if ((hitObjectElement != null)
            && (!(hitObjectElement.IsAMine))
            && (!hitObjectElement.IsSweepered)
            && (!hitObjectElement.IsFlagged))
            {



                // not a mine and not be sweepered

                // sweeper the elements near
                if (hitObjectElement.IsABlank)
                {

                    hitObjectElement.ClickOnABlank();


                } else //a number, with mines near
                {
                    hitObjectElement.SweeperThisElement();


                }

            }

        }
    }
}
