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
        CCManager.instance.StartDamageBuff(buffTime); 
    }
}
