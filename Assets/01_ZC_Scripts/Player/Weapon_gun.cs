using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_gun : MonoBehaviour
{
    public Transform muzzle;
    public Projectile_normal projectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    float nextShotTime;

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile_normal newProjectile = Instantiate(projectile, muzzle.position, muzzle.rotation) as Projectile_normal;
            newProjectile.SetSpeed(muzzleVelocity);
            GameObject.Destroy(newProjectile.gameObject,3f);
        }
    }
}
