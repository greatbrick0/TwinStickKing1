using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    public KeyCode space;

    Rigidbody2D enemy;
    RigidbodyConstraints2D ogconstraints;
    [SerializeField] private Transform _player;
    [SerializeField] GameObject player;
    GameObject eManager;
    [SerializeField] public float speed;
    [SerializeField] public float floorVal;
    bool moving;
    bool aggressive;
    bool angry;
    bool scared; //if player has katana, causes them to run
    bool lostPlayer;
    float moveTimerReset = 0;
    int moveType;

    Vector2 Floor= new Vector2(0,0);
    Vector2 rPosSelect = new Vector2(0, 0);
    
    void Start()
    {
        eManager = GameObject.Find("EnemyManager");
        Floor = new Vector2(0, 0 + (floorVal * eManager.GetComponent<EnemyManager>().GetFloor()));
        aggressive = (UnityEngine.Random.Range(0, 4) == 0); //1/4 of enemies will always attack, as 'aggressive' enemies.
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
        MoveReset();
        ogconstraints = enemy.constraints;
    }

    void Update()
    {
        if (DistanceFromPlayer() <= 3)
            angry = true;
        else if (!aggressive)
            angry = false;
        if (player.GetComponent<PlayerState>().swordTime > 0.0f)
            scared = true;
        else
            scared = false;
        if (player.GetComponent<PlayerState>().smokebombTime > 0.0f)
        {
            lostPlayer = true;
            //PlayerTeleported();
        }
        if(player.GetComponent<PlayerState>().smokebombTime <= 0.0f)
            lostPlayer = false;

        enemy.velocity = Vector2.zero;
        if (!scared)
        {

            if (!angry)
            {
                switch (moveType)
                {
                    case 0:
                        MoveAggro();
                        break;
                    case 1:
                        MoveCenter();
                        break;
                    default:
                        MovePassive();
                        break;
                }
            }
            else
                MoveAggro();
        }
        else
            MoveFlee();
        
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
        if (lostPlayer)
        {
            enemy.velocity = new Vector2(0, 0);
        }
        else
        {
            moveTimerReset += 1.0f * Time.deltaTime;
            if (moveTimerReset >= 3)
            {
                MoveReset();
                moveTimerReset = 0;
            }
        }
    }

    public void MoveReset()
    {
        int rando = UnityEngine.Random.Range(0, 11);
        switch (rando)
        {
            case 0:
            case 1:
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7:
                moveType = 0;
                break;
            case 8:
            case 9:
                moveType = 1;
                break;
            case 10:
                moveType = 2;
                rPosSelect = new Vector2(UnityEngine.Random.Range(-7f, 8f), UnityEngine.Random.Range(-7 + Floor.y, 8 + Floor.y));
                break;
        }
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
        Move(new Vector2(transform.position.x -_player.position.x,transform.position.y - _player.position.y));
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

    int DistanceFromPlayer() //casts to int, i assume it floors it. its fineeeeee
    {
        int xD = (int)(_player.position.x - transform.position.x);
        int yD = (int)(_player.position.y - transform.position.y);        
        return (int)Math.Sqrt((xD * xD) + (yD * yD));


    }

   /* 
    * void PlayerTeleported()
    {
        if (lostPlayer)
        {
            enemy.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePosition | RigidbodyConstraints2D.FreezeRotation;
        }
            enemy.constraints = ogconstraints;
    }
   */

    void OnCollisionEnter2D(Collision2D collide)
    {
        if (!scared && !lostPlayer)
        {
            if (collide.gameObject.GetComponent<PlayerScript>() != null)
                player.GetComponent<HealthScript>().TakeDamage(1);
        }
        else if (scared)
        {
            if (collide.gameObject.GetComponent<PlayerScript>() != null)
                this.gameObject.GetComponent<HealthScript>().TakeDamage(10);
        }
        //
    }

}
