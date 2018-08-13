using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{

	public Transform destination;
	public bool isActive = true;
	bool locked = true;
	Collider2D col;
	public Room parent;
	public Room destination_room;

	Animator ani;

	private void Awake()
	{
		col = gameObject.GetComponent<Collider2D>();
		ani = GetComponent<Animator>();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Player" && isActive && !locked)
		{
			PlayerController pc = (PlayerController)other.GetComponent("PlayerController");
			if (pc == null)
			{
				Debug.Log("WHAT?");
			}
			else
			{
				if (!pc.GetMove())
				{
					pc.SetMove(true);
					col.enabled = false;
					other.transform.position = destination.position;

					parent.setEnabled(false);
					destination_room.setEnabled(true);
					destination_room.updateUI();
				}
				else
				{
					Debug.Log("Cannot move as it just arrived from another pipe");
				}
			}

		}
	}

	public void setActive(bool active, bool notifypartner)
	{
		isActive = active;
		PipeController OtherPipe = destination.GetComponent<PipeController>();
		if (notifypartner)
		{
			OtherPipe.setActive(isActive, false);
		}
		ani.SetBool("active", active);
	}

	public void setLocked(bool lockB)
	{
		locked = lockB;
		if (ani != null)
		{
			ani.SetBool("locked", locked);
		}
	}

	void FixedUpdate()
	{
		if (!col.enabled)
		{
			col.enabled = true;
		}
	}
}
