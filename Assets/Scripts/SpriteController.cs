using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteController : MonoBehaviour
{

    public enum DIRECTION { UP, DOWN, LEFT, RIGHT, UPLEFT, UPRIGHT, DOWNLEFT, DOWNRIGHT };
	
    public bool MOVEUP = false;
    public bool MOVEDOWN = false;
    public bool MOVELEFT = false;
    public bool MOVERIGHT = false;
    public float speed = 10f;

    bool moving = false;

    Animator ani;
    private void Start() {
        ani = GetComponent<Animator>();
    }
    public void Move(DIRECTION direction)
    {
        moving = true;
        Vector2 newpos = new Vector2();
        switch (direction)
        {
            case DIRECTION.UP:
                newpos = Vector2.up;
                break;
            case DIRECTION.DOWN:
                newpos = Vector2.down;
                break;
            case DIRECTION.LEFT:
                newpos = Vector2.left;
                break;
            case DIRECTION.RIGHT:
                newpos = Vector2.right;
                break;
            case DIRECTION.UPLEFT:
                newpos = Vector2.up + Vector2.left;
                break;
            case DIRECTION.UPRIGHT:
                newpos = Vector2.up + Vector2.right;
                break;
            case DIRECTION.DOWNLEFT:
                newpos = Vector2.down + Vector2.left;
                break;
            case DIRECTION.DOWNRIGHT:
                newpos = Vector2.down + Vector2.right;
                break;
            default:
                Debug.Log("TODO " + direction);
                break;
        }
        transform.Translate(newpos * Time.deltaTime * speed);
    }


    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        moving = false;
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
        if (ani != null){
            ani.SetBool("moving", moving);
        }
    }
}
