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

    int F;
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
        _player = player.transform;
        MoveReset();
    }

    void Update()
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
    public void GetFloor(int f)
    {
       F = f;
    }
    public int GetFloor()
    {
        return F;
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
        float move = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, _player.position, move);
    }

    void MoveCenter()
    {
        float move = speed * Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(0, 0*32*F), move);
    }
    void MovePassive()
    {

    }
}
