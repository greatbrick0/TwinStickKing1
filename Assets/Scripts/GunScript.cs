using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    public GameObject bulletRef;
    public float spreadAngle = Mathf.PI / 24;
    public float cooldown = 1.0f;
    float cooldownTime;
    public float projectileSpeed = 8;
    public List<string> targetTeam = new List<string>();

    public bool wagonwheel;
    void Start()
    {
        cooldownTime = cooldown;
    }

    void Update()
    {
        cooldownTime -= 1.0f * Time.deltaTime;
    }

    public bool Shoot(Vector2 mainDirection, int damageArg, int sprayAmount)
    {
        GameObject newBullet;
        mainDirection.Normalize();
        Vector2 offset;

        if(cooldownTime <= 0.0f)
        {
            
            for (int ii = 0; ii < sprayAmount; ii++)
            {
                if(!wagonwheel)
                {
                //print(spreadAngle * (ii - ((sprayAmount - 1) / 2.0f)));
                offset = VectorRotate(mainDirection, Mathf.PI / 2.0f);
                offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                newBullet = Instantiate(bulletRef);
                newBullet.transform.position = transform.position;
                newBullet.GetComponent<BulletScript>().direction = mainDirection + offset;
                newBullet.GetComponent<BulletScript>().speed = projectileSpeed;
                newBullet.GetComponent<BulletScript>().targetTeam = targetTeam;
                newBullet.GetComponent<BulletScript>().damage = damageArg;
                }
                //very unoptimized way to implementation of Wagon Wheel
                else if (wagonwheel)
                {
                    for(int jj = 0; jj < 8; jj++)
                    {
                        newBullet = Instantiate(bulletRef);
                        newBullet.transform.position = transform.position;
                        offset = VectorRotate(new Vector2(0, 1), Mathf.PI / 2.0f);
                        offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                        newBullet.GetComponent<BulletScript>().direction = VectorRotate(new Vector2(0, 1) + offset, Mathf.PI/4 * jj);
                        newBullet.GetComponent<BulletScript>().speed = projectileSpeed;
                        newBullet.GetComponent<BulletScript>().targetTeam = targetTeam;
                        newBullet.GetComponent<BulletScript>().damage = damageArg;
                    }
                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Reload()
    {
        cooldownTime = cooldown;
    }

    public void Reload(float newCooldown)
    {
        cooldownTime = newCooldown;
    }

    Vector2 VectorRotate(Vector2 v, float theta)
    {
        return new Vector2(
            v.x * Mathf.Cos(theta) - v.y * Mathf.Sin(theta),
            v.x * Mathf.Sin(theta) + v.y * Mathf.Cos(theta)
        );
    }
}
