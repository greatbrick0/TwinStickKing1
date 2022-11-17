using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerRef;
    public ManagerScript mRef;
    [SerializeField] public List<GameObject> powerDrops; //powerup list, used by enemies on death.
    public List<GameObject> enemies;
    GameObject newSpawn;

    float spawningDuration = 0f;
    string enemySpawnData;
    Stack<string> spawnDatArray = new Stack<string>();
    public int alive = 0;
    public List<Vector2> spawnPos = new List<Vector2>();
    float currentRoundTime = 0f;
    bool spawn; //used in fixedupdate to repeat the spawn if necissary
    int Floor = 0;
    float d = 1;

    void Start()
    {
        d = mRef.d; //this is the distance between arenas, 32

        if(spawnPos.Count == 0)
        {
            spawnPos.Add(new Vector2(0, -8));
            spawnPos.Add(new Vector2(0, 8));
            spawnPos.Add(new Vector2(-8, 0));
            spawnPos.Add(new Vector2(8, 0));
        }
    }

    void FixedUpdate()
    {
        spawn = true;
        currentRoundTime += Time.deltaTime;
        while (spawn && spawnDatArray.Count > 0)
        {
            string topSpawn = spawnDatArray.Peek();
            string[] spawns = topSpawn.Split(':');
            if (float.Parse(spawns[1]) <= currentRoundTime)
            {
                
                SpawnEnemy(int.Parse(spawns[0]));
                spawnDatArray.Pop();
            }
            else
                spawn = false;

        }

       /* if (Input.GetKey(KeyCode.E))
        {
            SpawnEnemy(0);
        } */
    }

    void SpawnEnemy(int ID)
    {
        newSpawn = Instantiate(enemies[ID], this.transform);
        newSpawn.transform.position = spawnPos[UnityEngine.Random.Range(0, spawnPos.Count)];
        newSpawn.GetComponent<Enemy1>().SetSpeed(2f);
        alive++;
    }

    public int getFloor()
    {
        return Floor;
    }

    void NextFloor() //when the level is over, please call this. It will adjust spawnPosition
    {
        Vector2 newLocation;
        Floor++;
        for (int i=0; i<4; i++)
        {
           newLocation = spawnPos[i];
           newLocation.y = newLocation.y - (d * Floor);
           spawnPos[i] = newLocation;
        }
    }

    public void EnemyWaveData(float time, string data)
    {
        //wave data will be passed down from the manager as a string when the player goes to a new arena
        //ex: "00:002.4 01:003.6 00:005.0 02:007.9"
        //different enemy spawns are seperated by spaces
        //the first part of each enemy spawn is the type of enemy, the second part is the time they spawn
        currentRoundTime = 0f;
        enemySpawnData = data;
        spawningDuration = time;
        string[] arr = data.Split(' ');
        spawnDatArray = new Stack<string>(arr);
    }

    bool CheckWaveEnded()
    {
        return currentRoundTime >= spawningDuration && alive <= 0;
    }

    void KillChildren() //Trigger this on player death, plz
    {
        for (int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "healthbody")
            {
                Destroy(transform.GetChild(i).gameObject);
            }
        }
    }

    public void spawnDrop(Vector2 dropSpot) //triggered by an enemy dying (enemyScript). spawns a powerup (sometimes)
    {
        int dropRando = UnityEngine.Random.Range(0, 100);

        if(dropRando <= 2) //from 0 to 2 (3%)
        {
            //newSpawn = Instantiate(powerDrops[0], this.transform); //10Credit
            //newSpawn.transform.position = dropSpot;
        }
        else if( dropRando <= 19) //from 3 to 29 (15%)
        {
            dropRando = UnityEngine.Random.Range(0, 4); //Power up
            newSpawn = Instantiate(powerDrops[dropRando], this.transform);
            newSpawn.transform.position = dropSpot;
        }
    }

    public void enemyDies()
    {
        alive--;
        if (CheckWaveEnded())
        {
            Debug.Log("Wave Ended.");
            NextFloor();

            mRef.StageCleared();
        }
    }
}
