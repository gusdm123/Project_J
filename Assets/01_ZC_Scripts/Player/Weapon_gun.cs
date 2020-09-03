using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon_gun : MonoBehaviour
{
    public static Weapon_gun instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<Weapon_gun>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static Weapon_gun m_instance; // 싱글톤이 할당될 static 변수

    public Transform muzzle;

    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;

    private float nextShotTime;
    private GameObject currentProjectile;

    public GameObject baseProjectile;
    public GameObject firstProjectile;
    public GameObject secondProjectile;
    public GameObject thirdProjectile;
    public GameObject forthProjectile;

    public float FinalDamage = 1f;

    private void Start()
    {
        currentProjectile = baseProjectile;
    }

    #region 투사체 세팅 함수

    public void SetFirstprojectile()
    {
        if (firstProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
        {
            firstProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
        }
        else
        {
            if (currentProjectile == firstProjectile)
            {
                currentProjectile = baseProjectile;
            }
            else
            {
                currentProjectile = firstProjectile;
            }
        }
    }

    public void SetSecondprojectile()
    {
        if (secondProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
        {
            secondProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
        }
        else
        {
            if (currentProjectile == secondProjectile)
            {
                currentProjectile = baseProjectile;
            }
            else
            {
                currentProjectile = secondProjectile;
            }
        }
    }

    public void SetThirdprojectile()
    {
        if (thirdProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
        {
            thirdProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
        }
        else
        {
            if (currentProjectile == thirdProjectile)
            {
                currentProjectile = baseProjectile;
            }
            else
            {
                currentProjectile = thirdProjectile;
            }
        }
    }

    public void SetForthprojectile()
    {
        if (forthProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
        {
            forthProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
        }
        else
        {
            if (currentProjectile == forthProjectile)
            {
                currentProjectile = baseProjectile;
            }
            else
            {
                currentProjectile = forthProjectile;
            }
        }
    }

    //스킬이 버프인거를 체크 한뒤에 버프일 경우 SkillEffect 발동

    #endregion

    public void Shoot()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime = Time.time + msBetweenShots / 1000;
            GameObject newProjectile = Instantiate(currentProjectile, muzzle.position, muzzle.rotation);
            GameObject playMuzzleEffect = Instantiate(currentProjectile.GetComponent<Projectile_normal>().muzzleEffect, muzzle.position, muzzle.rotation);
            Destroy(playMuzzleEffect, 0.5f);

            newProjectile.GetComponent<Projectile_normal>().SetSpeed(muzzleVelocity);
            GameObject.Destroy(newProjectile.gameObject, 3f);        
        }
    }
}