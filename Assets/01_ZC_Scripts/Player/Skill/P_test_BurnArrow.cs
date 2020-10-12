using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_test_BurnArrow : Skill_Projectile
{
    public GameObject hitEffect;
    public float damage = 1;
    public float burnDamage = 0.5f;

    public override void ProjectileSkillEffect(Collider other, GameObject main)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            CCManager.instance.StartBurn(other, 3.0f, burnDamage, 1f);
            damageableObject.TakeHit(damage);
        }

        GameObject playhitEffect = Instantiate(hitEffect, main.transform.position, main.transform.rotation);
        playhitEffect.SetActive(true);
        Destroy(playhitEffect, 1.0f);

        if (Weapon_gun.instance.penetrationAttackCheck == true)
        {
            damage = damage * Weapon_gun.instance.penetrationDamage;
        }
        else
        {
            GameObject.Destroy(main);
        }
    }

    public override void BuffSkillEffect()
    {
        throw new System.NotImplementedException();
    }
}
