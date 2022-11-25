using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePickUp : MonoBehaviour
{
    public ShopScript shopRef;
    public List<Sprite> spriteList = new List<Sprite>();
    public int cost = 10;

    public void SetSprite(string stat, int level)
    {
        int spriteIndex = 0;
        if(stat == "speed")
        {
            spriteIndex = 0;
        }
        else if(stat == "attack")
        {
            spriteIndex = 3;
        }
        else if(stat == "damage")
        {
            spriteIndex = 6;
        }
        else if(stat == "lives")
        {
            spriteIndex = 9;
        }
        else if(stat == "kit")
        {
            spriteIndex = 10;
        }
        spriteIndex += level;

        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteIndex];
    }
}
