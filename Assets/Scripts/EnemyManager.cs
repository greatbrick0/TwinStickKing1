using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerRef;
    public ManagerScript mRef;
    public Transform timerBarRef;
    public GameObject smokeObj;
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
        UpdateTimerBar();
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
    }

    void SpawnEnemy(int ID)
    {
        newSpawn = Instantiate(enemies[ID], this.transform);
        newSpawn.transform.position = spawnPos[UnityEngine.Random.Range(0, spawnPos.Count)];
        alive++;
    }

    public int GetFloor()
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
           newLocation.y = newLocation.y - (d);
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

    public void KillChildren() 
    {
        for (int i=0; i<transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "healthbody")
            {
                transform.GetChild(i).GetComponent<HealthScript>().TakeDamage(10);
            }
        }
    }

    /*public void FreezeChildren()
    {
        for (int i = 0; i < enemies.Count; i++)
        {
            if (playerRef.GetComponent<PlayerState>().smokebombTime > 0.0f)
            {
                print("lost player");
                enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            }
            //if (playerRef.GetComponent<PlayerState>().smokebombTime <= 0.0f)
            //{
            //    enemies[i].GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None | RigidbodyConstraints2D.FreezeRotation;
            //}
        }
    }*/

    public void DestroyChildren() //Trigger this on player death, plz
    {
        alive = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    public void spawnDrop(Vector2 dropSpot) //triggered by an enemy dying (enemyScript). spawns a powerup (sometimes)
    {
        int dropRando = UnityEngine.Random.Range(0, 100);

        if(dropRando <= 24) //from 0 to 25 (25%)
        {
            dropRando = UnityEngine.Random.Range(0, 4);
            if(dropRando == 3)
            {
                newSpawn = Instantiate(powerDrops[1], this.transform); //50 credit
                newSpawn.transform.position = dropSpot;
            }
            else
            {
                newSpawn = Instantiate(powerDrops[0], this.transform); //10Credit
                newSpawn.transform.position = dropSpot;
            }
        }
        else if( dropRando <= 40) //from 26 to 40 (15%)
        {
            dropRando = UnityEngine.Random.Range(2, 10); //Power up
            newSpawn = Instantiate(powerDrops[dropRando], this.transform);
            newSpawn.transform.position = dropSpot;
        }
    }

    public void enemyDies(Vector2 deathSpot)
    {
        newSpawn = Instantiate(smokeObj, this.transform);
        newSpawn.transform.position = deathSpot;
        alive--;
        GetComponent<AudioSource>().Play();
        if (CheckWaveEnded())
        {
            Debug.Log("Wave Ended.");
            NextFloor();

            mRef.StageCleared();
        }
    }

    void UpdateTimerBar()
    {
        timerBarRef.GetChild(0).localScale = new Vector2(2, MathF.Min(currentRoundTime / spawningDuration, 1));
    }
}
