﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour
{

    public float moveTime = 2f;
    public float detectionRadius = 0.3f;
    public LayerMask blockingLayer;

    private Collider2D myCollider;
    private Rigidbody2D rb2D;

    // Use this for initialization
    protected virtual void Start()
    {
        myCollider = GetComponent<Collider2D>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // 'out' means pass by reference so we can return multiple values
    // Move will move xDir, yDir and return true or false depending on
    // whether we could move. Hit represents the line we tried to move
    // along
    protected bool Move(int xDir, int yDir, out RaycastHit2D hit)
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(xDir, yDir);

        // disable collider so we don't hit it whilst looking out
        myCollider.enabled = false;
        // check for collisions in a straight line between start and end
        // on blockingLayer
        hit = Physics2D.Linecast(start, end, blockingLayer);
        myCollider.enabled = true;

        if (hit.transform == null)
        {
            StartCoroutine(SmoothMovement(end));
            return true;
        }
        return false;

    }

    protected IEnumerator SmoothMovement(Vector3 end)
    {
        // sqrMagnitude is cheaper to calculate apparently
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        while (sqrRemainingDistance - detectionRadius > float.Epsilon)
        {
            Vector3 newPosition = Vector3.MoveTowards(rb2D.position, end, moveTime * Time.deltaTime);
            rb2D.MovePosition(newPosition);
            // recalculate remaining distance
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;
            // wait for the next frame before iterating again
            yield return null;
        }
    }

    protected virtual void AttemptMove<T>(int xDir, int yDir)
        where T : Component
    {
        RaycastHit2D hit;
        bool canMove = Move(xDir, yDir, out hit);

        if (hit.transform == null)
            return;

        // we don't know what type of hit component we're going to get
        // so keep this generic
        T hitComponent = hit.transform.GetComponent<T>();

        if (!canMove && hitComponent != null)
            OnCantMove(hitComponent);
    }

    protected abstract void OnCantMove<T>(T component)
        where T : Component; // what is this language????
}