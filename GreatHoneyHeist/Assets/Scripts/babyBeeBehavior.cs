using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class babyBeeBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not

    // Baby Bee Health
    private float health = 3;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // ====== BABY BEES CHASE PLAYER ======



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
        //Destroy(babyBee1);
        //Destroy(babyBee2);
        //Destroy(babyBee3);
    }
}
