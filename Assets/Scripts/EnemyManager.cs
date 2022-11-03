using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject playerRef;

    public List<GameObject> enemies;

    public int alive = 0;
    public List<Vector2> spawnPos = new List<Vector2>();

    void Start()
    {
        spawnPos.Add(new Vector2(0, -8));
        spawnPos.Add(new Vector2(0, 8));
        spawnPos.Add(new Vector2(-8, 0));
        spawnPos.Add(new Vector2(8, 0));
    }
}
