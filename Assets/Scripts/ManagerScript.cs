using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public EnemyManager enemyManager;
    public GameObject playerRef;
    public Transform cameraRef;
    public Transform secondCamera;

    public int currentArena = 0;
    public float d = 32;

    Vector2 currentArenaPos = Vector2.zero;
    Vector2 nextArenaPos;
    Vector2 playerTravelPreviousPos;
    Vector2 playerTravelNextPos;
    Vector3 currentArena3d;
    Vector3 nextArena3d;
    public float arenaTravelDuration = 1.0f;
    float arenaTravelTime = 0.0f;
    bool movingArenas = false;

    bool allowedToTravel = false;

    public void MoveArenas(Vector2 playerEndPos, Vector2 newArenaPos, Vector3 newLocation)
    {
        playerRef.GetComponent<PlayerScript>().userControl = false;
        playerTravelPreviousPos = playerRef.transform.position;
        playerTravelNextPos = playerEndPos;
        currentArena3d = secondCamera.position;
        nextArena3d = newLocation;
        nextArenaPos = newArenaPos;
        arenaTravelTime = 0.0f;
        movingArenas = true;

        currentArena += 1;
    }

    void Start()
    {
        enemyManager.EnemyWaveData(007.9f,"00:007.9 00:005.0 00:003.6 00:002.4");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            MoveArenas(new Vector2(0.5f, -32.5f), new Vector2(0, -32), new Vector3(102, 11, -32));
        }

        if (movingArenas)
        {
            arenaTravelTime += 1.0f * Time.deltaTime;

            playerRef.transform.position = SetZ((playerTravelNextPos * (arenaTravelTime / arenaTravelDuration)) 
                + (playerTravelPreviousPos * (1 - arenaTravelTime / arenaTravelDuration)), 0);
            cameraRef.position = SetZ((nextArenaPos * (arenaTravelTime / arenaTravelDuration)) 
                + (currentArenaPos * (1 - arenaTravelTime / arenaTravelDuration)), -10);
            secondCamera.position = (nextArena3d * (arenaTravelTime / arenaTravelDuration))
                + (currentArena3d * (1 - arenaTravelTime / arenaTravelDuration));

            if (arenaTravelTime >= arenaTravelDuration)
            {
                movingArenas = false;
                playerRef.transform.position = SetZ(playerTravelNextPos, 0);
                cameraRef.position = SetZ(nextArenaPos, -10);
                secondCamera.position = nextArena3d;
                playerRef.GetComponent<PlayerScript>().userControl = true;
                StartNewArena();
            }
        }
    }

    Vector3 SetZ(Vector2 flatVector, float newZ)
    {
        Vector3 output = Vector3.zero;
        output.x = flatVector.x;
        output.y = flatVector.y;
        output.z = newZ;
        return output;
    }

    public void Done()
    {
        enemyManager.EnemyWaveData(007.9f, "00:007.9 00:005.0 00:003.6 00:002.4");
    }

    public void StageCleared()
    {
        allowedToTravel = true;
        if(currentArena == 1 || currentArena == 3)
        {
            //call shop function
        }
        else
        {
            //call blinking arrow
        }
        // remove walls preventing travel
    }

    void StartNewArena()
    {
        allowedToTravel = false;
    }
}
