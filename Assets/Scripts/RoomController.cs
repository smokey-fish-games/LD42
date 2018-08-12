using System.Collections;
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

	float CurRabbits = 0;

	public Animator ani;

	private void FixedUpdate() {
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
		float perc = Mathf.Round((CurRabbits / MaxCapacity) * 100);
		text.text = "" + perc  + "%";
		if (perc >= 75) {
			// Set animator alert
			ani.SetBool("Alarm", true);
		} else {
			// Set animator to not alert
			ani.SetBool("Alarm", false);
		}
	}

	void checkLose(){
		if (CurRabbits >= MaxCapacity) {
			DisableMe();
			// LOSE
			Debug.Log("YOU LOSE SUCKER!");
			Application.Quit();

		}
	}

	void DisableMe(){
		foreach (PipeController p in gameObject.GetComponentsInChildren<PipeController>()) {
				p.setActive(false, true);
		}
	}
}
