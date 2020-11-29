using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not

    private Transform playerbee;
    private Transform sword; // variable to store sword model in the scene
    private Transform swordSock; // variable to store empty GameObject with position/rotation for sword placement
    private Transform swordFoundLocation; // variable to store location where Player will find the sword
    private bool hasSword;// Does Player have sword yet?

    // Sword Rotation Variables
    private float rotationSpeed = 1f;
    private Quaternion targetRotation;
    private bool swingLeft = true; // If sword will swing left

    public Text newScore;

    // Player Bee Health
    private float health = 5;

    void Start()
    {
        // SWORD CODE
        // Find and assign sword
        sword = GameObject.FindWithTag("sword").transform;
        // Find and assign socket for sword placement
        swordSock = GameObject.FindWithTag("swordSocket").transform;
        // Find and assign initial location for the sword
        swordFoundLocation = GameObject.FindWithTag("swordInitial").transform;

        // At the start of the game, sword will go to a specific location in the level for the Player to later find
        sword.transform.SetParent(swordFoundLocation);
        sword.transform.localPosition = Vector3.zero;
        sword.transform.localRotation = Quaternion.Euler(Vector3.zero);
        sword.transform.localScale = Vector3.one;

        swingLeft = true; // Assign initial sword direction to left
        hasSword = false; // player does not have sword at start of game
                
        GameObject ourScore;                                // Create score object to hold quantity text
        ourScore = GameObject.Find("PotScoreText");         // Store the current text in ourscore
        newScore = ourScore.GetComponent<Text>();           // Store the current score in new score, to be later incrememnted
        newScore.text = "0";                                // Set starting text to 0

        playerbee = GameObject.FindWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        // SWING SWORD ON SPACE BAR PRESS (only if Player has sword)
        if (hasSword && (Input.GetKeyDown(KeyCode.Space)) )
        {
            swingLeft = true; // set to true on initial space press to restart swing direction

            Invoke("Attack", 0f);    // set rotation direction
            Invoke("Attack", 0.25f); // change rotation direction
            Invoke("Attack", 0.45f); // change rotation direction again to go back to center
        }
        // Update sword rotation on every frame
        if (hasSword)
        {
            sword.rotation = Quaternion.Lerp(sword.rotation, swordSock.rotation, 10 * rotationSpeed * Time.deltaTime);            
        }
    }

    // ====== COLLIDING WITH HONEY POT / SCORE INCREASE ======
    void OnCollisionEnter(Collision coll)
    {
        GameObject collidedWith = coll.gameObject;
        if(collidedWith.tag == "honeyPot")              //If the player has collided specifically with a honey pot
        {
            Destroy(collidedWith);                      // Destroy the pot

            int score = int.Parse(newScore.text);       // find the previous score
            score += 1;                                 // Increase the score by 1
            newScore.text = score.ToString();           // Print out new score

        }
        //if (collidedWith.tag == "honeyCube")
        //{
        //    Debug.Log("Player hit Block");
        //    collidedWith.transform.position.x = collidedWith.transform.position.x + 20;
        //}
    }


    // ====== COLLIDING WITH SWORD / PLAYER PICKING UP SWORD ======
    // ====== COLLIDING WITH BABY BEES ======
    // ====== COLLIDING WITH GUARD BEE / RESTARTING GAME ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Sword
        if (col.gameObject.tag == "sword")
        {
            //if (debug) {Debug.Log("Player found sword");}
            hasSword = true; // Player now has sword


            // Parent sword to swordSock so it will follow the player
            // Move sword to default position/rotation of swordSock
            sword.transform.SetParent(swordSock);
            sword.transform.localPosition = Vector3.zero;
            sword.transform.localRotation = Quaternion.Euler(Vector3.zero);
            sword.transform.localScale = Vector3.one;
        }


        // Check to see if tag on collider is == to the babyBees
        if (col.gameObject.tag == "babyBees")
        {
            if (debug) {Debug.Log("Player hit by baby bees!");}
            TakeDamage();
        }


        // Check to see if tag on collider is == to the guard
        if (col.gameObject.tag == "guard")
        {
            if (debug) {Debug.Log("Player hit by guard");}
            restartGame();
        }
    }


    // ====== ATTACKING (swinging sword) ANIMATION VARIABLES FOR UPDATE FUNCTION ======
    private void Attack()
    {
        // swing sword left
        if (swingLeft)
        {
            // Set new angle rotation for sword using Quaternions
            swordSock.rotation *= Quaternion.AngleAxis(45, Vector3.left);
            swingLeft = false; // next time go right
        }
        // swing sword right
        else
        {
            // Set new angle rotation for sword using Quaternions
            swordSock.rotation *= Quaternion.AngleAxis(-90, Vector3.left);
            swingLeft = true; // next time go left
        }
    }


    // ====== PLAYER TAKE DAMAGE ======
    public void TakeDamage() {
        if (debug) {Debug.Log("Player is taking damage!");}
        health -= 1;

        if (debug) {Debug.Log("Player health: " + health);}

        if (health <= 0) {
            restartGame();
        }
    }


    // ====== RESTART GAME ON "DEATH" or CAUGHT BY GUARD ======
    private void restartGame()
    {
        // Loads very first scene/level in the game; while also restarting the game, not just the level
        SceneManager.LoadScene(0);
    }
}
