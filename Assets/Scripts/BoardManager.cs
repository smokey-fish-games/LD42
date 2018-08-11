using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour
{

    private List<GameObject> rooms = new List<GameObject>();

    public int num_rooms = 1;
    public GameObject player;

    public void SetupScene()
    {

        Object room = Resources.Load("Room");

        Debug.Log("Will create rooms: " + num_rooms);
        for (int i = 0; i < num_rooms; i++)
        {
            Debug.Log("Creating room: " + i);
            Vector2 pos = new Vector2(Random.Range(-50, 50), Random.Range(-50, 50));
            IntVector2 size = new IntVector2(Random.Range(4, 10), Random.Range(6, 10));
            //rooms.Add(new Room(pos, size));

            GameObject go = Instantiate(room) as GameObject;
            go.layer = 12;

            Room r = go.GetComponent(typeof(Room)) as Room;
            r.room_position = pos;
            r.room_size = size;
            if (i == 0)
            {
                r.startRoom = true;
                r.player = player;
            }
            r.initBoard();
        }
    }
}
