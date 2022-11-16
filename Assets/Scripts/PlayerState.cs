using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    public PlayerScript scriptRef;

    public int badges = 0;
    public string heldPowerUp = "none";

    float speedBoostTime = 0.0f;
    float attackBoostTime = 0.0f;
    float shotgunTime = 0.0f;
    float octoShotTime = 0.0f;
    float swordTime = 0.0f;
    
    void Update()
    {
        scriptRef.shootSpeedMod = 1.0f;
        scriptRef.bulletNumMod = 1;
        scriptRef.moveSpeedMod = 1.0f;

        if (shotgunTime >= 0.0f)
        {
            scriptRef.shootSpeedMod *= 1.5f; // slower attack speed
            scriptRef.bulletNumMod = 3;
            shotgunTime -= 1.0f * Time.deltaTime;
        }
        if(attackBoostTime >= 0.0f)
        {
            scriptRef.shootSpeedMod *= 0.4f;
            attackBoostTime -= 1.0f * Time.deltaTime;
        }
        if(speedBoostTime >= 0.0f)
        {
            scriptRef.moveSpeedMod *= 0.5f; // faster attack speed
            speedBoostTime -= 1.0f * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            UsePowerUp();
        }
    }

    private void Start()
    {
        scriptRef = gameObject.GetComponent<PlayerScript>();
    }

    void Coffee()
    {
        
        Debug.Log($"{heldPowerUp}: Used");
    }

    void HeavyMachineGun()
    {
        scriptRef.shootSpeedMod *= 0.5f;

        attackBoostTime = 16.0f;
        Debug.Log($"{heldPowerUp}: Used");
    }

    void ScreenNuke()
    {

    }

    void Shotgun()
    {
        shotgunTime = 12.0f;
        Debug.Log($"{heldPowerUp}: Used");
    }

    void SmokeBomb()
    {

    }

    void TombStone()
    {
        swordTime = 6.0f;
    }

    void WagonWheel()
    {
        octoShotTime = 12.0f;
    }

    void SheriffBadge()
    {
        speedBoostTime = 12.0f;
        attackBoostTime = 12.0f;
        shotgunTime = 12.0f;

        Debug.Log($"{heldPowerUp}: Used");
    }

    public void PickUpPowerUp(string pType)
    {

        Debug.Log($"Player Collected {pType}");
        UsePowerUp();
        heldPowerUp = pType;
    }

    void UsePowerUp()
    {
        if (badges > 0 && heldPowerUp == "Sheriff Badge")
        {
            SheriffBadge();
        }
        else if (badges > 0 && heldPowerUp == "Heavy Machine Gun")
        {
            HeavyMachineGun();
        }
        else if (badges > 0 && heldPowerUp == "Shotgun")
        {
            Shotgun();
        }
        else if (badges > 0 && heldPowerUp == "Coffee")
        {
            Coffee();
        } // add more power ups later
        else
        {
            badges++;
            //print(heldPowerUp + badges);
        }
        badges--;
        heldPowerUp = "none";
    }
}
