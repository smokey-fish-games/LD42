using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MovingObject
{
    private Transform target;
    public int health = 1;
    public int cooldown = 2;
    private float attackCountdown;
    protected override void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
        if (target == null)
            Debug.Log("player not found");
        base.Start();
    }


    void Update()
    {
        MoveEnemy();
        attackCountdown -= Time.deltaTime;
    }

    // MoveEnemy tries to move the current enemy sprite towards
    // the player game object (target)
    public void MoveEnemy()
    {
        int xDir = 0;
        int yDir = 0;

        // are we in the same column as our player?
        if (Mathf.Abs(target.position.x - transform.position.x) - base.detectionRadius < float.Epsilon)
        {
            yDir = target.position.y > transform.position.y ? 1 : -1;
        }
        else
        {
            xDir = target.position.x > transform.position.x ? 1 : -1;
        }

        AttemptMove<PlayerController>(xDir, yDir);
    }

    // AttemptMove checks whether the player is close to the enemy, and if so
    // does not move but attacks instead
    protected override void AttemptMove<T>(int xDir, int yDir)
    {

        base.AttemptMove<T>(xDir, yDir);
    }

    // OnCantMove is called from AttemptMove and attacks the player if it
    // is encountered
    protected override void OnCantMove<T>(T component)
    {
        if (attackCountdown > 0)
            return;
        PlayerController hitPlayer = component as PlayerController;
        Debug.Log("attack player");
        hitPlayer.attackMe(1);
        // TODO: attack animation
        attackCountdown = cooldown;
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        if (health == 0)
        {
            Debug.Log("enemy destroyed");
            Animator a = GetComponent<Animator>();
            a.SetTrigger("destroy");
            return;
        }
    }

    public void OnDeath()
    {
        Destroy(gameObject);
    }

}