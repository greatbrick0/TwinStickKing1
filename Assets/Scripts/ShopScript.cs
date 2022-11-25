using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    public GameObject upgradeObj;
    GameObject newUpgradeObj;

    public string state = "descend";
    float stateDuration = 0.0f;
    public float flySpeed = 5.0f;
    public float flyAccel = 1.6f;
    public float floatSpeed = 0.3f;

    int purchaseAmount = 0;
    public int maxPurchases = 2;

    void Start()
    {
        
    }

    void Update()
    {
        if(state == "float")
        {
            transform.position += new Vector3(0, Mathf.Sin(stateDuration + 3), 0) * floatSpeed * Time.deltaTime;
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
    }

    void SpawnUpgrades()
    {
        InstanceUpgrade("speed", 0, new Vector2(-1.1f, 1.3f));
        InstanceUpgrade("attack", 0, new Vector2(-1.1f, 0));
        InstanceUpgrade("damage", 0, new Vector2(-1.1f, -1.3f));
    }

    void InstanceUpgrade(string statType, int level, Vector2 pos)
    {
        newUpgradeObj = Instantiate(upgradeObj, transform.parent);
        newUpgradeObj.GetComponent<UpgradePickUp>().SetSprite(statType, level);
        newUpgradeObj.transform.position = transform.position + new Vector3(pos.x, pos.y, 0);
    }

    public void Purchased()
    {
        purchaseAmount += 1;
        if(purchaseAmount >= maxPurchases)
        {
            state = "ascend";
            stateDuration = 0.0f;
        }
    }
}
