using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: playerBehavior.cs
*/

public class playerBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = false; // If print statements should be printed or not

    // General Sword Variables
    private Transform playerbee;
    private Transform sword; // variable to store sword model in the scene
    private Transform swordSock; // variable to store empty GameObject with position/rotation for sword placement
    private Transform swordFoundLocation; // variable to store location where Player will find the sword
    private bool hasSword;// Does Player have sword yet?

    // Sword Rotation Variables
    private float rotationSpeed = 1f;
    private Quaternion targetRotation;
    private bool swingLeft = true; // If sword will swing left

    // Honey Blocks Variable
    private Transform cube;

    // Score Variable for Honey Pots
    public Text newScore;

    // Player Bee Health
    private float health = 5;

    // Variables for Queen Bee appearances and dialogue
    public GameObject QueenBee;         //Create gameobjects for objects to be spawned in (the queen prefab, and her text / textboxes)
    public GameObject QueenBeeIns;
    public GameObject Room1Text;
    public GameObject Room2Text;
    public GameObject Room3Text;
    public GameObject Room4Text;
    public GameObject textbox;
    public GameObject contButton;
    private bool isQueen2Out = false;   //mark all future queen instantiations as false
    private bool isQueen1Out = false;
    private bool isQueen3Out = false;
    private bool isQueen4Out = false;
    public static bool finishedGame = false; // Is Player at the end of the game?


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

        Room2Text.SetActive(false);
        textbox.SetActive(false);
        contButton.SetActive(false);

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
    // ====== COLLIDING WITH HONEY CUBE / MOVE CUBE ======
    // ====== COLLIDING WITH SWORD / PLAYER PICKING UP SWORD ======
    // ====== COLLIDING WITH BABY BEES ======
    // ====== COLLIDING WITH GUARD BEE / RESTARTING GAME ======
    // ====== COLLIDING WITH QUEEN TRIGGERS / SUMMON QUEEN ======
    private void OnTriggerEnter(Collider col)
    {

        // If the player has collided specifically with a honey pot
        if(col.gameObject.tag == "honeyPot")
        {
            Destroy(col.gameObject);                // Destroy the pot

            int score = int.Parse(newScore.text);   //find the previous score
            score += 1;                             // Increase the score by 1
            newScore.text = score.ToString();       // Print out new score
        }


        // If the player has collided specifically with a honey cube
        if(col.gameObject.tag == "honeyCube")
        {

            Vector3 pos;                                // Vector to hold position
            pos = col.gameObject.transform.position;    // Make position current cube's position

            // Depending on which direction Player is moving, move Honey Cube in that direction
            if (PlayerMovement.direction == 0) // UP
            {
                //if (debug) {Debug.Log("Player is facing up");}
                pos.z +=100;  // add in positive z direction
            }
            if (PlayerMovement.direction == 1) // LEFT
            {
                //if (debug) {Debug.Log("Player is facing left");}
                pos.x -=100;  // add in positive z direction
            }
            if (PlayerMovement.direction == 2) // DOWN
            {
                //if (debug) {Debug.Log("Player is facing down");}
                pos.z -=100;  // add in positive z direction
            }
            if (PlayerMovement.direction == 3) // RIGHT
            {
                //if (debug) {Debug.Log("Player is facing right");}
                pos.x +=100;  // add in positive z direction
            }

            col.gameObject.transform.position = pos;    // apply new position to cube to move it
        }


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


        // Check to see if the collided object is the first empty Queen Triggering object (Occurs in cell)
        if (col.gameObject.tag == "room1")
        {
            if (isQueen1Out == false)         //Check that the queen has not already been summoned
            {
                QueenBeeIns = Instantiate<GameObject>(QueenBee);                //Summon Queen and move her to apporpriate position
                QueenBeeIns.transform.position = new Vector3(-121, 1689, 2507);
                isQueen1Out = true;                                             //Prevents multiple queen summons
                Room1Text.SetActive(true);                                      //Activate the appropriate text, textbox, and the continue button
                textbox.SetActive(true);                                    
                contButton.SetActive(true);         
            }
        }


        // Check to see if the collided object is the second empty Queen Triggering object (Occurs before baby bee room) See comments above ^
        if (col.gameObject.tag == "room2")
        {
            if (isQueen2Out == false)  
            {
                QueenBeeIns = Instantiate<GameObject>(QueenBee);
                QueenBeeIns.transform.position = new Vector3(28633, 1840, 25814);
                isQueen2Out = true;
                Room2Text.SetActive(true);
                textbox.SetActive(true);
                contButton.SetActive(true);
            }
        }


        // Check to see if the collided object is the third empty Queen Triggering object (Occurs before maze) See comments above ^
        if (col.gameObject.tag == "room3")
        {
            if (isQueen3Out == false)
            {
                QueenBeeIns = Instantiate<GameObject>(QueenBee);
                QueenBeeIns.transform.position = new Vector3(50929, 1763, 29290);
                isQueen3Out = true;
                Room3Text.SetActive(true);
                textbox.SetActive(true);
                contButton.SetActive(true);
            }
        }


        // Check to see if the collided object is the final empty Queen Triggering object (Occurs after maze) See comments above ^
        if (col.gameObject.tag == "endGame")
        {
            if (isQueen4Out == false)
            {
                QueenBeeIns = Instantiate<GameObject>(QueenBee);
                QueenBeeIns.transform.position = new Vector3(76617, 1788, 16480);
                isQueen4Out = true;
                Room4Text.SetActive(true);
                textbox.SetActive(true);
                contButton.SetActive(true);
                Time.timeScale = 0;
                finishedGame = true; // Player is at the end of the game/final trigger box
            }
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
