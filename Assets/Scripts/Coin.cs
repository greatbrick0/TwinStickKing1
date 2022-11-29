using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ManagerScript mgRef;
    GameObject player;
    void Start()
    {
        mgRef = GameObject.Find("Manager").GetComponent<ManagerScript>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == player)
        {
            player = collision.gameObject;
            if (this.gameObject.name == "10Credit")
            {
                mgRef.coinAmount += 10;
            }
            if (this.gameObject.name == "50Credit")
            {
                mgRef.coinAmount += 50;
            }
            Destroy(this.gameObject);
        }
    }
}
