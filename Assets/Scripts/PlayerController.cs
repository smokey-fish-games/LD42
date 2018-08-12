using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public SpriteController controller;
    public Collider2D col;
    public int health = 3;
    public AttackController attack;
    public float cooldown = 1f;
    public float attackCountdown = 0;
    public Image img;


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
        controller.MOVEUP = false;
        controller.MOVEDOWN = false;
        controller.MOVELEFT = false;
        controller.MOVERIGHT = false;
        if (Input.GetMouseButtonDown(0))
        {
            if (attackCountdown <= 0)
            {
                attack.attemptAttack(Input.mousePosition);
                attackCountdown = cooldown;
                return;
            }
        }
        attackCountdown -= Time.deltaTime;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

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
        StartCoroutine(FadeImage());
        if (health <= 0)
        {
            // Debug.Log("You Died");
            // TODO: kill da wabbit
            // Destroy(gameObject);
        }
    }

    IEnumerator FadeImage()
    {
        // fade from opaque to transparent
        // loop over 1 second backwards
        for (float i = 0.8f; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            img.color = new Color(0.4f, 0, 0, i);
            yield return null;
        }
    }
}
