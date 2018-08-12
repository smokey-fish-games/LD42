using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class GameManager : MonoBehaviour
{

    private BoardManager boardScript = new BoardManager();

    void Awake()
    {
        boardScript = GetComponent<BoardManager>();
        InitGame();
    }

    void InitGame()
    {
        Debug.Log("Starting Game");
        boardScript.SetupScene();
    }
}
