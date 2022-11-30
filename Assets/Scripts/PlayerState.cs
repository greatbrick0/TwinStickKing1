using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

public class PlayerState : MonoBehaviour
{
    public GameObject [] enemy;
    public string [] powerUps = new string [5];
    public PlayerScript scriptRef;
    public GunScript gun;//for sake of testing
    public AudioSource powerUpSound;
    public int badges = 0;
    public string heldPowerUp = "none";

    float speedBoostTime = 0.0f;
    float attackBoostTime = 0.0f;
    float shotgunTime = 0.0f;
    float octoShotTime = 0.0f;
    public float swordTime = 0.0f;
    public float smokebombTime = 0.0f;
    void Update()
    {
        scriptRef.shootSpeedMod = 1.0f;
        scriptRef.bulletNumMod = 1;
        scriptRef.moveSpeedMod = 1.0f;

        if (shotgunTime >= 0.0f)
        {
            scriptRef.shootSpeedMod *= 1.3f; // slower attack speed
            scriptRef.bulletNumMod = 3;
            shotgunTime -= 1.0f * Time.deltaTime;
        }
        if(attackBoostTime >= 0.0f)
        {
            scriptRef.shootSpeedMod *= 0.4f; // faster attack speed
            attackBoostTime -= 1.0f * Time.deltaTime;
        }
        if(speedBoostTime >= 0.0f)
        {
            scriptRef.moveSpeedMod *= 2.0f;
            speedBoostTime -= 1.0f * Time.deltaTime;
        }
        if(octoShotTime >= 0.0f)
        {
            gun.wagonwheel = true;
            octoShotTime -= 1.0f * Time.deltaTime;
        }
        else
        {
            gun.wagonwheel = false;
        }
        if (smokebombTime >= 0.0f)
        {
            smokebombTime -= 1.0f * Time.deltaTime;
        }
        if (swordTime >= 0.0f)
        {
            swordTime -= 1.0f * Time.deltaTime;
        }
                
        if (Input.GetKey(KeyCode.Space) && heldPowerUp != "none")
        {
            UsePowerUp(heldPowerUp);
            heldPowerUp = "none";
        }
    }

    private void Start()
    {
        scriptRef = gameObject.GetComponent<PlayerScript>();
        gun = GetComponent<GunScript>();
    }

    void Coffee()
    {
        speedBoostTime = 12.0f;
    }

    void HeavyMachineGun()
    {
        attackBoostTime = 16.0f;
    }

    void ScreenNuke()
    {
        GameObject.Find("EnemyManager").GetComponent<EnemyManager>().KillChildren();
        /*for (int i = 0; i <= 2; i++)      Not sure what any of this is. im just gunna blow everyone up with enemyManager.
        {
            if (enemy[i].GetComponentInChildren<SpriteRenderer>().isVisible)
            {
                enemy[i].GetComponent<HealthScript>().TakeDamage(3);
                print($" enemy health: {enemy[i].GetComponent<HealthScript>().health}");
            }
            print(i);
        } */
        
    }

    void Shotgun()
    {
        shotgunTime = 12.0f;
    }

    void SmokeBomb()
    {
        scriptRef.TeleportPlayer();
        smokebombTime = 8.0f;
    }

    void TombStone()
    {
        swordTime = 10.0f;
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
    }

    public void PickUpPowerUp(string pType)
    {
        badges += 1;
        if(heldPowerUp == "none")
        {
            heldPowerUp = pType;
        }
        else
        {
            UsePowerUp(pType);
        }
    }

    void UsePowerUp(string usedPowerUp)
    {
        if(usedPowerUp != "none")
        {
            powerUpSound.Play();
            if (badges > 0 && usedPowerUp == "Sheriff Badge")
            {
                SheriffBadge();
            }
            else if (badges > 0 && usedPowerUp == "Heavy Machine Gun")
            {
                HeavyMachineGun();
            }
            else if (badges > 0 && usedPowerUp == "Shotgun")
            {
                Shotgun();
            }
            else if (badges > 0 && usedPowerUp == "Coffee")
            {
                Coffee();
            }
            else if (badges > 0 && usedPowerUp == "Wagon Wheel")
            {
                WagonWheel();
            }
            else if (badges > 0 && usedPowerUp == "Smoke Bomb")
            {
                SmokeBomb();
            }
            else if (badges > 0 && usedPowerUp == "Tomb Stone")
            {
                TombStone();
            }
            else if(badges > 0 && usedPowerUp == "Screen Nuke")
            {
                ScreenNuke();
            }
            badges--;
        }
    }
    //This Does Not Work, Needs To Be Fixed
    void OnCollisionEnter(Collision collision)
    {
        GameObject ob = collision.gameObject;
        Debug.Log($"Player Hit {ob.name}");

        if (ob.tag == "healthbody" && swordTime >= 0.0f)
        {
            ob.GetComponent<HealthScript>().TakeDamage(3);
        }
    }
}
