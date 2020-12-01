using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
* TEAM 5
* Harleigh Bass, Kimberly Brooks, Emma Kratt
* SCRIPT: guardBehavior.cs
*/

public class guardBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = false; // If print statements should be printed or not
    
    private Transform player;        // Player character
    private float speed = 8f;        // How fast the guard will move
    private bool canMove;

    // Patroling
    public Transform leftLoc;   // Pre-determined location the guards can move
    public Transform rightLoc;  // Pre-determined location the guards can move
    private bool addMovement;   // Add speed to move to new location if this is true; else, subtract
    public bool vertical;       // Is Guard going up and down?


    // Start is called before the first frame update
    void Start()
    {
        canMove = true;
        player = GameObject.Find("playerBee").transform; // Find and assign Player

        float direction = Random.Range (0f, 1f);
        if (debug) {Debug.Log("direction choice: " + direction);}
        if (direction <= 0.5)
        {
            addMovement = true;
        }
        else
        {
            addMovement = false;
            this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World); // About to move right, so face right
        }

        Invoke("Patroling", 0f);
    }


    // Update is called once per frame
    void Update()
    {
        
    }


    // ====== PATROL ======
    private void Patroling()
    {
        if (canMove)
        {
            Vector3 currentPos; // Vector of current Position
            currentPos = this.transform.position; // What is current position of Guard?

            // SWAP DIRECTION ON REACHING TARGET LOCATION
            if (vertical)
            {
                // Guard bee is moving left and right (X-Axis)


                // If Guard is going right and current position is >= right location, swap direction
                if (addMovement && currentPos.x >= rightLoc.position.x)
                {
                    addMovement = false;
                    this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World); // About to move left, so face left
                } // If Guard is going left and current position is <= left location, swap direction
                else if (!addMovement && currentPos.x <= leftLoc.position.x)
                {
                    addMovement = true;
                    this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World); // About to move right, so face right
                }


                // MOVEMENT
                if (addMovement)
                {
                    currentPos.x += speed; // move right
                }
                else
                {
                    currentPos.x -= speed; // move left
                }
            } 
            else
            {
                // Guard bee is moving up and down (Z-Axis)


                // If Guard is going right (up) and current position is >= right (up) location, swap direction
                if (addMovement && currentPos.z >= rightLoc.position.z)
                {
                    addMovement = false;
                    this.transform.Rotate(0.0f, 180.0f, 0.0f, Space.World); // About to move down, so face down
                } // If Guard is going left (down) and current position is <= left (down) location, swap direction
                else if (!addMovement && currentPos.z <= leftLoc.position.z)
                {
                    addMovement = true;
                    this.transform.Rotate(0.0f, -180.0f, 0.0f, Space.World); // About to move up, so face up
                }


                // MOVEMENT
                if (addMovement)
                {
                    currentPos.z += speed; // move up
                }
                else
                {
                    currentPos.z -= speed; // move down
                }
            }

            this.transform.position = currentPos; // apply new translation to Guard
            Invoke("Patroling", 0.02f);
        }
        else{
            if (debug) {Debug.Log("Guard is stuck in honey. No movement.");}
        }
    }


    // ====== GETTING STUCK IN HONEY ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to a honey cube
        if (col.gameObject.tag == "honeyCube")
        {
            canMove = false; // Guard can no longer move
            if (debug) {Debug.Log("Guard can't move. Stuck in Honey!");}

            col.gameObject.GetComponent<Collider>().isTrigger = false; // Prevents Honey Cube from being able to be pushed again
            gameObject.GetComponent<Collider>().isTrigger = false;     // Prevents Guards from triggering restart game method if Player touches them while in Honey
            
            Vector3 currentPos; // Vector of current position
            currentPos = this.transform.position; // get Guard's position

            // Put Guard INTO honey cube
            currentPos.x = col.gameObject.transform.position.x;
            currentPos.z = col.gameObject.transform.position.z;

            this.transform.position = currentPos; // Apply new translation to object
        }
    }
}
