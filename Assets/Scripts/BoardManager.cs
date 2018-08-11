using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

using System.Linq;

public class BoardManager : MonoBehaviour {

    private List<GameObject> rooms = new List<GameObject>();

    public int num_rooms = 1;

    public void SetupScene () {

        Object room = Resources.Load("Room");

        Debug.Log("Will create rooms: " + num_rooms);

        Vector2 pos = new Vector2(0,0);

        for (int i = 0; i < num_rooms; i++) {
            Debug.Log("Creating room: " + i);

            bool freeSpaceFound = false;

            //Vector2 pos = new Vector2(Random.Range(-50, 50),Random.Range(-150, 150));
            IntVector2 size = new IntVector2(Random.Range(3, 10),Random.Range(3, 10));



            // while (!freeSpaceFound) {

            //     pos = new Vector2(Random.Range(-50, 50),Random.Range(-150, 150));
            //     size = new IntVector2(Random.Range(3, 10),Random.Range(3, 10));

            //     foreach (GameObject g in rooms) {
            //         Room room1 = g.GetComponent(typeof(Room)) as Room;

            //         if (room1.roomsIntersect(pos, size)) {
            //             freeSpaceFound = false;
            //             break;
            //         }
            //         else 
            //         {
            //             freeSpaceFound = true;
            //             break;
            //         }
            //     }
                
            // }

            GameObject go = Instantiate(room) as GameObject;

            Room r = go.GetComponent(typeof(Room)) as Room;
            r.room_position = pos;
            r.room_size = size;
            r.initBoard();
            rooms.Add(go);

            pos.x += r.getFullSize().x + 2;

        }

        for (int i = 0; i < rooms.Count; i++)
        {
            Debug.Log("Init pipes: " + i);
            Room r = rooms.ElementAt(i).GetComponent(typeof(Room)) as Room;
            r.initPipes();
        }

        for (int i = 0; i < (rooms.Count - 1); i++)
        {
            Debug.Log("Connct pipes: " + i + " " + (i+1));
            Room r = rooms.ElementAt(i).GetComponent(typeof(Room)) as Room;
            Room r2 = rooms.ElementAt(i+1).GetComponent(typeof(Room)) as Room;
            connect_rooms(r, r2);
        }

        
        Room first_room = rooms.ElementAt(0).GetComponent(typeof(Room)) as Room;
        Room last_room = rooms.ElementAt(rooms.Count-1).GetComponent(typeof(Room)) as Room;
        connect_rooms(last_room, first_room);
    }

    private void connect_rooms(Room r1, Room r2)
    {
        r1.connectOutPipe(r2.getInLocation());
        r2.connectInPipe(r1.getOutLocation());      
    }
}
