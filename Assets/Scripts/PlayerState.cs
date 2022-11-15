using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    static public int badges = 0;
    
    private void Update()
    {
        if (Input.GetKey(KeyCode.B) && badges > 0 && PowerUp._powerUp == "Sheriff Badge(Clone)")
        {
            SheriffBadge();
        }
        if (Input.GetKey(KeyCode.H) && badges > 0 && PowerUp._powerUp == "Heavy Machine Gun(Clone)")
        {
            HeavyMachineGun();
        }
        if (Input.GetKey(KeyCode.G) && badges > 0 && PowerUp._powerUp == "Shotgun(Clone)")
        {
            Shotgun();
        }
        if (Input.GetKey(KeyCode.C) && badges > 0 && PowerUp._powerUp == "Coffee(Clone)")
        {
            Coffee();
        }


    }
    public void Coffee()
    {
        PlayerScript.bodySpeed = 5;
        Debug.Log($"{PowerUp._powerUp}: Used");
    }

    // Update is called once per frame
    public void HeavyMachineGun()
    {
        PlayerScript.baseShootSpeed = 0.1f;
        Debug.Log($"{PowerUp._powerUp}: Used");
    }

    public void ScreenNuke()
    {

    }

    public void Shotgun()
    {

        PlayerScript.bulletNum = 3;
        PlayerScript.baseShootSpeed = 0.95f * PlayerScript.baseShootSpeed;
        Debug.Log($"{PowerUp._powerUp}: Used");
    }

    public void SmokeBomb()
    {

    }

    public void TombStone()
    {

    }

    public void WagonWheel()
    {

    }

    public void SheriffBadge()
    {
        PlayerScript.baseShootSpeed = 0.2f;
        PlayerScript.bulletNum = 3;
        PlayerScript.bodySpeed = 3;
        Debug.Log($"{PowerUp._powerUp}: Used");
    }
}
