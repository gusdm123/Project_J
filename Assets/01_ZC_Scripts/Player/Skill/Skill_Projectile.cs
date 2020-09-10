using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Projectile : MonoBehaviour
{
    public float buffTime = 5f;

    public abstract void ProjectileSkillEffect(Collider enemy,GameObject main);
    public abstract void BuffSkillEffect();
}
