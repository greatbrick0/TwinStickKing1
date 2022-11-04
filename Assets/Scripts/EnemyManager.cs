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

    void Start()
    {
        spawnPos.Add(new Vector2(0, -8));
        spawnPos.Add(new Vector2(0, 8));
        spawnPos.Add(new Vector2(-8, 0));
        spawnPos.Add(new Vector2(8, 0));
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.E))
        {
            newEnemy = Instantiate(enemies[0]);
            newEnemy.transform.position = spawnPos[Random.Range(0, spawnPos.Count)];
        }
    }
}
