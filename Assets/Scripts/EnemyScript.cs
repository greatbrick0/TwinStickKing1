using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Code that is on all enemies, and functions that can be called by enemy manager
    // such as damaging the player
    public GameObject eManRef; //Reference to EnemyManager

    public List<Sprite> frames = new List<Sprite>();
    public float frameTime = 0.4f;
    float frameTimer = 0.0f;
    int currentFrame = 0;

    bool isWalking = true;
    void Start()
    {
        eManRef = transform.parent.gameObject;
    }
    void Update()
    {
        //print(GetComponent<HealthScript>().health);
        if(GetComponent<HealthScript>().health <= 0)
        {
            eManRef.GetComponent<EnemyManager>().spawnDrop(this.transform.position);
            eManRef.GetComponent<EnemyManager>().enemyDies(this.transform.position);
            Destroy(this.gameObject);
        }

        frameTimer += 1.0f * Time.deltaTime;
        if(frameTimer >= frameTime)
        {
            SwitchFrames();
            frameTimer = 0.0f;
        }
    }

    void SwitchFrames()
    {
        if (isWalking)
        {
            currentFrame++;
            if (currentFrame >= frames.Count)
            {
                currentFrame = 0;
            }
            transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = frames[currentFrame];
        }
    }
}
