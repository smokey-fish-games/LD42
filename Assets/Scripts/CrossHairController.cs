using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossHairController : MonoBehaviour {

	public float radius = 1f;

	public float cooldown = 4f;
	float curCool = 0.0f;

	public GameObject attack;

	Transform rabbit;
	void Update () {
		moveCrosshair();
		checkAttack();
	}

	void checkAttack(){
		if(Input.GetMouseButtonDown(0) && curCool <= 0){
			Debug.Log("Attacking");
			Instantiate(attack, transform.position, Quaternion.identity);
			curCool = cooldown;
			return;
		}
		if (curCool > 0) {
			curCool -= Time.deltaTime;
		}
	}

	void moveCrosshair(){
		rabbit = GetComponentInParent<Transform>();
		Vector3 mouse_pos = Input.mousePosition;
    	mouse_pos.z = radius;
    	Vector3 object_pos = Camera.main.WorldToScreenPoint(rabbit.position);
		mouse_pos.x = mouse_pos.x - object_pos.x;
		mouse_pos.y = mouse_pos.y - object_pos.y;
		float angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler( new Vector3(0, 0, angle));
	}
		
}
