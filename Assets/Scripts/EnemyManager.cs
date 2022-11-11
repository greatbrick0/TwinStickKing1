using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerRef;

    public List<GameObject> enemies;
    GameObject newEnemy;

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
            newEnemy = Instantiate(enemies[0], this.transform);
            newEnemy.transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
            newEnemy.GetComponent<Enemy1>().SetSpeed(0.965f);
        }
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
}
