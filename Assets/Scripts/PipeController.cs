using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

    public Transform destination;
    public Collider2D col;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            PlayerController pc = (PlayerController)other.GetComponent("PlayerController");
            if (pc == null)
            {
                Debug.Log("WHAT?");

            }
            else
            {
                if (!pc.GetMove())
                {
                    pc.SetMove(true);
                    col.enabled = false;
                    other.transform.position = destination.position;
                }
                else
                {
                    Debug.Log("Cannot move as it just arrived from another pipe");
                }
            }

        }
    }

    void FixedUpdate()
    {
        if (!col.enabled)
        {
            col.enabled = true;
        }
    }
}
