using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile_normal : MonoBehaviour
{
    public Skill_Projectile current_Skill;
    public bool buffSkillCheck = false;
    public bool aoeSkillCheck = false;
    public bool reflectCheck = true;
    public GameObject muzzleEffect;
    public float skillCooldown = 5f;

    public float skillBullet = 10;

    private float speed = 10f;
    private int bounceStack = 0;

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
        if (other.tag == "Enemy" || other.tag == "Plane") // 바닥 혹은 적과 충돌했을 경우 스킬 효과
        {
            current_Skill.ProjectileSkillEffect(other,gameObject);
        }

        if (other.tag == "SideWall" && Weapon_gun.instance.bounceAttackCheck == true)
        {
            bounceStack++; // 벽에 부딪힐때 마다 스택을 쌓아준다.

            if (bounceStack == 3) // 3번째 부딪힐때 충돌 이펙트와 함께 삭제
            {
                Destroy(gameObject);
                GameObject hit = Instantiate(current_Skill.hitEffect, transform.position, transform.rotation);
                hit.SetActive(true);
                Destroy(hit, 1.0f);
            }

            transform.rotation = Quaternion.Euler(0f, 180f - transform.rotation.eulerAngles.y, 0f);
        }
        else if (other.tag == "SideWall") // 벽에 부딪혔을 경우 충돌 이펙트
        {
            GameObject hit = Instantiate(current_Skill.hitEffect, transform.position, transform.rotation);
            hit.SetActive(true);
            Destroy(hit, 1.0f);
        }
    }
}
