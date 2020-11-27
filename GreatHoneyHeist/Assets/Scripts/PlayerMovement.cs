using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    int direction = 0;              //Variable for checking player's current facing direction, 0 = up, 1 = left, 2 = down, 3 = right
    
    void Start()
    {
        this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        if (Input.GetKey (KeyCode.D))               //If player wants to move RIGHT, and has hence pressed D on keyboard
        {
            if (direction != 3)                     //Check that bee is not already facing that in the desired direction
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);          //if in wrong direction, change world rotation of player to desired direction (in this case, right)
                direction = 3;                                                          //Acknowledge that player is now facing in the correct direction
            }
            transform.Translate(15f, 0f, 0f, Space.World);                              //Move player
        }
        if (Input.GetKey(KeyCode.A))                //If player wants to move LEFT.  Repeat above descriptions ^
        {
            if (direction != 1)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 270.0f, 0.0f);
                direction = 1;
            }
            transform.Translate(-15f, 0f, 0f, Space.World);
        }
        if (Input.GetKey(KeyCode.S))                //If player wants to move DOWN.  Repeat above descriptions ^
        {
            if (direction != 2)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 180f, 0.0f);
                direction = 2;
            }
            transform.Translate(0.00f, 0f, -15f, Space.World);
        }
        if (Input.GetKey(KeyCode.W))                //If player wants to move UP.  Repeat above descriptions ^
        {
            if (direction != 0)
            {
                this.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                direction = 0;
            }
            transform.Translate(0.00f, 0f, 15f, Space.World);
        }
    }
}