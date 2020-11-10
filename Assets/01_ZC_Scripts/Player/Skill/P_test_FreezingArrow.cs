using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_test_FreezingArrow : Skill_Projectile
{
    public float damage = 1;

    public override void ProjectileSkillEffect(Collider other, GameObject main)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            CCManager.instance.StartFreezing(other, 3.0f, DamageManager.instance.CharacterDamage(main.transform).damage * damage, 1f);
            damageableObject.TakeHit(DamageManager.instance.CharacterDamage(main.transform).damage * damage);
            UIManager.instance.SetFloating(main.transform, DamageManager.instance.CharacterDamage(main.transform), damage);
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
