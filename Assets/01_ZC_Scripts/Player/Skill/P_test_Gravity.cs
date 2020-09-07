using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_test_Gravity : Skill_Projectile
{
    public GameObject hitEffect;
    public GameObject gravityArea;
    public float damage = 1;

    public override void ProjectileSkillEffect(Collider other, GameObject main)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            CCManager.instance.StartGravity(main.transform.position,gravityArea, 3f);
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
