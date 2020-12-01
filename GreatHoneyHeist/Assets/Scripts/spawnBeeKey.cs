using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: spawnBeeKey.cs
*/

public class spawnBeeKey : MonoBehaviour
{
    // Debugging
    private bool debug = false;      // If print statements should be printed or not

    public Transform[] beehives;    // Array of all beehives in the scene
    private int randomHive;          // Hive that will have the bee key

    // Create variable to hold the prefab
    public GameObject beeKey;


    // Start is called before the first frame update
    void Start()
    {
        // Move bee key to position of a random bee hive
        randomHive = Random.Range(0, beehives.Length);
        beeKey.transform.position = beehives[randomHive].transform.position;
        Vector3 pos;
        pos = beeKey.transform.position; // set vector to new beeKey position
        pos.y += 160f; // move up so key is visible above ground
        beeKey.transform.position = pos; // assign new translation to bee key

        if (debug) {Debug.Log("Bee Key spawned in level under hive " + (randomHive + 1));}
    }
}