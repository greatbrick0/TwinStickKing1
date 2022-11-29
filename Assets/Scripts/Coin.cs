using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public ManagerScript mgRef;

    public int value = 10;

    void Start()
    {
        mgRef = GameObject.Find("Manager").GetComponent<ManagerScript>();
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            collision.gameObject.GetComponent<PlayerScript>().PickUpCoin(value);
            mgRef.coinAmount += value;
            Destroy(this.gameObject);
        }
    }
}
