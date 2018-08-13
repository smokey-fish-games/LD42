using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

using UnityEngine.UI;

using System.Linq;

public class BoardManager : MonoBehaviour
{

	private List<GameObject> rooms = new List<GameObject>();


	public int num_rooms = 1;
	public GameObject player;
	public GameObject rabbit;

	public Slider slider;
	public Text text;
	public Animator ani;

	bool stopScore = false;
	public Text score;
	public Text finalScore;
	public GameObject BSOD;

	int CurScore = 0;

	List<Room> allRooms;

	public float interval = 1;
	public float intervalMax = 1.5f;
	public float intervalMin = 0.5f;

	public int minEnemy = 0;
	public int maxEnemy = 2;

	public void SetupScene()
	{
		allRooms = new List<Room>();

		Object room = Resources.Load("Room");

		Debug.Log("Will create rooms: " + num_rooms);

		Vector2 pos = new Vector2(0, 0);

		for (int i = 0; i < num_rooms; i++)
		{
			Debug.Log("Creating room: " + i);

			IntVector2 size = new IntVector2(Random.Range(3, 10), Random.Range(3, 10));

			GameObject go = Instantiate(room) as GameObject;
			go.layer = 12;
			go.transform.parent = transform.parent;

			Room r = go.GetComponent(typeof(Room)) as Room;
			allRooms.Add(r);
			r.transform.parent = transform.parent;
			r.room_position = pos;
			r.room_size = size;
			r.level = i;
			if (i == 0)
			{
				r.player = player;
			}
			r.rabbit = this.rabbit;
			r.player = this.player;
			r.interval = Random.Range(intervalMin, intervalMax);
			r.BSOD = BSOD;

			r.slider = this.slider;
			r.text = this.text;
			// WHY CHRIS
			//https://youtu.be/DDGpwuMAmVs?t=24s
			r.ani = this.ani;

			r.MaxCapacity = Mathf.Round(10 + (50 * (intervalMax - r.interval)));

			r.IDSNum = Random.Range(minEnemy + i, maxEnemy + i);
			r.FIREWALLNum = Random.Range(minEnemy + i, maxEnemy + i);
			r.ANTIVIRUSNum = Random.Range(minEnemy + i, maxEnemy + i);

			r.initBoard();
			rooms.Add(go);

			pos.x += r.getFullSize().x + 2;
		}

		for (int i = 0; i < rooms.Count; i++)
		{
			Debug.Log("Init pipes: " + i);
			Room r = rooms.ElementAt(i).GetComponent(typeof(Room)) as Room;
			r.initPipes();
		}

		for (int i = 0; i < (rooms.Count - 1); i++)
		{
			Debug.Log("Connct pipes: " + i + " " + (i + 1));
			Room r = rooms.ElementAt(i).GetComponent(typeof(Room)) as Room;
			Room r2 = rooms.ElementAt(i + 1).GetComponent(typeof(Room)) as Room;
			connect_rooms(r, r2);
		}


		Room first_room = rooms.ElementAt(0).GetComponent(typeof(Room)) as Room;
		Room last_room = rooms.ElementAt(rooms.Count - 1).GetComponent(typeof(Room)) as Room;
		connect_rooms(last_room, first_room);
	}

	private void connect_rooms(Room r1, Room r2)
	{
		r1.connectOutPipe(r2);
		r2.connectInPipe(r1);
	}

	/// <summary>
	/// Update is called every frame, if the MonoBehaviour is enabled.
	/// </summary>
	void Update()
	{
		if (allRooms != null && !stopScore)
		{
			int countScore = 0;
			foreach (Room r in allRooms)
			{
				// count da rabbits
				countScore += r.GetComponentsInChildren<SpawnController>().Length;

				// check for dead rooms
				if (r.killed)
				{
					countScore += 100;
				}
			}

			// got the score... update!
			CurScore = countScore;
		}

		score.text = "" + CurScore;
		finalScore.text = "(" + CurScore + ")";
	}

	public void StopScore()
	{
		stopScore = true;
	}
}
