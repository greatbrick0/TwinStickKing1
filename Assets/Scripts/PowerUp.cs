using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    GameObject powerUp;
    GameObject player;
    public static string _powerUp;
    // Start is called before the first frame update
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
            PlayerState.badges = PlayerState.badges + 1;
            _powerUp = this.name;
            Debug.Log($"Player Collected {_powerUp}");
        }
    }
}
