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
    public int[] shopLevels = new int[] { 1, 3 };
    List<string> storedWaves = new List<string>();
    List<float> storedWavesDuration = new List<float>();

    public void MoveArenas(Vector2 playerEndPos, Vector2 newArenaPos, Vector3 newLocation)
    {
        arenaTravelTime = 0.0f;
        movingArenas = true;
        currentArena += 1;
        allowedToTravel = false;
        playerRef.GetComponent<PlayerScript>().userControl = false;

        playerTravelPreviousPos = playerRef.transform.position;
        playerTravelNextPos = playerEndPos;

        currentArena3d = secondCamera.position;
        nextArena3d = newLocation;
        nextArenaPos = newArenaPos;
    }

    void Start()
    {
        storedWaves.Add("00:007.9 00:005.0 00:003.6 00:002.4");
        storedWavesDuration.Add(007.9f);
        storedWaves.Add("00:012.8 00:007.9 00:007.0 00:005.0 00:004.6 00:003.6 00:003.0 00:002.4 00:001.4");
        storedWavesDuration.Add(012.8f);

        StartNewArena();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && allowedToTravel)
        {
            MoveArenas(new Vector2(0.5f, -32.5f - currentArena * d), new Vector2(0, -32 - currentArena * d), new Vector3(102, 11, -32 - currentArena * d));
        }

        if (movingArenas)
        {
            arenaTravelTime += 1.0f * Time.deltaTime;

            playerRef.transform.position = SetZ((playerTravelNextPos * (arenaTravelTime / arenaTravelDuration)) 
                + (playerTravelPreviousPos * (1 - arenaTravelTime / arenaTravelDuration)), 0);
            cameraRef.position = SetZ((nextArenaPos * (arenaTravelTime / arenaTravelDuration)) 
                + (currentArenaPos * (1 - arenaTravelTime / arenaTravelDuration)), -10);
            secondCamera.position = (nextArena3d * (arenaTravelTime / arenaTravelDuration))
                + (currentArena3d * (1 - arenaTravelTime / arenaTravelDuration)); // do not touch this

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
        if(currentArena == 4)
        {
            // win the game
        }
        else
        {
            allowedToTravel = true;
            if (currentArena == 1 || currentArena == 3)
            {
                //call shop function
            }
            else
            {
                //call blinking arrow
            }
            // remove walls preventing travel
        }
    }

    void StartNewArena()
    {
        
        enemyManager.EnemyWaveData(storedWavesDuration[currentArena], storedWaves[currentArena]);
    }
}
