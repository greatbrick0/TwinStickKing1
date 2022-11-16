using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    Rigidbody2D body;
    GunScript gun;

    Vector2 moveDirection;
    Vector2 shootDirection;

    bool attemptShoot;

    public int baseDamage = 1; // these are used for permanant shop upgrades
    public float baseShootSpeed = 0.4f;
    public float baseSpeed = 2;

    public int bulletNumMod = 1; // these are used for temporary power ups
    public float moveSpeedMod = 1.0f;
    public float shootSpeedMod = 1.0f;

    public bool userControl = true;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        gun = GetComponent<GunScript>();
    }

    void Update()
    {
        moveDirection = FindMoveVector();
        shootDirection = FindShootVector();
        attemptShoot = shootDirection.magnitude > 0.0f;
    }

    void FixedUpdate()
    {
        if(GetComponent<HealthScript>().health <= 0)
        {
            PlayerDeath();
        }

        body.velocity = moveDirection * baseSpeed * moveSpeedMod;
        if (attemptShoot)
        {
            if(gun.Shoot(shootDirection, baseDamage, bulletNumMod))
            {
                gun.Reload(baseShootSpeed * shootSpeedMod);
            }
        }
    }

    Vector2 FindMoveVector()
    {
        Vector2 outputVector = new Vector2(0, 0);
        if (userControl)
        {
            if (Input.GetKey(KeyCode.W))
            {
                outputVector += new Vector2(0, 1);
            }
            if (Input.GetKey(KeyCode.S))
            {
                outputVector += new Vector2(0, -1);
            }
            if (Input.GetKey(KeyCode.A))
            {
                outputVector += new Vector2(-1, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                outputVector += new Vector2(1, 0);
            }
        }
        outputVector.Normalize();
        return outputVector;
    }

    Vector2 FindShootVector()
    {
        Vector2 outputVector = new Vector2(0, 0);
        if (userControl)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                outputVector += new Vector2(0, 1);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                outputVector += new Vector2(0, -1);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                outputVector += new Vector2(-1, 0);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                outputVector += new Vector2(1, 0);
            }
        }
        outputVector.Normalize();
        return outputVector;
    }

    void OnCollisionEnter(Collision collision)
    {
        print(name + " collided with: " + collision.gameObject.name);
    }

    void PlayerDeath()
    {
        GetComponent<HealthScript>().health = 1;
        print("player died");
        //run other code
    }
}
