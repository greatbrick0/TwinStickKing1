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
        storedWaves.Add("00:094.1 00:090.3 00:089.6 00:089.1 00:088.4 00:085.7 00:085.5 00:085.3 00:082.2 " +
            "00:082.0 00:081.8 00:081.6 00:081.4 00:081.2 00:081.0 00:080.3 00:079.8 00:078.9 00:076.3 00:074.0 00:072.9 " +
            "00:072.3 00:071.8 00:071.3 00:069.0 00:068.5 00:067.0 00:066.6 00:065.8 00:065.0 00:064.4 00:063.9 00:063.5 " +
            "00:062.0 00:061.5 00:061.2 00:060.9 00:060.6 00:060.3 00:060.0 00:058.5 00:058.0 00:057.0 00:053.3 00:053.0 " +
            "00:052.5 00:052.0 00:051.3 00:050.5 00:049.0 00:046.8 00:044.4 00:043.9 00:043.5 00:043.2 00:043.1 00:042.9 " +
            "00:042.7 00:042.5 00:041.0 00:039.2 00:039.1 00:039.0 00:038.5 00:037.0 00:034.0 00:031.0 00:030.0 00:029.0 " +
            "00:028.0 00:023.8 00:023.7 00:023.6 00:023.5 00:023.4 00:023.3 00:023.2 00:023.1 00:023.0 00:022.9 00:022.8 " +
            "00:025.7 00:023.9 00:022.6 00:022.5 00:021.5 00:021.0 00:020.5 00:020.0 00:017.3 00:015.3 00:014.7 00:013.5 " +
            "00:013.4 00:013.3 00:013.2 00:013.1 00:012.8 00:007.9 00:007.0 00:005.0 00:004.6 00:003.6 00:003.0 00:002.4 00:001.4");
        storedWavesDuration.Add(094.1f);
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
