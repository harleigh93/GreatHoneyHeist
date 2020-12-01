using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: babyBeeBehavior.cs
*/

public class babyBeeBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = false; // If print statements should be printed or not
    
    // Baby Bee Health
    private float health = 3;

    // Chasing Variables
    private Transform player; // Player character
    private float rotationSpeed = 3.0f;
    private float moveSpeed = 200.0f; // movement speed



    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("playerBee").transform; // Find and assign Player
    }


    // Update is called once per frame
    void Update()
    {
        // Look at the Player - from location, to location (distance bw player and this bee), then speed of rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        chasePlayer();
    }


    // ====== BABY BEES CHASE PLAYER ======
    private void chasePlayer()
    {
        // bee's position = from position, to position, speed of movement
        transform.position = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
    }



    // ====== COLLIDING WITH SWORD / PLAYER HITTING WITH SWORD ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Player
        if (col.gameObject.tag == "sword")
        {
            if (debug) {Debug.Log("Player hit baby bees with sword");}
            TakeDamage();
        }
    }


    // ====== BABY BEES TAKE DAMAGE ======
    public void TakeDamage() {
        if (debug) {Debug.Log("Baby Bees are taking damage!");}
        health -= 1;

        if (debug) {Debug.Log("Baby Bee health: " + health);}

        if (health <= 0) {
            KO_BabyBees();
        }
    }


    // ====== BABY BEES GET KO'D ======
    private void KO_BabyBees()
    {
        Destroy(gameObject);
    }
}
