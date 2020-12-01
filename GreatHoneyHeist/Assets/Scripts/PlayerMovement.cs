using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: PlayerMovement.cs
*/

public class PlayerMovement : MonoBehaviour
{
    public static int direction = 0;              //Variable for checking player's current facing direction, 0 = up, 1 = left, 2 = down, 3 = right
    
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKey (KeyCode.D))               // check if player wants to move RIGHT, and has hence pressed 'D' on keyboard
        {
            if (direction != 3)                     //Check that bee is not already facing in the desired direction
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);          //if in wrong direction, change world rotation of player to desired direction (in this case, right)
                direction = 3;                                                          //Acknowledge that player is now turned in the correct direction
            }
            transform.Translate(1000f * Time.deltaTime, 0f, 0f, Space.World);                              //Move player 25 units
        }
        if (Input.GetKey(KeyCode.A))                //If player wants to move LEFT ('A').  Repeat above descriptions ^
        {
            if (direction != 1)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                direction = 1;
            }
            transform.Translate(-1000f * Time.deltaTime, 0f, 0f, Space.World);
        }
        if (Input.GetKey(KeyCode.S))                //If player wants to move DOWN ('S').  Repeat above descriptions ^
        {
            if (direction != 2)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
                direction = 2;
            }
            transform.Translate(0f, 0f, -1000f * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.W))                //If player wants to move UP ('W').  Repeat above descriptions ^
        {
            if (direction != 0)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                direction = 0;
            }
            transform.Translate(0f, 0f, 1000f * Time.deltaTime, Space.World);
        }
    }
}