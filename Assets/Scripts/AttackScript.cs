using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour
{

    private Vector2 mousePos;
    public float attackRange = 1f;
    public LayerMask blockingLayer;

    // Use this for initialization
    void Start()
    {
        Debug.Log("I AM ALIVE)");
        Animator a = this.GetComponent<Animator>();
        a.Play("SwipeAttack");
        Destroy(gameObject, a.GetCurrentAnimatorStateInfo(0).length);
    }

    public void SetMousePosition(Vector3 mousePosition)
    {
        mousePos = mousePosition;
    }

    public void attackEnemy()
    {
        Vector2 start = transform.position;

        if (transform.position.magnitude > attackRange)
        {
            Debug.Log("attack out of range");
            return;
        }

        Vector2 end = start + mousePos;

        // disable collider so we don't hit it whilst looking out
        Collider2D col2d = GetComponent<Collider2D>();
        col2d.enabled = false;
        // check for collisions in a straight line between start and end
        // on blockingLayer
        RaycastHit2D hit = Physics2D.Linecast(start, end, blockingLayer);
        col2d.enabled = true;

        if (hit.transform == null)
        {
            Debug.Log("no enemies");
            return;
        }
        Enemy enemy = hit.transform.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.takeDamage(1);
        }
    }
}
