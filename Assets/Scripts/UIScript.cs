using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Image imageBox;
    public Image [] upgradeBox;
    [SerializeField] Text healthText;
    [SerializeField] Text coinText;
    public Sprite kitSprite;
    public Sprite machineGunSprite;
    public Sprite shotGunSprite;
    public Sprite needleSprite;
    public Sprite octoSprite;
    public Sprite nukeSprite;
    public Sprite teleSprite;
    public Sprite swordSprite;
    public ManagerScript mgRef;
    PlayerState stateRef;
    string item;

    public Sprite[] speedUP;
    public Sprite[] attackUP;
    public Sprite[] damageUP;
    private void Awake()
    {
        stateRef = GameObject.Find("Player").GetComponent<PlayerState>();
    }
    private void Start()
    {
        for (int i = 0; i < 3; i++)
        {
            var tempColor = imageBox.color;
            tempColor.a = 0f;
            upgradeBox[i].color = tempColor;
        }
    }
    void Update()
    {
        healthText.text = $"x{mgRef.liveAmount.ToString()}";
        coinText.text = $"x{mgRef.coinAmount.ToString()}";
        if (stateRef.heldPowerUp == "none")
        {
            var tempColor = imageBox.color;
            tempColor.a = 0f;
            imageBox.color = tempColor;
        }
        else
        {
            var tempColor = imageBox.color;
            tempColor.a = 255f;
            imageBox.color = tempColor;
            ChangePowerUpImage();
        }
        for (int i = 0; i < 3; i++)
        {
            var tempColor = imageBox.color;
            tempColor.a = 0f;
            upgradeBox[i].color = tempColor;
        }
        ChangeUpgradeBox();
    }
    void ChangePowerUpImage()
    {
        if (stateRef.heldPowerUp == "Sheriff Badge")
        {
            imageBox.sprite = kitSprite;
        }
        else if (stateRef.heldPowerUp == "Heavy Machine Gun")
        {
            imageBox.sprite = machineGunSprite;
        }
        else if (stateRef.heldPowerUp == "Shotgun")
        {
            imageBox.sprite = shotGunSprite;
        }
        else if (stateRef.heldPowerUp == "Coffee")
        {
            imageBox.sprite = needleSprite;
        }
        else if (stateRef.heldPowerUp == "Wagon Wheel")
        {
            imageBox.sprite = octoSprite;
        }
        else if (stateRef.heldPowerUp == "Smoke Bomb")
        {
            imageBox.sprite = teleSprite;
        }
        else if (stateRef.heldPowerUp == "Tomb Stone")
        {
            imageBox.sprite = swordSprite;
        }
        else if (stateRef.heldPowerUp == "Screen Nuke")
        {
            imageBox.sprite = nukeSprite;
        }
    }

    void ChangeUpgradeBox()
    {
        for (int i = 0; i < 3; i++)
        {
            //Speed imagebox
            if (mgRef.upgradeLevels["speed"].Equals(i + 1))
            {
                var tempColor = imageBox.color;
                tempColor.a = 255f;
                upgradeBox[0].color = tempColor;
                upgradeBox[0].sprite = speedUP[i];
            }
            //Attack imagebox
            if (mgRef.upgradeLevels["attack"].Equals(i + 1))
            {
                var tempColor = imageBox.color;
                tempColor.a = 255f;
                upgradeBox[1].color = tempColor;
                upgradeBox[1].sprite = attackUP[i];
            }
            //Damage imagebox
            if (mgRef.upgradeLevels["damage"].Equals(i + 1))
            {
                upgradeBox[2].sprite = damageUP[i];
                var tempColor = imageBox.color;
                tempColor.a = 255f;
                upgradeBox[2].color = tempColor;
            }
        }
    }
}
