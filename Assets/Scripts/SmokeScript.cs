using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeScript : MonoBehaviour
{
    public List<Sprite> spriteList;
    int spriteIndex = 0;
    float duration = 0.0f;
    public float animationSpeed = 0.5f;

    private void Update()
    {
        duration += 1.0f * Time.deltaTime;

        if(duration >= animationSpeed)
        {
            spriteIndex += 1;

            if(spriteIndex >= spriteList.Count)
            {
                Destroy(this.gameObject);
            }
            else
            {
                transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = spriteList[spriteIndex];
                duration = 0.0f;
            }
        }
    }
}
