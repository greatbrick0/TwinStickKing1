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
        GameObject[] newBullet = new GameObject[4]; //made newbullts an array so I can perform my shenanigans
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
                newBullet[0] = Instantiate(bulletRef);
                newBullet[0].transform.position = transform.position;
                newBullet[0].GetComponent<BulletScript>().direction = mainDirection + offset;
                newBullet[0].GetComponent<BulletScript>().speed = projectileSpeed;
                newBullet[0].GetComponent<BulletScript>().targetTeam = targetTeam;
                newBullet[0].GetComponent<BulletScript>().damage = damageArg;
                }
                //very unoptimized way to implementation of Wagon Wheel
                if (wagonwheel)
                {
                    newBullet[0] = Instantiate(bulletRef);
                    newBullet[0].transform.position = transform.position;
                    offset = VectorRotate(new Vector2(0, 1), Mathf.PI / 2.0f);
                    offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                    newBullet[0].GetComponent<BulletScript>().direction = new Vector2(0,1) + offset;
                    newBullet[0].GetComponent<BulletScript>().speed = projectileSpeed;
                    newBullet[0].GetComponent<BulletScript>().targetTeam = targetTeam;
                    newBullet[0].GetComponent<BulletScript>().damage = damageArg;

                    newBullet[1] = Instantiate(bulletRef);
                    newBullet[1].transform.position = transform.position;
                    offset = VectorRotate(new Vector2(0, -1), Mathf.PI / 2.0f);
                    offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                    newBullet[1].GetComponent<BulletScript>().direction = new Vector2(0, -1) + offset;
                    newBullet[1].GetComponent<BulletScript>().speed = projectileSpeed;
                    newBullet[1].GetComponent<BulletScript>().targetTeam = targetTeam;
                    newBullet[1].GetComponent<BulletScript>().damage = damageArg;

                    newBullet[2] = Instantiate(bulletRef);
                    newBullet[2].transform.position = transform.position;
                    offset = VectorRotate(new Vector2(1, 0), Mathf.PI / 2.0f);
                    offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                    newBullet[2].GetComponent<BulletScript>().direction = new Vector2(1, 0) + offset;
                    newBullet[2].GetComponent<BulletScript>().speed = projectileSpeed;
                    newBullet[2].GetComponent<BulletScript>().targetTeam = targetTeam;
                    newBullet[2].GetComponent<BulletScript>().damage = damageArg;

                    newBullet[3] = Instantiate(bulletRef);
                    newBullet[3].transform.position = transform.position;
                    offset = VectorRotate(new Vector2(-1, 0), Mathf.PI / 2.0f);
                    offset *= spreadAngle * (ii - ((sprayAmount - 1) / 2.0f));
                    newBullet[3].GetComponent<BulletScript>().direction = new Vector2(-1, 0) + offset;
                    newBullet[3].GetComponent<BulletScript>().speed = projectileSpeed;
                    newBullet[3].GetComponent<BulletScript>().targetTeam = targetTeam;
                    newBullet[3].GetComponent<BulletScript>().damage = damageArg;
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
