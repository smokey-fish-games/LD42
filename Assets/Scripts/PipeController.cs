using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

    public Transform destination;
    public bool isActive = true;
    Collider2D col;
    SpriteRenderer sp;

    private void Start() {
        col = gameObject.GetComponent<Collider2D>();
        sp = gameObject.GetComponent<SpriteRenderer>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && isActive)
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

    public void setActive(bool active, bool notifypartner) {
        isActive = active;
        PipeController OtherPipe = destination.GetComponent<PipeController>();
        if (notifypartner){
            OtherPipe.setActive(isActive, false);
        }
        if (isActive) {
            sp.color = Color.white;
        } else {
            sp.color = Color.red;
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
