using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{

	public float moveIntMax = 0.5f;
	public float moveIntMin = 0.2f;

	public GameObject spawner;

	public float stopMax = 5f;
	public float stopMin = 2f;

	public float speed = 5f;

	public float splitMin = 5f;
	public float splitMax = 10f;

	bool moving = false;
	SpriteController.DIRECTION dirMoving;
	SpriteController sp;
	Animator ani;

	float moveFor = 0f;
	float split = 10f;

	bool stop = false;


	private void Start()
	{
		sp = GetComponent<SpriteController>();
		sp.speed = speed;
		ani = GetComponent<Animator>();
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		// move around a bit?
		if (!stop)
		{
			shouldDoMove();
			// Split?
			shouldSplit();

			moveFor -= Time.deltaTime;
			split -= Time.deltaTime;
		}
	}

	void shouldDoMove()
	{
		if (moveFor <= 0)
		{
			moving = !moving;
			if (moving)
			{
				moveFor = Random.Range(moveIntMin, moveIntMax);
			}
			else
			{
				moveFor = Random.Range(stopMin, stopMax);
			}
			dirMoving = (SpriteController.DIRECTION)Random.Range(0, 7);
			if (ani != null)
			{
				ani.SetBool("moving", moving);
			}
		}

		if (moving)
		{
			//move
			sp.Move(dirMoving);
		}
	}

	void shouldSplit()
	{
		if (split <= 0)
		{
			split = Random.Range(splitMin, splitMax);
			GameObject newSpawn = Instantiate(spawner, transform.position, transform.rotation);
			newSpawn.layer = 0;
			newSpawn.transform.parent = transform.parent;
		}
	}

	public void stopSpawning()
	{
		stop = true;
	}
}
