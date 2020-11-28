using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not

    private Transform sword; // variable to store sword model in the scene
    private Transform swordSock; // variable to store empty GameObject with position/rotation for sword placement
    private bool hasSword;// Does Player have sword yet?
    private bool rotated; // Has sword been rotated yet?

    // Sword Rotation Variables
    private float rotationSpeed = 1f;
    private Quaternion targetRotation;
    private bool swingLeft = true; // If sword will swing left

    public Text newScore;

    void Start()
    {

        // SWORD CODE
        // Find and assign sword
        sword = GameObject.FindWithTag("sword").transform;
        // Find and assign socket for sword placement
        swordSock = GameObject.FindWithTag("swordSocket").transform;
        // Assign initial sword direction to left
        swingLeft = true;
        // Set variables to false
        hasSword = false;
        rotated = false;
                
        GameObject ourScore;                                // Create score object to hold quantity text
        ourScore = GameObject.Find("PotScoreText");         // Store the current text in ourscore
        newScore = ourScore.GetComponent<Text>();           // Store the current score in new score, to be later incrememnted
        newScore.text = "0";                                // Set starting text to 0

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
            sword.rotation = Quaternion.Lerp (sword.rotation, targetRotation, 10 * rotationSpeed * Time.deltaTime); 
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
    }

    // ====== COLLIDING WITH SWORD / PLAYER PICKING UP SWORD ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to the Sword
        if (col.gameObject.tag == "sword")
        {
            if (debug) {Debug.Log("Player found sword");}
            hasSword = true; // Player now has sword

            // rotated variable makes sure that this only happens once
            if (!rotated)
            {
                Vector3 pos; // Vector to hold position
                pos = swordSock.position;  // set vector to position of sword socket on player bee
                sword.position = pos; // assign new translation to sword

                sword.transform.Rotate(0.0f, 0.0f, -50.0f, Space.World); // Rotate sword once
                rotated = true; // won't rotate it again

                targetRotation = sword.rotation; // store sword rotation in this variable for later attack rotation

                sword.transform.parent = swordSock; // parent sword to swordSock so it will follow the player
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
            targetRotation *= Quaternion.AngleAxis(45, Vector3.right);
            swingLeft = false; // next time go right
        }
        // swing sword right
        else
        {
            // Set new angle rotation for sword using Quaternions
            targetRotation *= Quaternion.AngleAxis(-90, Vector3.right);
            swingLeft = true; // next time go left
        }
    }
}
