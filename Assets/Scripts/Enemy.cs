using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    public int playerDamage;
    // TODO: use this
    // private Animator animator;
    private Transform target;
    // use to move slowly
    private bool skipMove;

    // Use this for initialization
    protected override void Start()
    {
        // this may blow up?
        // animator = GetComponent<Animator>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
            Debug.Log("player not found");

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        MoveEnemy();
    }

    protected override void AttemptMove<T>(int xDir, int yDir)
    {
        // move every other turn
        if (skipMove)
        {
            skipMove = false;
            return;
        }

        base.AttemptMove<T>(xDir, yDir);

        skipMove = true;
    }

    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        // are we in the same column as our player?
        if (Mathf.Abs(target.position.x - transform.position.x) < base.detectionRadius)
        {
            Debug.Log("same column");
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            Debug.Log("different column");
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<PlayerController>(xDir, yDir);
    }

    protected override void OnCantMove<T>(T component)
    {
        PlayerController hitPlayer = component as PlayerController;
        Debug.Log("hit player");
    }
}
