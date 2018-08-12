using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

	public float moveIntMax = 0.5f;
	public float moveIntMin = 0.2f;

	public float stopMax = 5f;
	public float stopMin = 2f;

	public float speed = 5f;

	bool moving = false;
	SpriteController.DIRECTION dirMoving;
	SpriteController sp;
	Animator ani;

	float moveFor = 0f;


	private void Start() {
		sp = GetComponent<SpriteController>();
		sp.speed = speed;
		ani = GetComponent<Animator>();
	}

	// Update is called once per frame
		void Update () {
		// move around a bit?
		shouldDoMove();
		// Split?
		shouldSplit();

		moveFor -= Time.deltaTime;
	}

	void shouldDoMove(){
		if (moveFor <= 0) {
			moving = !moving;
			if (moving) {
				moveFor = Random.Range(moveIntMin, moveIntMax);
			}	else {
				moveFor = Random.Range(stopMin, stopMax);
			}
			dirMoving = (SpriteController.DIRECTION)Random.Range(0, 7);
			string message;
			message = moving ? "Moving for " + moveFor + " in direction " + dirMoving : "Staying still for " + moveFor;
			Debug.Log(message);
			if (ani != null){
            	ani.SetBool("moving", moving);
        	}
		}

		if(moving){
			//move
			sp.Move(dirMoving);
		}
	}

	void shouldSplit(){

	}
}
