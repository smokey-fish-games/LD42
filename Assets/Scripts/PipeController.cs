using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour {

	public Transform destination;

	void OnTriggerEnter2D(Collider2D other) {
        Debug.Log("PIPE!");
    }
}
