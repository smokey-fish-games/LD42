using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class IntVector2 {
        public int X;
        public int Y;

        public IntVector2(int x, int y) {
            X = x;
            Y = y;
        }
    }
    public IntVector2 level_size = new IntVector2(8, 8);

    public GameObject[] floorTiles;

    private Transform boardHolder;

    private List <Vector3> gridPositions = new List <Vector3> ();

    void initList() {
        gridPositions.Clear();

        for (int x = 0; x < level_size.X; x++) {
            for (int y = 0; y < level_size.Y; y++) {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void initBoard() 
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < level_size.X; x++) {
            for (int y = 0; y < level_size.Y; y++) {
                GameObject toInit = floorTiles[Random.Range(0, floorTiles.Length)];

                Bounds bounds = toInit.GetComponent<Renderer>().bounds;

                float posx = (bounds.size.x * x);
                float posy = (bounds.size.y * y);

                GameObject instance = Instantiate(toInit, new Vector3(posx, posy, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }        
    }

    public void SetupScene (int level) {
        initBoard();
        initList();
    }
}
