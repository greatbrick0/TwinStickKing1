using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    // Code that is on all enemies, and functions that can be called by enemy manager
    // such as damaging the player

    void Update()
    {
        //print(GetComponent<HealthScript>().health);
        if(GetComponent<HealthScript>().health <= 0)
        {
            
            Destroy(this.gameObject);
        }
    }
}
