using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{

    public enum DIRECTION { UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT };
	
	public bool MOVEUP = false;
    public bool MOVEDOWN = false;
    public bool MOVELEFT = false;
    public bool MOVERIGHT = false;
    public float speed = 0.5f;

    public void Move(DIRECTION direction)
    {
        Vector2 currentPos = transform.position;
        Vector2 newpos = new Vector2();
        float dirMove = (speed * Time.deltaTime) / 2;
        switch (direction)
        {
            case DIRECTION.UP:
                Debug.Log("Moving up!");
                newpos = currentPos + new Vector2(0, speed * Time.deltaTime);
                break;
            case DIRECTION.DOWN:
                Debug.Log("Moving Down!");
                newpos = currentPos + new Vector2(0, -speed * Time.deltaTime);
                break;
            case DIRECTION.LEFT:
                Debug.Log("Moving Left!");
                newpos = currentPos + new Vector2(-speed * Time.deltaTime, 0);
                break;
            case DIRECTION.RIGHT:
                Debug.Log("Moving Right!");
                newpos = currentPos + new Vector2(speed * Time.deltaTime, 0);
                break;

            case DIRECTION.UPLEFT:
                Debug.Log("Moving up Left!");
                newpos = currentPos + new Vector2(-dirMove, dirMove);
                break;
            case DIRECTION.UPRIGHT:
                Debug.Log("Moving Up Right!");
                newpos = currentPos + new Vector2(dirMove, dirMove);
                break;
            case DIRECTION.DOWNLEFT:
                Debug.Log("Moving Down Left!");
                newpos = currentPos + new Vector2(-dirMove, -dirMove);
                break;
            case DIRECTION.DOWNRIGHT:
                Debug.Log("Moving Down Right!");
                newpos = currentPos + new Vector2(dirMove, -dirMove);
                break;

            default:
                Debug.Log("TODO " + direction);
                break;
        }
        transform.position = newpos;
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if (MOVEUP && MOVELEFT)
        {
            Move(DIRECTION.UPLEFT);
        }
        else if (MOVEUP && MOVERIGHT)
        {
            Move(DIRECTION.UPRIGHT);
        }
        else if (MOVEDOWN && MOVELEFT)
        {
            Move(DIRECTION.DOWNLEFT);
        }
        else if (MOVEDOWN && MOVERIGHT)
        {
            Move(DIRECTION.DOWNRIGHT);
        }
        else if (MOVEDOWN)
        {
            Move(DIRECTION.DOWN);
        }
        else if (MOVEUP)
        {
            Move(DIRECTION.UP);
        }
        else if (MOVELEFT)
        {
            Move(DIRECTION.LEFT);
        }
        else if (MOVERIGHT)
        {
            Move(DIRECTION.RIGHT);
        }
        else
        {
            Debug.Log("What?");
        }
    }
}
