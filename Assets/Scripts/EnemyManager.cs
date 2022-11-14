using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerRef;

    public List<GameObject> enemies;
    GameObject newEnemy;

    float spawningDuration = 100.0f;
    string enemySpawnData;
    public int alive = 0;
    public List<Vector2> spawnPos = new List<Vector2>();

    int Floor = 0;
    void Start()
    {
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
        if (Input.GetKey(KeyCode.E))
        {
            SpawnEnemy(0);
        }
    }

    void SpawnEnemy(int ID)
    {
        newEnemy = Instantiate(enemies[ID], this.transform);
        newEnemy.transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
        newEnemy.GetComponent<Enemy1>().SetSpeed(2f);
    }

    public int getFloor()
    {
        return Floor;
    }

    public void NextFloor() //when the level is over, please call this. It will adjust spawnPosition
    {
        Vector2 newLocation;
        Floor++;
        for (int i=0; i<4; i++)
        {
           newLocation = spawnPos[0];
           newLocation.y = newLocation.y + (32 * Floor);
           spawnPos[0] = newLocation;
        }
    }

    public void EnemyWaveData(float time, string data)
    {
        //wave data will be passed down from the manager as a string when the player goes to a new arena
        //ex: "00:002.4 01:003.6 00:005.0 02:007.9"
        //different enemy spawns are seperated by spaces
        //the first part of each enemy spawn is the type of enemy, the second part is the time they spawn
        enemySpawnData = data;
        spawningDuration = time;
    }

    bool CheckWaveEnded()
    {
        return spawningDuration <= 0.0f && alive <= 0;
    }

    void KillChildren()
    {
        //delete all children of the enemy manager
    }


}
