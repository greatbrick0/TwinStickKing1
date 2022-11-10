using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public EnemyManager enemyManager;
    public GameObject playerRef;
    public Transform cameraRef;

    Vector2 currentArenaPos = Vector2.zero;
    Vector2 nextArenaPos;
    Vector2 playerTravelPreviousPos;
    Vector2 playerTravelNextPos;
    public float arenaTravelDuration = 1.0f;
    float arenaTravelTime = 0.0f;
    bool movingArenas = false;

    public void MoveArenas()
    {
        playerRef.GetComponent<PlayerScript>().userControl = false;
        playerTravelPreviousPos = playerRef.transform.position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
