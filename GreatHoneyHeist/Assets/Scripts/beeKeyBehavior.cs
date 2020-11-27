using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beeKeyBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true;          // If print statements should be printed or not

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
            Destroy(gameObject);
            playerHaveKey = true;
        }
    }
}
