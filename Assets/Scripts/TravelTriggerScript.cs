using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelTriggerScript : MonoBehaviour
{
    public ManagerScript managerRef;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            managerRef.FindAndMoveArenas();
        }
    }
}
