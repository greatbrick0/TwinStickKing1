using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpgradePickUp : MonoBehaviour
{
    public ShopScript shopRef;
    public ManagerScript managerRef;
    public List<Sprite> spriteList = new List<Sprite>();
    public int cost = 5;
    bool purchased = false;

    string statType = "speed";
    int upgradeLevel = 0;

    public void SetSprite()
    {
        int spriteIndex = 0;
        if(statType == "speed")
        {
            spriteIndex = 0;
        }
        else if(statType == "attack")
        {
            spriteIndex = 2;
        }
        else if(statType == "damage")
        {
            spriteIndex = 5;
        }
        else if(statType == "lives")
        {
            spriteIndex = 8;
        }
        else if(statType == "kit")
        {
            spriteIndex = 9;
        }
        spriteIndex += upgradeLevel;

        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteIndex];
        transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = cost.ToString();
    }

    public void SetData(string stat, int level, ManagerScript manager)
    {
        managerRef = manager;
        statType = stat;
        upgradeLevel = level;
        cost = managerRef.upgradeCosts[statType][managerRef.upgradeLevels[statType]];
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!purchased)
        {
            if (collision.gameObject.GetComponent<PlayerScript>() != null)
            {
                if (managerRef.coinAmount >= cost)
                {
                    if (statType == "lives")
                    {
                        managerRef.liveAmount += 1;
                    }
                    else if (statType == "kit")
                    {
                        collision.gameObject.GetComponent<PlayerState>().PickUpPowerUp("Sheriff Badge");
                    }
                    else
                    {
                        ApplyStatUpgrade(collision.gameObject.GetComponent<PlayerScript>());
                    }

                    managerRef.coinAmount -= cost;
                    purchased = true;
                    shopRef.Purchased();
                    Destroy(this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>());
                    transform.GetChild(1).gameObject.GetComponent<TextMeshPro>().text = "";
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerScript>() != null)
        {
            if (purchased)
            {
                shopRef.RemoveUpgrades();
                shopRef.SpawnUpgrades();
            }
        }
    }

    void ApplyStatUpgrade(PlayerScript playerRef)
    {
        print(statType);
        if(statType == "speed")
        {
            playerRef.baseSpeed += 1.2f;
        }
        else if(statType == "attack")
        {
            playerRef.baseShootSpeed -= 0.07f;
        }
        else if(statType == "damage")
        {
            playerRef.baseDamage += 1;
        }
        managerRef.upgradeLevels[statType] += 1;
    }
}
