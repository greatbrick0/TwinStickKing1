using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    public KeyCode space;

    Rigidbody2D enemy;


    [SerializeField] private Transform _player;
    [SerializeField] GameObject player;

    public float speed;
    bool moving;

    int moveTimerReset = 0;
    int moveType;

    Vector2 Floor= new Vector2(0,0);

    
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
        MoveReset();
    }

    void Update()
    {
        enemy.velocity = Vector2.zero;
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
        Collision2D.Equals(player, enemy);
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
        moveTimerReset++;
        if (moveTimerReset == 300)
        {
            MoveReset();
            moveTimerReset = 0;
        }
    }

    public void MoveReset()
    {
        int rando = Random.Range(0, 10);
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

}
