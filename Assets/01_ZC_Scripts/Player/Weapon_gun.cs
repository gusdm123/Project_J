using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_gun : MonoBehaviour
{
    public Transform muzzle;
    public GameObject muzzleEffect;
    public GameObject[] projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            GameObject newProjectile = Instantiate(projectile[0], muzzle.position, muzzle.rotation);
            GameObject playMuzzleEffect = Instantiate(muzzleEffect, muzzle.position, muzzle.rotation);
            Destroy(playMuzzleEffect, 0.5f);

            newProjectile.GetComponent<Projectile_normal>().SetSpeed(muzzleVelocity);
            GameObject.Destroy(newProjectile.gameObject,3f);
        }
    }
}
