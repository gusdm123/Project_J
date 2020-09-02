using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class B_test_DamageUp : Skill_Projectile
{
    public override void ProjectileSkillEffect(Collider enemy, GameObject main)
    {
        throw new System.NotImplementedException();
    }

    public override void BuffSkillEffect()
    {
        StartCoroutine(Buff_DamageUp(5f));      
    }

    IEnumerator Buff_DamageUp(float time)
    {
        Weapon_gun.instance.FinalDamage = 2.0f;

        yield return new WaitForSeconds(time);

        Weapon_gun.instance.FinalDamage = 1.0f;
    }
}
