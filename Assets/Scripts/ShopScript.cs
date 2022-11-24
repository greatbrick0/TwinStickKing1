using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour
{
    string state = "descend";
    float stateDuration = 0.0f;
    public float flySpeed = 3.0f;
    void Start()
    {
        
    }

    void Update()
    {
        if(state == "descend")
        {
            transform.position += new Vector3(0, -1, 0) * flySpeed * Time.deltaTime;
            stateDuration += 1.0f * Time.deltaTime;

            if(stateDuration >= 3.0f)
            {
                state = "float";
                stateDuration = 0.0f;
            }
        }
    }
}
