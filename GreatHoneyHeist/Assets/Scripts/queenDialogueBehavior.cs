using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

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
    public GameObject QueenPrefab;

    // Create internal variable in code to hold the Queen prefab
    public GameObject queenBee;

    // Queen Bee Trigger
    public GameObject queenTrigger;


    // Start is called before the first frame update
    void Start()
    {
        inRoomOne = true;//
        spawnQueenText();
        spawnQueenBee();
        inRoomTwo = false;
        inRoomThree = false;
        atEnd = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == room1
        if (col.gameObject.tag == "room1")
        {
            if (debug) { Debug.Log("Player in room one"); }
            inRoomOne = true;
            spawnQueenText();
        }
        // Check to see if tag on collider is == room2
        if (col.gameObject.tag == "room2")
        {
            if (debug) { Debug.Log("Player in room two"); }
            inRoomTwo = true;
            spawnQueenText();
        }
        // Check to see if tag on collider is == room3
        if (col.gameObject.tag == "room3")
        {
            if (debug) { Debug.Log("Player in room three"); }
            inRoomThree = true;
            spawnQueenText();
        }
        // Check to see if tag on collider is == endGame
        if (col.gameObject.tag == "endGame")
        {
            if (debug) { Debug.Log("Player at end of game"); }
            inRoomThree = true;
            spawnQueenText();
        }
    }

    public void spawnQueenText()
    {
        //Instantiates the text object
    GameObject queenText = (GameObject)Instantiate(textPrefab);

    //Grabs the TextMesh component from the game object
    Text entranceText = queenText.GetComponent<Text>();

        //Spawns Queen and sets the text according to room.
        if (inRoomOne) {
            entranceText.text = "Room1 Text";
            spawnQueenBee();
        }
        if (inRoomTwo) {
            entranceText.text = "Room2 Text";
            spawnQueenBee();
        }
        if (inRoomThree) {
            entranceText.text = "Room3 Text";
            spawnQueenBee();
        }
        if (atEnd) {
            entranceText.text = "Darn, you did it";
            spawnQueenBee();
        }
    }
    
    // ====== SPAWN QUEEN BEE ======
    public void spawnQueenBee()
    {
        // Spawn baby bee swarm
        queenBee = Instantiate<GameObject>(QueenPrefab);

        Vector3 pos; // Vector to hold position
        pos = queenTrigger.transform.position;  // set vector to trigger position
        pos.y += 160f; // move up a little so visible above ground
        pos.x -= 1750f; //move to frame left
        pos.z -= 300f; //move down frame
        queenBee.transform.position = pos; // assign new translation to queen bee
    }

}
