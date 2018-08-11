using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    private List<Room> rooms = new List<Room>();

    public int num_rooms = 1;

    public void SetupScene () {
        for (int i = 0; i < num_rooms; i++) {
            Vector2 pos = new Vector2(Random.Range(-50, 50),Random.Range(-50, 50));
            IntVector2 size = new IntVector2(Random.Range(4, 10),Random.Range(6, 10));
            rooms.Add(new Room(pos, size));
        }
    }
}
