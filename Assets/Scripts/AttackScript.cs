using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("I AM ALIVE)");
		Animator a = this.GetComponent<Animator>();
		a.Play("SwipeAttack");
		Destroy(gameObject, a.GetCurrentAnimatorStateInfo(0).length);
	}
}
