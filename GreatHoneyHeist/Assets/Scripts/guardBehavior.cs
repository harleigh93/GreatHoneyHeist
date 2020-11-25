using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class guardBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should print or not
    
    private Transform player;    // Player character
    private float speed = 15f; // How fast the guard will move
    public float health;
    private bool canMove;

    // Patroling
    public Transform leftLoc;   // Pre-determined location the guards can move
    public Transform rightLoc;  // Pre-determined location the guards can move
    private bool addMovement;   // Add speed to move to new location if this is true; else, subtract
    public bool vertical;       // Is Guard going up and down?

    // Attacking
    private float attackCooldown = 2f; // Wait time before guard can attack again
    private bool alreadyAttacked;

    // States
    private float sightRange = 1500f;    // How far away the guard can see the player
    private float attackRange = 800f;    // How close the player has to be before the guard attacks
    private bool playerInSightRange, playerInAttackRange;



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
    }


    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            // Is Player in sight range?
            if (Vector3.Distance(transform.position, player.transform.position) < sightRange)
            {
                if (debug) {Debug.Log("Player is in sight range");}
                playerInSightRange = true;
            }
            else
            {
                if (debug) {Debug.Log("Player is NOT in sight range");}
                playerInSightRange = false;
            }

            // Is Player in attack range?
            if (Vector3.Distance(transform.position, player.transform.position) < attackRange)
            {
                if (debug) {Debug.Log("Player is in attack range");}
                playerInAttackRange = true;
            }
            else
            {
                if (debug) {Debug.Log("Player is NOT in attack range");}
                playerInAttackRange = false;
            }        
            
            // Control States
            if (!playerInSightRange && !playerInAttackRange) Patroling();
            if (playerInSightRange && !playerInAttackRange) Patroling (); // No chasing for these guards
            if (playerInAttackRange && playerInSightRange) AttackPlayer();
        }
        else{
            if (debug) {Debug.Log("Guard is stuck in honey. No movement.");}
        }
    }


    // ================================
    //      Guard State Functions
    // ================================


    // ====== PATROL ======
    private void Patroling()
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
    }

    // ====== ATTACK ======
    private void AttackPlayer()
    {
        if (debug) {Debug.Log("Attacking Player... with words! Curse you!");}
        // Attack code
        Invoke("Attack", 0f);


        //transform.LookAt(player);

        if (!alreadyAttacked)
        {
            alreadyAttacked = true;
            Invoke("ResetAttack", attackCooldown);
        }
    }

    // ====== RESET ATTACK ABILITY ======
    private void ResetAttack()
    {
        if (debug) {Debug.Log("Resetting Attack");}
        alreadyAttacked = false;
    }

    // ====== CALL FUNCTIONS FOR MOVING DURING AN ATTACK ======
    private void Attack()
    {
        if (debug) {Debug.Log("Calling Attack()");}
        bool moved = false;
        AttackMovement(moved);
        AttackMovement(moved);
        moved = true;
        AttackMovement(moved);
        AttackMovement(moved);
    }

    // ====== MOVEMENT DURING AN ATTACK ======
    private void AttackMovement(bool moved)
    {
        if (debug) {Debug.Log("Calling AttackMovement()");}

        Vector3 currentPos; // Vector of current position
        currentPos = this.transform.position; // What is current position of the guard?

        if (moved)
        {
            // Guard has already moved forward for the attack, so move back to original position
            currentPos.z -= speed;
            currentPos.x -= speed;
        }
        else
        {
            // If guard hasn't started moving for the attack yet, move
            currentPos.z += speed;
            currentPos.x += speed;
        }

        this.transform.position = currentPos; // Apply new translation to object
    }

    // ====== GETTING STUCK IN HONEY ======
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == to a honey cube
        if (col.gameObject.tag == "honeyCube") {
            canMove = false;
            if (debug) {Debug.Log("Guard can't move. Stuck in Honey!");}
            
            Vector3 currentPos; // Vector of current position
            currentPos = this.transform.position; // get Guard's position

            // Put Guard INTO honey cube
            currentPos.x = col.gameObject.transform.position.x;
            currentPos.z = col.gameObject.transform.position.z;

            this.transform.position = currentPos; // Apply new translation to object
        }
    }














    // ====== TAKE DAMAGE ======
    // for player and baby bees later on
    public void TakeDamage(int damage) {
        if (debug) {Debug.Log("Guard is taking damage!");}
        health -= damage;

        if (health <= 0) {
            Destroy(gameObject);
        }
    }
}
