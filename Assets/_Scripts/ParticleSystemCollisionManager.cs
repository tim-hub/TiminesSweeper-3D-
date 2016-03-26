using UnityEngine;
using System.Collections;

public class ParticleSystemCollisionManager : MonoBehaviour {

    void OnParticleCollision(GameObject other) {

        GameObject hitObject = other;
        ElementControl hitObjectElement = hitObject.GetComponent<ElementControl>();


        if ((hitObjectElement!=null)
            &&(!(hitObjectElement.IsAMine))
            && (!hitObjectElement.IsSweepered) 
            &&(!hitObjectElement.IsFlagged))
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
