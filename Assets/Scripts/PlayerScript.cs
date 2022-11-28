using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Animator animator;

    Rigidbody2D body;
    GameObject enemy;
    GunScript gun;

    Vector2 moveDirection;
    public Vector2 shootDirection;

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
        if (Input.GetKey(KeyCode.W) == true)
        {
            animator.speed.Equals(1f);
            animator.SetBool("movingUp", true);
            CheckShootingDirection();
        }
         else if (Input.GetKey(KeyCode.A) == true)
        {
            animator.speed.Equals(1f);
            animator.SetBool("movingLeft", true);
            CheckShootingDirection();
        }
        else if (Input.GetKey(KeyCode.S) == true)
        {
            animator.speed.Equals(1f);
            animator.SetBool("movingDown", true);
            CheckShootingDirection();
        }
        else if (Input.GetKey(KeyCode.D) == true)
        {
            animator.speed.Equals(1f);
            animator.SetBool("movingRight", true);
            CheckShootingDirection();
        }
        else
        {
            animator.SetBool("movingRight", false);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingUp", false);
            animator.SetBool("movingDown", false);
        }
        
        moveDirection = FindMoveVector();
        shootDirection = FindShootVector();
        attemptShoot = shootDirection.magnitude > 0.0f;
    }
    //Face the player in the direction that the player is shooting
    void CheckShootingDirection()
    {
        if (Input.GetKey(KeyCode.LeftArrow) ||
            Input.GetKey(KeyCode.RightArrow) || 
            Input.GetKey(KeyCode.UpArrow) || 
            Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("movingRight", false);
            animator.SetBool("movingLeft", false);
            animator.SetBool("movingUp", false);
            animator.SetBool("movingDown", false);
            ChangeAnimation();
        }
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

    void ChangeAnimation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            animator.SetBool("movingRight", false);
            animator.SetBool("movingLeft", true);
            animator.speed.Equals(-1f);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            animator.SetBool("movingUp", true);
            animator.SetBool("movingDown", false);
            animator.speed.Equals(-1f);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            animator.SetBool("movingRight", true);
            animator.SetBool("movingLeft", false);
            animator.speed.Equals(-1f);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.SetBool("movingDown", true);
            animator.SetBool("movingUp", false);
            animator.speed.Equals(-1f);
        }
    }

    void KatanaAnimation()
    {

    }

    public void TeleportPlayer()
    {
        float[] numbers = new float[] {0f, 3f, -3f};
        float xPos = numbers[Random.Range(0, 2)];
        float yPos = numbers[Random.Range(0, 2)];
        body.position = new Vector2(xPos, yPos);
        Debug.Log(body.position);
    }

    void PlayerDeath()
    {
        GetComponent<HealthScript>().health = 1;
        print("player died");
        //run other code
    }
}
