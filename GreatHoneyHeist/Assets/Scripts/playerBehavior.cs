using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBehavior : MonoBehaviour
{

    // Debugging
    private bool debug = true; // If print statements should be printed or not

    private Transform sword; // variable to store sword model in the scene
    private Transform swordSock; // variable to store empty GameObject with position/rotation for sword placement
    private bool hasSword;
    private bool rotated; // Has sword been rotated yet?


    // Start is called before the first frame update
    void Start()
    {
        // Find and assign sword
        sword = GameObject.FindWithTag("sword").transform;
        // Find and assign socket for sword placement
        swordSock = GameObject.FindWithTag("swordSocket").transform;

        hasSword = false;
        rotated = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hasSword)
        {
            Vector3 pos; // Vector to hold position
            pos = swordSock.position;  // set vector to position of sword socket on player bee
            sword.position = pos; // assign new translation to sword
        }
    }


    // ====== COLLIDING WITH SWORD / PLAYER PICKING UP SWORD ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Sword
        if (col.gameObject.tag == "sword")
        {
            if (debug) {Debug.Log("Player found sword");}
            hasSword = true;
            if (!rotated)
            {
                sword.transform.Rotate(0.0f, 0.0f, -50.0f, Space.World); // Rotate sword once
                rotated = true;
            }
        }
    }
}
