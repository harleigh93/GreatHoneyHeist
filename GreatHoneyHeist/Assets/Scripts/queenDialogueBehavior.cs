using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class queenDialogueBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not
    private bool inRoomOne;   // Is player in room1?
    private bool inRoomTwo;   // Is player in room1?
    private bool inRoomThree;   // Is player in room1?
    private bool atEnd;   // Is player in at the end?

    // QUEEN BEE VARIABLES

    // Queen Bee Text Prefab
    public GameObject textPrefab;

    // Create internal variable in code to hold the text prefab
    public GameObject queenText;

    // Queen Bee Prefab
    public GameObject queenPrefab;

    // Create internal variable in code to hold the Queen prefab
    public GameObject queenBee;

    // Queen Bee Trigger
    public GameObject queenTrigger;


    // Start is called before the first frame update
    void Start()
    {
        inRoomOne = false;
        inRoomTwo = false;
        inRoomThree = false;
        atEnd = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            // Check to see if tag on collider is == room1
            if (gameObject.tag == "room1")
            {
                if (debug) { Debug.Log("Player in room one"); }
                inRoomOne = true;
                spawnQueenText();
            }
            // Check to see if tag on collider is == room2
            if (gameObject.tag == "room2")
            {
                if (debug) { Debug.Log("Player in room two"); }
                inRoomTwo = true;
                spawnQueenText();
            }
            // Check to see if tag on collider is == room3
            if (gameObject.tag == "room3")
            {
                if (debug) { Debug.Log("Player in room three"); }
                inRoomThree = true;
                spawnQueenText();
            }
            // Check to see if tag on collider is == endGame
            if (gameObject.tag == "endGame")
            {
                if (debug) { Debug.Log("Player at end of game"); }
                inRoomThree = true;
                spawnQueenText();
            }
            Destroy(queenTrigger);
        }
    }

    // ====== SPAWN QUEEN BEE TEXT ======
    public void spawnQueenText()
    {
        //Instantiates the text object
        queenText = Instantiate<GameObject>(textPrefab);

        //Grabs the Text component from the game object
        Text entranceText = queenText.GetComponent<Image>().GetComponentInChildren<Text>();

        //Spawns Queen and sets the text according to room.
        if (inRoomOne) {
            spawnQueenBee();
            entranceText.text = "Room1 Text";            
        }
        if (inRoomTwo) {
            spawnQueenBee();
            entranceText.text = "Room2 Text";
        }
        if (inRoomThree) {
            spawnQueenBee();
            entranceText.text = "Room3 Text";
        }
        if (atEnd) {
            spawnQueenBee();
            entranceText.text = "Darn, you did it";
        }
    }
    
    // ====== SPAWN QUEEN BEE ======
    public void spawnQueenBee()
    {
        // Spawn queen bee 
        queenBee = Instantiate<GameObject>(queenPrefab);

        Vector3 pos; // Vector to hold position
        pos = queenTrigger.transform.position;  // set vector to trigger position
        pos.y += 750f; // move up a little so visible above ground
        pos.x -= 1650f; //move to frame left
        pos.z -= 700f; //move down frame
        queenBee.transform.position = pos; // assign new translation to queen bee
    }

}
