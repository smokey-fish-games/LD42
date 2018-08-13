using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSceneController : MonoBehaviour {

	// Update is called once per frame

	public int maxRabbits = 40;
	public int curRabbits = 0;

	void FixedUpdate () {
		SpawnController[] RabbitList = GetComponentsInChildren<SpawnController>();
		curRabbits = RabbitList.Length;

		if (curRabbits >= maxRabbits){
			foreach (SpawnController s in RabbitList){
				s.stopSpawning();
			}
		}

	}
}
