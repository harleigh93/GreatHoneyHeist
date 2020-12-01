using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: hiveBehavior.cs
*/

public class hiveBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = false; // If print statements should be printed or not

    private Transform key; // Bee key model
    private bool hasKey;   // Does beehive have key?

    // BABY BEE VARIABLES
    
    // Baby Bees Swarm Prefab
    public GameObject SwarmPrefab;

    // Create internal variable in code to hold the prefab
    public GameObject babyBeeSwarm;



    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;

        // Find and assign bee key
        key = GameObject.FindWithTag("beekey").transform;
    }


    // ====== COLLIDING WITH SWORD / PLAYER HITTING WITH SWORD ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Sword
        if (col.gameObject.tag == "sword")
        {
            if (debug) {Debug.Log("Player hit hive with sword");}
            Destroy(gameObject);

            if (!hasKey)
            {
                spawnBabyBees(); // Spawns baby bees
            }
        }


        // Check to see if tag on collider is == to the beekey
        if (col.gameObject.tag == "beekey")
        {
            // Key is under hive
            hasKey = true;

            if (hasKey)
            {
                if (debug) {Debug.Log("Found bee key");}
            }
        }
    }


    // ====== SPAWN BABY BEES ======
    public void spawnBabyBees()
    {
        // Spawn baby bee swarm
        babyBeeSwarm = Instantiate<GameObject>(SwarmPrefab);


        Vector3 pos; // Vector to hold position
        pos = this.transform.position;  // set vector to beehive position
        pos.y += 160f; // move up a little is visible above ground
        babyBeeSwarm.transform.position = pos; // assign new translation to baby bee swarm
    }
}
