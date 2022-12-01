using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBoss: MonoBehaviour
{
    public KeyCode space;

    Rigidbody2D enemy;

    [SerializeField] private Transform _player;
    [SerializeField] GameObject player;
    GameObject eManager;
    GameObject HealthBar;
    [SerializeField] public float speed;
    bool moving;
    float moveTimerReset = 0.0f;
    float movementPauseTime = 0.0f;
    int moveType;
    [SerializeField] public float floorVal;
    Vector2 leftBorder;
    Vector2 rightBorder;
    Vector2 Floor = new Vector2(0, 0);

    public List<string> targetTeam = new List<string>();
    int shotCount;
    int fastShotVal;
    float shotTime;
    public GameObject bulletRef;

    void Start()
    {
        GetComponent<EnemyScript>().isWalking = false;
        shotTime = 3.0f;
        shotCount = 0;
        eManager = GameObject.Find("EnemyManager");
        HealthBar = GameObject.Find("BossHealthLine");
        Floor = new Vector2(0, 0 + (floorVal * eManager.GetComponent<EnemyManager>().GetFloor()));
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
        leftBorder = new Vector2(transform.position.x - 4, transform.position.y);
        rightBorder = new Vector2(transform.position.x + 4, transform.position.y);
        MoveReset();
    }

    void Update()
    {

        if (shotTime <= 0.0f)
            Shoot();
        shotTime -= Time.deltaTime;

        if (movementPauseTime >= 0.0f)
        {
            switch (moveType)
            {
                case 0:
                    MoveLeft();
                    break;
                case 1:
                    MoveRight();
                    break;
                default:
                    break;
            }
        }
        HealthBar.transform.localScale = new Vector2(2,(float) GetComponent<HealthScript>().health / 50);
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
    public float GetSpeed()
    {
        return speed;
    }
    public void SetFloor(int f)
    {
        Floor.y = f;
    }
    public Vector2 GetFloor()
    {
        return Floor;
    }
    void FixedUpdate()
    {
       
        moveTimerReset += Time.deltaTime;
        if (moveTimerReset >= 3)
        {
            MoveReset();
            moveTimerReset = 0;
        }
    }

    public void MoveReset()
    {
        movementPauseTime = (UnityEngine.Random.Range(0.5f, 2.5f));
        int rando = UnityEngine.Random.Range(0, 2);
        switch (rando)
        {
            case 0:
                moveType = 0;
                break;
            case 1:
                moveType = 1;
                break;
 
        }
    }

    void MoveLeft()
    { 
        Move(leftBorder);
        if (transform.position.x <= leftBorder.x)
        {
            moveType = 1;
            Debug.Log("Turn right.");
        }
    }

    void MoveRight()
    {
        Move(rightBorder);
        if (transform.position.x >= rightBorder.x)
        {
            moveType = 0;
            Debug.Log("Turn right.");
        }
    }
    

    void Move(Vector2 TargetPosition)
    {
        if (player.GetComponent<PlayerState>().smokebombTime <= 0.0f)
        {
            Vector2 moveVCount = new Vector2(0, 0);
            if (transform.position.y < TargetPosition.y)
            {
                moveVCount.y++;
            }
            if (transform.position.y > TargetPosition.y)
            {
                moveVCount.y--;
            }
            if (transform.position.x < TargetPosition.x)
            {
                moveVCount.x++;
            }
            if (transform.position.x > TargetPosition.x)
            {
                moveVCount.x--;
            }
            if (moveVCount.x != 0 && moveVCount.y != 0)
            {
                moveVCount.x /= 2;
                moveVCount.y /= 2;
            }
            //transform.position = Vector2.MoveTowards(transform.position, Line, move);
            enemy.velocity = moveVCount * speed;
        }
    }

    void Shoot()
    {
        ShootTimeSet();
        GameObject newBullet;
        Vector2 playerDirection = new Vector2(_player.position.x - transform.position.x, _player.position.y - transform.position.y);
        playerDirection.Normalize();
        newBullet = Instantiate(bulletRef);
        newBullet.transform.position = transform.position;
        newBullet.GetComponent<BulletScript>().direction = playerDirection;
        newBullet.GetComponent<BulletScript>().speed = 7.0f;
        newBullet.GetComponent<BulletScript>().targetTeam = targetTeam;
        newBullet.GetComponent<BulletScript>().damage = 1;

    }

    void ShootTimeSet()
    {
        switch (shotCount)
        {
            case 0:
            case 1:
                shotTime = 3.0f;
                shotCount++;
                break;
            case 2:
            case 3:
            case 4:
                shotTime = 0.75f;
                    shotCount++;
                break;
            case 5:
                shotTime = 3.0f;
                shotCount = 0;
                break;
        }
    }
}
