using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour {

    public Vector2 room_position = new Vector2(0f, 0f);
    public IntVector2 room_size = new IntVector2(8, 8);

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;

    private Transform boardHolder;

    public Room(Vector2 position, IntVector2 size){ 
        this.room_position = position;
        this.room_size = size;

        initBoard();
    }

    public void Start() {
        initBoard();
    }

    private void initBoard() 
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < room_size.X +1; x++) {
            for (int y = -1; y < room_size.Y +1; y++) {
                
                GameObject toInit;
                
                if (x == -1 || x == room_size.X || y == -1 || y == room_size.Y) 
                {
                    Debug.Log("Select wal");                               
                    toInit = wallTiles[Random.Range(0, wallTiles.Length)];
                }
                else {
                    Debug.Log("Select floor");
                    toInit = floorTiles[Random.Range(0, floorTiles.Length)];
                }


                Bounds bounds = toInit.GetComponent<Renderer>().bounds;

                float posx = room_position.x + (bounds.size.x * x);
                float posy = room_position.y + (bounds.size.y * y);

                GameObject instance = Instantiate(toInit, new Vector3(posx, posy, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }        
    }
}
