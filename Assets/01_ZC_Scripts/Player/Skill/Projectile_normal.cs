using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile_normal : MonoBehaviour
{
    public Skill_Projectile current_Skill;
    public bool buffSkillCheck = false;
    public bool aoeSkillCheck = false;
    public GameObject muzzleEffect;
    public float skillCooldown = 5f;

    public float skillBullet = 10;

    private float speed = 10f;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Plane")
        {
            current_Skill.ProjectileSkillEffect(other,gameObject);
        }
    }
}
