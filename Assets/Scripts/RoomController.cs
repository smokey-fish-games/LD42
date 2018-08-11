﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomController : MonoBehaviour {

	public Transform slocation;
	public GameObject rabbit;
	public float interval = 40;
	public float curTimer = 0;

	public float MaxCapacity = 20;

	public Slider slider;
	public Text text;

	float CurRabbits = 1;

	private void FixedUpdate() {
		//hello
		curTimer += Time.deltaTime;
		if (curTimer >= interval) {
			SpawnRabbit();
			curTimer = 0;
		}

		checkLose();
	}	



	void SpawnRabbit(){
		CurRabbits++;

		Instantiate(rabbit, slocation.position, slocation.rotation);
		slider.value = CurRabbits / MaxCapacity;
		text.text = "" + Mathf.Round((CurRabbits / MaxCapacity) * 100)  + "%";



	}

	void checkLose(){
		if (CurRabbits >= MaxCapacity) {
			// LOSE
			Debug.Log("YOU LOSE SUCKER!");
			Application.Quit();

		}
	}

	void DisableMe(){

	}
}