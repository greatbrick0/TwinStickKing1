using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ManagerScript mgRef;
    GameObject player;

    public int value = 10;

    void Start()
    {
        mgRef = GameObject.Find("Manager").GetComponent<ManagerScript>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            mgRef.coinAmount += value;
            Destroy(this.gameObject);
        }
    }
}
