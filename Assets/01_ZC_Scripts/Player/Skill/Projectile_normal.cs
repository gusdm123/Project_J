using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile_normal : MonoBehaviour
{
    public Skill_Projectile current_Skill;
    public bool buffSkillCheck = false;
    public GameObject muzzleEffect;
    float speed = 10f;

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
        if (other.tag == "Enemy")
        {
            current_Skill.ProjectileSkillEffect(other,gameObject);
        }
    }
}
