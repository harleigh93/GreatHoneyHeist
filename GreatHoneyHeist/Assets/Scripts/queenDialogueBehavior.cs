using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenDialogueBehavior : MonoBehaviour
{
    // Debugging
    private bool debug = true; // If print statements should be printed or not
    private bool inRoomOne;   // Is player in room1?
    private bool inRoomTwo;   // Is player in room1?
    private bool inRoomThree;   // Is player in room1?

    // QUEEN BEE VARIABLES

    // Queen Bee Prefab
    public GameObject textPrefab;

    // Create internal variable in code to hold the prefab
    public GameObject queenText;

    // Queen Bee Trigger
    public GameObject textTrigger;



    // Start is called before the first frame update
    void Start()
    {
        inRoomOne = false;
        inRoomTwo = false;
        inRoomThree = false;
    }
    private void OnTriggerEnter(Collider col)
    {
        // Check to see if tag on collider is == room1
        if (col.gameObject.tag == "room1")
        {
            if (debug) { Debug.Log("Player in room one"); }
            inRoomOne = true;
        }
        // Check to see if tag on collider is == room2
        if (col.gameObject.tag == "room2")
        {
            if (debug) { Debug.Log("Player in room two"); }
            inRoomTwo = true;
        }
        // Check to see if tag on collider is == room3
        if (col.gameObject.tag == "room3")
        {
            if (debug) { Debug.Log("Player in room two"); }
            inRoomThree = true;
        }
    }

//    public void spawnQueenText()
//    {
        //Instantiates the text object
//    GameObject queenText = (GameObject)Instantiate(textPrefab);

    //Grabs the TextMesh component from the game object
//    TextMesh entranceText = queenText.transform.getComponent<TextMesh>();

        //Sets the text according to room.
//        if (inRoomOne) { entranceText.text = "Room1 Text"; }
//        if (inRoomTwo) { entranceText.text = "Room2 Text"; }
//        if (inRoomThree) { entranceText.text = "Room3 Text"; }
//    }

}
