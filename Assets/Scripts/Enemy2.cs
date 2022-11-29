using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy2 : MonoBehaviour
{
    public KeyCode space;

    Rigidbody2D enemy;

    [SerializeField] private Transform _player;
    [SerializeField] GameObject player;
    GameObject eManager;
    [SerializeField] float baseSpeed;
    float speed;
    bool moving;
   // bool aggressive = true; //the bulky enemy is always angry! (does nothing)
    //bool scared = false; also does nothing. I simplified the brute's decisions, so it isnt needed. just being consistant.
    //float moveTimerReset = 0;
    //int moveType;

    Vector2 Floor= new Vector2(0,0);
    Vector2 rPosSelect = new Vector2(0, 0);
    
    void Start()
    {
        speed = baseSpeed;
        eManager = GameObject.Find("EnemyManager");
        Floor = new Vector2(0, 0 + (32 * eManager.GetComponent<EnemyManager>().GetFloor()));
        //aggressive = (UnityEngine.Random.Range(0, 4) == 0); //1/4 of enemies will always attack, as 'aggressive' enemies.
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
    }

    void Update()
    {
        if (DistanceFromPlayer() < 3)
        {
            SetSpeed(baseSpeed * 2f);
        }
        else
            SetSpeed(baseSpeed);
        enemy.velocity = Vector2.zero;
        if (player.GetComponent<PlayerState>().swordTime >= 0.0f)
            MoveFlee();
        else
            MoveAggro();
        

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



    void MoveAggro()
    {
        
        Move(_player.position);
        

    }

    void MoveCenter()
    {
        Move(Floor);
    }
    void MovePassive()
    {
        Move(rPosSelect);
    }
    void MoveFlee()
    {
        Move(new Vector2(transform.position.x - _player.position.x, transform.position.y - _player.position.y));
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
        int xD = (int)(player.transform.position.x - transform.position.x);
        int yD = (int)(player.transform.position.y - transform.position.y);        
        return (int)Math.Sqrt((xD * xD) + (yD * yD));


    }

    void OnCollisionEnter2D(Collision2D collide)
    {
        if(collide.gameObject.GetComponent<PlayerScript>() != null)
        {
            if (player.GetComponent<PlayerState>().swordTime > 0.0f)
                player.GetComponent<HealthScript>().TakeDamage(1);
            else
                GetComponent<HealthScript>().TakeDamage(10);
        }
    }
}
