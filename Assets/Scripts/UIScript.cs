using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Image imageBox;
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
    private void Awake()
    {
        stateRef = GameObject.Find("Player").GetComponent<PlayerState>();
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
}
