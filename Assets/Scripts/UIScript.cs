using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class UIScript : MonoBehaviour
{
    public Image imageBox;
    [SerializeField] Text healthText;
    [SerializeField] Text coinText;
    public Sprite SB;
    public Sprite HMG;
    public Sprite SG;
    public Sprite C;
    public Sprite WW;
    ManagerScript mgRef = new ManagerScript();
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
            imageBox.sprite = SB;
        }
        else if (stateRef.heldPowerUp == "Heavy Machine Gun")
        {
            imageBox.sprite = HMG;
        }
        else if (stateRef.heldPowerUp == "Shotgun")
        {
            imageBox.sprite = SG;
        }
        else if (stateRef.heldPowerUp == "Coffee")
        {
            imageBox.sprite = C;
        }
        else if (stateRef.heldPowerUp == "Wagon Wheel")
        {
            imageBox.sprite = WW;
        }
    }
}
