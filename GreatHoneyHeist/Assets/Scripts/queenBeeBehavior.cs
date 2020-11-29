using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class queenBeeBehavior : MonoBehaviour
{   
    // QUEEN BEE VARIABLES

    // Queen Bee Prefab
    public GameObject QueenPrefab;

    // Create internal variable in code to hold the prefab
    public GameObject queenBee;

    // Queen Bee Trigger
    public GameObject queenTrigger;

    // Start is called before the first frame update
    void Start()
    {
        spawnQueenBee(); // Spawns queen bee
        
    }
    // ====== SPAWN QUEEN BEE ======
    public void spawnQueenBee()
    {
        // Spawn baby bee swarm
        queenBee = Instantiate<GameObject>(QueenPrefab);

        Vector3 pos; // Vector to hold position
        pos = queenTrigger.transform.position;  // set vector to trigger position
        pos.y += 160f; // move up a little so visible above ground
        pos.x -= 1500f; //move to frame left
        pos.z -= 300f; //move down frame
        queenBee.transform.position = pos; // assign new translation to queen bee
    }
}
