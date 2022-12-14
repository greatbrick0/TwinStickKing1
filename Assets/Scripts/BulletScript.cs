using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    float age = 0;
    public float speed;
    public Vector2 direction;
    public int damage = 1;

    public List<string> targetTeam = new List<string>();

    public List<Sprite> bulletSprites;

    Rigidbody2D body;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        if(damage < bulletSprites.Count)
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[damage - 1];
        }
        else
        {
            transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().sprite = bulletSprites[bulletSprites.Count - 1];
        }
    }

    void Update()
    {
        age += 1.0f * Time.deltaTime;
        if(age >= 10.0f)
        {
            Destroy(this.gameObject);
        }
    }

    void FixedUpdate()
    {
        body.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameObject ob = other.gameObject;

        if(ob.tag == "wall")
        {
            Destroy(this.gameObject);
        }
        else if(ob.tag == "healthbody")
        {
            if (targetTeam.Contains(ob.GetComponent<HealthScript>().team))
            {
                damage = ob.GetComponent<HealthScript>().TakeDamage((int)damage);
                if (damage <= 0)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
