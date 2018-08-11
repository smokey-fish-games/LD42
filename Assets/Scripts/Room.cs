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

    private UnityEngine.Object pipe_resource;

    public GameObject input_pipe;
    public GameObject output_pipe;

    private Bounds bounds;

    private Transform in_location;

    public Room(Vector2 position, IntVector2 size){ 
        this.room_position = position;
        this.room_size = size;

        initBoard();
    }

    public void Start() {
        initBoard();
    }

    public Vector2 getFullSize() {
        return new Vector2((this.room_size.X + 2) * bounds.size.x, (this.room_size.Y + 2) * bounds.size.y);
    }

    public void initBoard() 
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < room_size.X +1; x++) {
            for (int y = -1; y < room_size.Y +1; y++) {
                
                GameObject toInit;
                
                if (x == -1 || x == room_size.X || y == -1 || y == room_size.Y) 
                {
                    //Debug.Log("Select wal");                               
                    toInit = wallTiles[Random.Range(0, wallTiles.Length)];
                }
                else {
                    //Debug.Log("Select floor");
                    toInit = floorTiles[Random.Range(0, floorTiles.Length)];
                }


                bounds = toInit.GetComponent<Renderer>().bounds;

                float posx = room_position.x + (bounds.size.x * x);
                float posy = room_position.y + (bounds.size.y * y);

                GameObject instance = Instantiate(toInit, new Vector3(posx, posy, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }        
    }

    public bool roomsIntersect(Vector2 other_position, IntVector2 other_size) {
        //  return (abs(a.x - b.x) * 2 < (a.width + b.width)) &&
        //  (abs(a.y - b.y) * 2 < (a.height + b.height));
        //

        return ((Math.Abs(room_position.x - other_position.x) * 2) < ((room_size.X * bounds.size.x) + (other_size.X * bounds.size.x))) &&
        ((Math.Abs(room_position.y - other_position.y) * 2) < ((room_size.Y * bounds.size.y) + (other_size.Y * bounds.size.y)));

        
    }

    public Transform getInLocation() 
    {
        return input_pipe.transform;
    }
    
    public Transform getOutLocation() {
        return output_pipe.transform;
    }
    public void initPipes() {

        pipe_resource = Resources.Load("Pipe");

        Vector3 pos_in = new Vector3(room_position.x + bounds.size.x, room_position.y, 0f);
        Vector3 pos_out = new Vector3(room_position.x + (bounds.size.x * (room_size.X - 2)), room_position.y + (bounds.size.y * 2));

        input_pipe = Instantiate(pipe_resource, pos_in, Quaternion.identity) as GameObject;
        output_pipe = Instantiate(pipe_resource, pos_out, Quaternion.identity) as GameObject;

        input_pipe.transform.SetParent(boardHolder);
        output_pipe.transform.SetParent(boardHolder);
    }

    public void connectInPipe (Transform destination) {
        PipeController pc = input_pipe.GetComponent(typeof(PipeController)) as PipeController;

        pc.destination = destination;
    }

    public void connectOutPipe (Transform destination) {
        PipeController pc = output_pipe.GetComponent(typeof(PipeController)) as PipeController;

        pc.destination = destination;
    }
}
