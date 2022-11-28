using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ManagerScript : MonoBehaviour
{
    public EnemyManager enemyManager;
    public GameObject playerRef;
    public Transform cameraRef;
    public Transform secondCamera;
    public Transform doorsRef;
    public GameObject arrowObj;
    public GameObject shopObj;
    GameObject newObj;

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

    float deathWaitTime = 0.0f;
    bool waitingForDeathRestart = false;

    public int coinAmount = 0;
    public int liveAmount = 3;
    public Dictionary<string, int> upgradeLevels = new Dictionary<string, int>() 
    {
        {"speed", 0}, { "attack", 0 }, { "damage", 0 }, { "lives", 0 }, { "kit", 0 }
    };
    public Dictionary<string, List<int>> upgradeCosts = new Dictionary<string, List<int>>()
    {
        {"speed", new List<int>(){80, 200}}, 
        {"attack", new List<int>(){100, 200, 300}},
        {"damage", new List<int>(){150, 300, 450}},
        {"lives", new List<int>(){100}},
        {"kit", new List<int>(){100}}
    };

    void MoveArenas(Vector2 playerEndPos, Vector2 newArenaPos, Vector3 newLocation)
    {
        if (allowedToTravel)
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
    }

    void Start()
    {
        //intentionally tedious
        storedWaves.Add("00:012.8 00:007.9 00:007.0 00:005.0 00:004.6 00:003.6 02:003.0 01:002.4 00:001.4");
        storedWavesDuration.Add(012.8f);
        storedWaves.Add("00:012.8 00:007.9 00:007.0 00:005.0 00:004.6 00:003.6 00:003.0 00:002.4 00:001.4");
        storedWavesDuration.Add(012.8f);
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
        if (waitingForDeathRestart)
        {
            deathWaitTime += 1.0f * Time.deltaTime;
            if(deathWaitTime >= 2.0f)
            {
                FinishPlayerDeath();
            }
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
                currentArenaPos = nextArenaPos;
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
            if (Contains(shopLevels, currentArena))
            {
                SpawnShop();
            }
            else
            {
                CreateArrow();
            }
            doorsRef.localPosition = new Vector3(20, 0, 0);
        }
    }

    void StartNewArena()
    {
        if(currentArena == storedWaves.Count)
        {
            //start boss
        }
        else
        {
            enemyManager.EnemyWaveData(storedWavesDuration[currentArena], storedWaves[currentArena]);
        }
        doorsRef.localPosition = new Vector3(0, 0, 0);
    }

    bool Contains(int[] checkList, int element)
    {
        foreach(int check in checkList)
        {
            if(check == element)
            {
                return true;
            }
        }
        return false;
    }

    public void FindAndMoveArenas()
    {
        DeleteShop();
        MoveArenas(new Vector2(0.5f, ((currentArena + 1) * -d) - 0.5f), new Vector2(0, (currentArena + 1) * -d), new Vector3(102, 11, (currentArena+1)*d*11));
    }

    void SpawnShop()
    {
        newObj = Instantiate(shopObj, transform);
        newObj.transform.position = currentArenaPos + new Vector2(0.5f, 10.0f);
        newObj.transform.position += new Vector3(0, 0, -8);
        //print("shop spawned at " + newObj.transform.position);
    }

    void DeleteShop()
    {
        for(int ii = 0; ii < transform.childCount; ii++)
        {
            if (transform.GetChild(ii).gameObject.GetComponent<ShopScript>() != null)
            {
                transform.GetChild(ii).gameObject.GetComponent<ShopScript>().CloseShop();
            }
        }
    }

    public void CreateArrow()
    {
        newObj = Instantiate(arrowObj, this.transform);
        newObj.transform.position = currentArenaPos + new Vector2(0.5f, -7.2f);
        newObj.transform.position += new Vector3(0, 0, -8);
    }

    public void StartPlayerDeath()
    {
        waitingForDeathRestart = true;
        deathWaitTime = 0.0f;
        playerRef.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
        playerRef.GetComponent<Collider2D>().enabled = false;
        playerRef.GetComponent<PlayerScript>().enabled = false;
        playerRef.GetComponent<HealthScript>().HealToMax();
    }

    void FinishPlayerDeath()
    {
        waitingForDeathRestart = false;
        deathWaitTime = 0.0f;
        liveAmount--;

        if (liveAmount < 0)
        {
            LoseGame();
        }
        else
        {
            playerRef.GetComponent<Collider2D>().enabled = true;
            playerRef.transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
            playerRef.GetComponent<PlayerScript>().enabled = true;
            enemyManager.DestroyChildren();
            playerRef.transform.position = SetZ(playerTravelNextPos, 0);
            StartNewArena();
        }
    }

    void LoseGame()
    {
        SceneManager.LoadScene(3);
    }
}
