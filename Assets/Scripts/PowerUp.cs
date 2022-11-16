using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameObject powerUp;
    GameObject player;
    public string powerUpType;
    
    void Start()
    {
        powerUp = GetComponent<GameObject>();
        player = GameObject.Find("Player");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == player)
        {
            player = collision.gameObject;
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerState>().badges += 1;
            collision.gameObject.GetComponent<PlayerState>().PickUpPowerUp(powerUpType);
        }
    }
}
