using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlinkingScript : MonoBehaviour
{
    public float blinkSpeed = 0.4f;
    float duration = 0.0f;
    SpriteRenderer sprite;
    public bool shown = true;
    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        duration += 1.0f * Time.deltaTime;
        if(duration >= blinkSpeed)
        {
            duration = 0.0f;
            shown = !shown;
            sprite.enabled = shown;
        }
    }
}
