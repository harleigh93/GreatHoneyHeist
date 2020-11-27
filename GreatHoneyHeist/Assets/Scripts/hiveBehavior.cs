using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hiveBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not

    private Transform key; // Bee key model
    private bool hasKey;   // Does beehive have key?

    // Baby Bee Prefab
    public GameObject babyBeePrefab;// Baby bee prefab model

    // Create internal variable in code to hold the prefab
    public GameObject babyBee1;
    public GameObject babyBee2;
    public GameObject babyBee3;


    // Start is called before the first frame update
    void Start()
    {
        hasKey = false;

        // Find and assign bee key
        key = GameObject.FindWithTag("beekey").transform; // Find and assign Bee Key
    }


    // Update is called once per frame
    void Update()
    {
        
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
        babyBee1 = Instantiate<GameObject>(babyBeePrefab);
        babyBee2 = Instantiate<GameObject>(babyBeePrefab);
        babyBee3 = Instantiate<GameObject>(babyBeePrefab);


        Vector3 pos; // Vector to hold position
        pos = this.transform.position;  // set vector to beehive position
        pos.y += 160f; // move up a little is visible above ground
        babyBee1.transform.position = pos; // assign new translation to baby bee 1
        babyBee2.transform.position = pos; // assign new translation to baby bee 2
        babyBee3.transform.position = pos; // assign new translation to baby bee 3


        // Move babyBee2 up a little
        pos.y += 275f;
        pos.x += 175f;
        babyBee2.transform.position = pos; // Apply new translation to baby bee

        // Move babyBee3 to the side a little
        Vector3 pos2;
        pos2 = babyBee3.transform.position; // What is current position of babyBee3?
        pos2.x += 300f;
        babyBee3.transform.position = pos2; // Apply new translation to baby bee
    }
}
