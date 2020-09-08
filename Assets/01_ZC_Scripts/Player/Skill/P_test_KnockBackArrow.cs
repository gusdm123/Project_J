﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_test_KnockBackArrow : Skill_Projectile
{
    public GameObject hitEffect;
    public float damage = 1;

    public override void ProjectileSkillEffect(Collider other, GameObject main)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            CCManager.instance.StartKnockBack(other, 0.25f,3f);
            damageableObject.TakeHit(damage);
        }

        GameObject playhitEffect = Instantiate(hitEffect, main.transform.position, main.transform.rotation);
        playhitEffect.SetActive(true);
        Destroy(playhitEffect, 1.0f);

        GameObject.Destroy(main);
    }

    public override void BuffSkillEffect()
    {
        throw new System.NotImplementedException();
    }
}