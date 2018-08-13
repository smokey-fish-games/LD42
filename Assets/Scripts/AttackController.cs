using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
	private Vector2 mousePos;
	public float attackRange = 1f;

	public LayerMask blockingLayer;

	public Animator attackAnimator;
	void Awake()
	{
		attackAnimator = GetComponent<Animator>();
	}


	public void attemptAttack(Vector3 position)
	{
		mousePos = (Vector2)Camera.main.ScreenToWorldPoint(position);
		attackEnemy();
		return;

	}

	public void attackEnemy()
	{
		Vector2 start = transform.position;

		attackAnimator.SetTrigger("Active");
		if ((start - mousePos).sqrMagnitude > attackRange)
		{
			Debug.Log("attack out of range");
			return;
		}

		Vector2 end = mousePos;

		// disable collider so we don't hit it whilst looking out
		Collider2D col2d = GetComponentInParent<Collider2D>();
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
		Debug.Log("enemy found");

		Enemy enemy = hit.transform.GetComponent<Enemy>();
		if (enemy != null)
		{
			enemy.takeDamage(1);
		}
	}
}
;