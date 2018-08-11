using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour {

    private BoardManager boardScript = new BoardManager();

    private int level = 1;

    void Awake() {
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame() {
        boardScript.SetupScene();
    }
}
