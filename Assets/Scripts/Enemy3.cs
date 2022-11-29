using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy3 : MonoBehaviour
{
    public KeyCode space;

    Rigidbody2D enemy;
    Collider2D eColl;
    [SerializeField] private Transform _player;
    [SerializeField] GameObject player;
    GameObject eManager;
    [SerializeField] public float speed;
    bool moving;
    //bool aggressive;
    //bool angry;
    bool scared; //if player has katana, causes them to run
    float moveTimerReset = 0;
    int moveType;
    bool frozen;
    float timeSpentFrozen = 0.0f;
    public List<Sprite> spriteList;

    Vector2 Floor= new Vector2(0,0);
    Vector2 rPosSelect = new Vector2(0, 0);
    
    void Start()
    {
        moveTimerReset = 5.0f;
        eManager = GameObject.Find("EnemyManager");
        Floor = new Vector2(0, 0 + (32 * eManager.GetComponent<EnemyManager>().GetFloor()));
        rPosSelect = new Vector2(UnityEngine.Random.Range(-4f, 5f), UnityEngine.Random.Range(-4 + Floor.y, 5 + Floor.y)); //finds targer location on spawn
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
    }

    void Update()
    {
        
        if (player.GetComponent<PlayerState>().swordTime > 0.0f)
            scared = true;
        else
            scared = false;

        enemy.velocity = Vector2.zero;
        if (moveTimerReset > 0.0f)
        {

            if (!scared)
            {
                MovePassive();
            }
            else
                MoveFlee();
        }
        else
        {
            if (!frozen)
            {
                enemy.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
                GetComponent<HealthScript>().TakeDamage(-5);
                frozen = true;
                
            }
        }
        moveTimerReset -= Time.deltaTime;
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
    }
    
    void MovePassive()
    {
        Move(rPosSelect);
    }

    void MoveFlee()
    {
        Move(new Vector2(transform.position.x -_player.position.x,transform.position.y - _player.position.y));
    }

    void Move(Vector2 TargetPosition)
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

    int DistanceFromPlayer() //casts to int, i assume it floors it. its fineeeeee
    {
        int xD = (int)(_player.position.x - transform.position.x);
        int yD = (int)(_player.position.y - transform.position.y);        
        return (int)Math.Sqrt((xD * xD) + (yD * yD));


    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        if (collide.gameObject.GetComponent<PlayerScript>() != null)
            player.GetComponent<HealthScript>().TakeDamage(1);
    }

}
