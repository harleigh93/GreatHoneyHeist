using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: floatingBees.cs
*/

public class floatingBees : MonoBehaviour
{
    // Bees will constantly float in the air - up and down!

    private float speed = 15f; // How fast the bee will move up and down in the air
    private bool goUp;        // Will bee be going up or down?
    private int counter;


    // Start is called before the first frame update
    void Start ()
    {
        float direction = Random.Range(0f, 1f);
        if (direction <= 0.5)
        {
            goUp = true;
            counter = 0;
        }
        else
        {
            goUp = false;
            counter = 3;
        }

        Invoke("Float", 0.2f);
    }


    // ====== BEES FLOATING UP AND DOWN ======
    private void Float()
    {
        Vector3 currentPos; // Vector for current position
        currentPos = this.transform.position; // What is current position of bee?

        if (goUp)
        {
            // move up
            currentPos.y += speed;

            counter += 1;
            if (counter >= 3)
            {
                counter = 3;
                goUp = false;
            }
        }
        else{
            // move down
            currentPos.y -= speed;

            counter -= 1;
            if (counter <= 0)
            {
                counter = 0;
                goUp = true;
            }
        }

        this.transform.position = currentPos; // apply new translation to bee

        Invoke("Float", 0.1f);
    }
}
