using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public SpriteController controller;
    public Collider2D col;
    public int health = 3;


    bool movedByPipe = false;

    //SetMove
    public void SetMove(bool move)
    {
        movedByPipe = move;
    }

    public bool GetMove()
    {
        return movedByPipe;
    }
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        controller.MOVEUP = false;
        controller.MOVEDOWN = false;
        controller.MOVELEFT = false;
        controller.MOVERIGHT = false;

        if (h > 0)
        {
            controller.MOVERIGHT = true;
            controller.MOVELEFT = false;
        }
        else if (h < 0)
        {
            controller.MOVERIGHT = false;
            controller.MOVELEFT = true;
        }
        if (v > 0)
        {
            controller.MOVEUP = true;
            controller.MOVEDOWN = false;

        }
        if (v < 0)
        {
            controller.MOVEUP = false;
            controller.MOVEDOWN = true;
        }
    }


    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("Player trigger Exit");
        if (other.tag == "Pipe")
        {
            movedByPipe = false;
        }
    }

    public void attackMe(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("You Died");
            // TODO: kill da wabbit
            // Destroy(gameObject);
        }
    }
}
