using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Room : MonoBehaviour
{

    public Vector2 room_position = new Vector2(0f, 0f);
    public IntVector2 room_size = new IntVector2(8, 8);

    public GameObject[] floorTiles;
    public GameObject[] wallTiles;
    public GameObject player;

    public bool startRoom;

    private Transform boardHolder;

    private UnityEngine.Object pipe_resource;
    private UnityEngine.Object IDS;
    private UnityEngine.Object FIREWALL;
    private UnityEngine.Object ANTIVIRUS;

    public GameObject input_pipe;
    public GameObject output_pipe;

    private Bounds bounds;

    private Transform in_location;

    public bool visited = false;
    bool visitedOnce = false;
    public bool killed = false;

    public int IDSNum = 3;
    public int FIREWALLNum = 3;
    public int ANTIVIRUSNum = 3;

    public Room(Vector2 position, IntVector2 size)
    {
        this.room_position = position;
        this.room_size = size;

        initBoard();
    }

    public Vector2 getFullSize()
    {
        Vector2 full_size = new Vector2((this.room_size.X + 2) * bounds.size.x, (this.room_size.Y + 2) * bounds.size.y);
        // Debug.Log("room_size.x: " + this.room_size.X);
        // Debug.Log("bounds_size.x: " + this.bounds.size.x);
        // Debug.Log("full_size: " + full_size);
        return full_size;
    }

    public void Start()
    {
        initBoard();
        initEnemies();
    }

    public void initBoard()
    {
        GameObject board = new GameObject("Board");
        board.layer = 12;
        boardHolder = board.transform;

        for (int x = -1; x < room_size.X + 1; x++)
        {
            for (int y = -1; y < room_size.Y + 1; y++)
            {

                GameObject toInit;

                if (x == -1 || x == room_size.X || y == -1 || y == room_size.Y)
                {
                    //Debug.Log("Select wal");                               
                    toInit = wallTiles[Random.Range(0, wallTiles.Length)];
                }
                else
                {
                    //Debug.Log("Select floor");
                    toInit = floorTiles[Random.Range(0, floorTiles.Length)];
                }


                bounds = toInit.GetComponent<Renderer>().bounds;

                float posx = room_position.x + (bounds.size.x * x);
                float posy = room_position.y + (bounds.size.y * y);

                GameObject instance = Instantiate(toInit, new Vector3(posx, posy, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
        if (startRoom)
        {
            setPlayerStart();
        }
    }

    public void setPlayerStart()
    {
        float posx = room_position.x + room_size.X * 0.5f;
        float posy = room_position.y + room_size.Y * 0.5f;
        player.transform.position = new Vector3(posx, posy, 0);
    }

    public Transform getInLocation()
    {
        return input_pipe.transform;
    }

    public Transform getOutLocation()
    {
        return output_pipe.transform;
    }
    public void initPipes()
    {

        pipe_resource = Resources.Load("Pipe");

        Vector3 pos_in = new Vector3(room_position.x + bounds.size.x, room_position.y, 0f);
        Vector3 pos_out = new Vector3(room_position.x + (bounds.size.x * (room_size.X - 2)), room_position.y + (bounds.size.y * 2));

        input_pipe = Instantiate(pipe_resource, pos_in, Quaternion.identity) as GameObject;
        output_pipe = Instantiate(pipe_resource, pos_out, Quaternion.identity) as GameObject;

        input_pipe.transform.parent = transform;
        output_pipe.transform.parent = transform;

        // input_pipe.transform.SetParent(boardHolder);
        // output_pipe.transform.SetParent(boardHolder);
    }

    public void connectInPipe(Room destination)
    {
        PipeController pc = input_pipe.GetComponent(typeof(PipeController)) as PipeController;

        pc.destination = destination.getOutLocation();
        pc.parent = this;
        pc.destination_room = destination;
    }

    public void connectOutPipe(Room destination)
    {
        PipeController pc = output_pipe.GetComponent(typeof(PipeController)) as PipeController;

        pc.destination = destination.getInLocation();
        pc.parent = this;
        pc.destination_room = destination;
    }

    public Transform slocation;
    public GameObject rabbit;
    public float interval = 1;
    public float curTimer = 0;

    float CurRabbits = 0;
    public float MaxCapacity = 20;

    public Slider slider;
    public Text text;
    // 
    public Animator ani;
    public GameObject BSOD;

    private void FixedUpdate()
    {
        if (visited) {
            visitedOnce = true;
        }

        if (killed){
            DisableMe();
            return;
        }

        if (!killed && visitedOnce)
        {
            curTimer += Time.deltaTime;
            if (curTimer > interval)
            {
                SpawnRabbit();
                curTimer = 0;
            }
            checkLose();
        }
    }

    void SpawnRabbit()
    {
        // Count the rabbits
        CurRabbits = GetComponentsInChildren<SpawnController>().Length;


        //Debug.Log(player.transform.position);
        GameObject go = Instantiate(rabbit, player.transform.position, player.transform.rotation);
        go.transform.parent = transform;
        go.layer = 12;

        CurRabbits++;

        if (visited) {
            updateUI();
        }
    }

    public void updateUI()
    {
        slider.value = CurRabbits / MaxCapacity;
        float perc = Mathf.Round((CurRabbits / MaxCapacity) * 100);
        text.text = "" + perc + "%";
        if (perc >= 75)
        {
            // Set animator alert
            ani.SetBool("Alarm", true);
        }
        else
        {
            // Set animator to not alert
            ani.SetBool("Alarm", false);
        }
    }

    void checkLose()
    {
        if (CurRabbits >= MaxCapacity)
        {
            if (visited){
                BSOD.SetActive(true);
                // Kill all other rooms
                Room[] allRooms = GameObject.FindObjectsOfType(typeof(Room)) as Room[];
                foreach (Room derp in allRooms){
                    Room r = derp.GetComponent<Room>();
                    r.killIT();
                    derp.killIT();
                }

                GameObject go;
                go = GameObject.FindGameObjectWithTag("GC");
                BoardManager gm = go.GetComponent<BoardManager>();
                gm.StopScore();
            }
            DisableMe();
        }
    }

    void DisableMe()
    {
        setEnabled(false);
        killed = true;
        // tell pipes to stop
        foreach (PipeController p in gameObject.GetComponentsInChildren<PipeController>())
        {
            p.setActive(false, true);
        }
        // tell rabbits to stop
        foreach (SpawnController s in gameObject.GetComponentsInChildren<SpawnController>())
        {
            s.stopSpawning();
        }
    }

    void initEnemies() {
        // Spawn IDS
        IDS = Resources.Load("ids");
        ANTIVIRUS = Resources.Load("antivirus");
        FIREWALL = Resources.Load("firewall");

        Debug.Log("Making " + IDSNum + " IDSSss");

        for (int i = 0; i < IDSNum; i++){
            GameObject go = Instantiate(IDS, room_position + new Vector2(room_size.X /2, room_size.Y /2), transform.rotation, transform) as GameObject;
            go.layer = 12;
        }

        Debug.Log("Making " + FIREWALLNum + " FIREWALLS");

        for (int i = 0; i < FIREWALLNum; i++){
            GameObject go = Instantiate(FIREWALL, room_position + new Vector2(room_size.X /2, room_size.Y /2), transform.rotation, transform) as GameObject;
            go.layer = 12;
        }

        Debug.Log("Making " + ANTIVIRUSNum + " ANTIVIRUS");

        for (int i = 0; i < ANTIVIRUSNum; i++){

            GameObject go = Instantiate(ANTIVIRUS, room_position + new Vector2(room_size.X /2, room_size.Y /2), transform.rotation, transform) as GameObject;
            go.layer = 12;
        }
    }

    public void setEnabled(bool e)
    {
        this.visited = e;
    }

    public void killIT(){
        killed = true;
        DisableMe();
    }

}
