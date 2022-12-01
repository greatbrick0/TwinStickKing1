using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject upgradeObj;
    GameObject newUpgradeObj;
    ManagerScript managerRef;

    public List<Sprite> droneSprites;
    public string state = "descend";
    float stateDuration = 0.0f;
    public float flySpeed = 5.0f;
    public float flyAccel = 1.6f;
    public float floatSpeed = 0.3f;

    int purchaseAmount = 0;
    public int maxPurchases = 2;

    void Start()
    {
        managerRef = transform.parent.GetComponent<ManagerScript>();
    }

    void Update()
    {
        if(state == "float")
        {
            transform.GetChild(0).position += new Vector3(0, Mathf.Sin(stateDuration + 3), 0) * floatSpeed * Time.deltaTime;
            stateDuration += 1.0f * Time.deltaTime;
        }
        else if(state == "descend")
        {
            flySpeed -= flyAccel * 1.0f * Time.deltaTime;
            transform.position += new Vector3(0, -1, 0) * flySpeed * Time.deltaTime;
            stateDuration += 1.0f * Time.deltaTime;

            if(stateDuration >= 3.0f)
            {
                state = "float";
                stateDuration = 0.0f;
                SpawnUpgrades();
            }
        }
        else if (state == "ascend")
        {
            flySpeed += flyAccel * 1.0f * Time.deltaTime;
            transform.position += new Vector3(0, 1, 0) * flySpeed * Time.deltaTime;
            stateDuration += 1.0f * Time.deltaTime;

            if (stateDuration >= 3.0f)
            {
                Destroy(this.gameObject);
            }
        }
        transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = droneSprites[Mathf.FloorToInt((stateDuration * 8) % droneSprites.Count)];
    }

    public void SpawnUpgrades()
    {
        //print(managerRef.upgradeLevels["speed"] + " " + managerRef.upgradeCosts["speed"].Count);
        if(managerRef.upgradeLevels["speed"] < managerRef.upgradeCosts["speed"].Count)
        {
            InstanceUpgrade("speed", new Vector2(-1.4f, -1.6f));
        }
        else
        {
            InstanceUpgrade("lives", new Vector2(-1.4f, -1.6f));
        }

        if (managerRef.upgradeLevels["attack"] < managerRef.upgradeCosts["attack"].Count)
        {
            InstanceUpgrade("attack", new Vector2(0.0f, -1.9f));
        }
        else
        {
            InstanceUpgrade("kit", new Vector2(0.0f, -1.9f));
        }

        if (managerRef.upgradeLevels["damage"] < managerRef.upgradeCosts["damage"].Count)
        {
            InstanceUpgrade("damage", new Vector2(1.4f, -1.6f));
        }
        else
        {
            InstanceUpgrade("kit",  new Vector2(1.4f, -1.6f));
        }


    }

    void InstanceUpgrade(string statType, Vector2 pos)
    {
        newUpgradeObj = Instantiate(upgradeObj, transform.parent);
        newUpgradeObj.GetComponent<UpgradePickUp>().SetData(statType, managerRef.upgradeLevels[statType], managerRef);
        newUpgradeObj.GetComponent<UpgradePickUp>().SetSprite();
        newUpgradeObj.GetComponent<UpgradePickUp>().shopRef = this;
        newUpgradeObj.transform.position = transform.position + new Vector3(pos.x, pos.y, 0);
    }

    public void Purchased()
    {
        purchaseAmount += 1;
        GetComponent<AudioSource>().Play();
        if(purchaseAmount >= maxPurchases)
        {
            managerRef.CreateArrow();
            CloseShop();
        }
    }

    public void CloseShop()
    {
        //print("shop closed");
        RemoveUpgrades();
        state = "ascend";
        stateDuration = 0.0f;
    }

    public void RemoveUpgrades()
    {
        for (int ii = 0; ii < transform.parent.childCount; ii++)
        {
            //print(transform.parent.GetChild(ii).gameObject.GetComponent<UpgradePickUp>() != null);
            if (transform.parent.GetChild(ii).gameObject.GetComponent<UpgradePickUp>() != null)
            {
                Destroy(transform.parent.GetChild(ii).gameObject);
            }
        }
    }
}
