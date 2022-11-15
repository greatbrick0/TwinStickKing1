using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Code that is on all enemies, and functions that can be called by enemy manager
    // such as damaging the player
    public GameObject eManRef; //Reference to EnemyManager

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
            eManRef.GetComponent<EnemyManager>().enemyDies();
            Destroy(this.gameObject);
        }
    }
}
