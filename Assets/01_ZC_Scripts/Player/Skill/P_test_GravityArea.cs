using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P_test_GravityArea : Skill_stat
{
    public GameObject target;

    private void Update()
    {
        duration_time -= Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (other.GetComponent<LivingEntity>() != null)
            {
                if (other.GetComponent<LivingEntity>().gravityCheck == false)
                {
                    other.GetComponent<Enemy>().gravity_time = duration_time;

                    StartCoroutine(GravityEffect(other, duration_time));
                }
            }
        }
    }

    private IEnumerator GravityEffect(Collider enemy,float duration)
    {
        Vector3 dir;
        float distance;
        Vector3 direction;

        while (true)
        {
            if (enemy != null && target != null)
            {
                dir = (target.transform.position - enemy.transform.position);
                dir.y = 0;
                distance = dir.magnitude;
                direction = dir / distance;
 
                enemy.GetComponent<Rigidbody>().MovePosition(enemy.transform.position + direction * Time.deltaTime);
            }

            yield return new WaitForSeconds(Time.deltaTime);
            duration -= Time.deltaTime;

            if (duration <= 0)
                break;           
        }
    }
}
