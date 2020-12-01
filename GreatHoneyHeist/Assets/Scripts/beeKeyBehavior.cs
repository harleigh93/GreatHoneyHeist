using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: beeKeyBehavior.cs
*/

public class beeKeyBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = false;          // If print statements should be printed or not

    public static bool playerHaveKey;   // Does the Player have the bee key?

    // Runs upon the object being brought into existance
    private void Awake()
    {
        playerHaveKey = false; // Player starts out without the key
    }

    // ====== COLLIDING WITH PLAYER / PLAYER PICKING UP KEY ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Player
        if (col.gameObject.tag == "Player")
        {
            if (debug) {Debug.Log("Player picked up key");}
            this.transform.parent = col.transform;                  //Have the Key follow the player
            playerHaveKey = true;   
           
        }
        if (col.gameObject.tag == "Door")                           //If the key (Now on the player) hits the door, open the door
        {
            col.transform.Rotate (0,90,0);
            Destroy(gameObject);                                    //Destroy key to avoid rapid door spinning
        }
    }
}
