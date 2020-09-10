using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public Transform normalMuzzle; // 일반 투사체 공격을 위한 머즐
    public Transform aoeMuzzle; // 범위공격을 위한 머즐

    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    private float currentBullet = 0;

    private float nextShotTime;
    private GameObject currentProjectile;

    public GameObject baseProjectile;
    public GameObject firstProjectile;
    public GameObject secondProjectile;
    public GameObject thirdProjectile;
    public GameObject forthProjectile;

    public Image[] skillCooldownImage;
    public Image[] skillBulletGage;

    public bool[] projectileCoolDownCheck = new bool[5]; // 1,2,3,4 로 스킬 관리

    public float FinalDamage = 1f;

    private void Start()
    {
        currentProjectile = baseProjectile;

        for (int i = 1; i < 5; i++)
            projectileCoolDownCheck[i] = false;
    }

    #region 투사체 세팅 함수

    public void SetFirstprojectile()
    {
        if (currentProjectile == firstProjectile)
        {
            muzzle = normalMuzzle;
            currentProjectile = baseProjectile;
        }
        else if (projectileCoolDownCheck[1] == false)
        {
            StartCoroutine(CheckCoolDown(1, firstProjectile.GetComponent<Projectile_normal>().skillCooldown));

            if (firstProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
            {
                firstProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
                StartCoroutine(StartBuffTime(1, firstProjectile.GetComponentInChildren<Skill_Projectile>().buffTime));
            }
            else
            {
                currentBullet = firstProjectile.GetComponent<Projectile_normal>().skillBullet;

                if (firstProjectile.GetComponent<Projectile_normal>().aoeSkillCheck == true)
                {
                    muzzle = aoeMuzzle;
                }
                else
                    muzzle = normalMuzzle;

                currentProjectile = firstProjectile;
            }
        }
    }

    public void SetSecondprojectile()
    {
        if (currentProjectile == secondProjectile)
        {
            muzzle = normalMuzzle;
            currentProjectile = baseProjectile;
        }
        else if (projectileCoolDownCheck[2] == false)
        {
            StartCoroutine(CheckCoolDown(2, secondProjectile.GetComponent<Projectile_normal>().skillCooldown));

            if (secondProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
            {
                secondProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
                StartCoroutine(StartBuffTime(2, secondProjectile.GetComponentInChildren<Skill_Projectile>().buffTime));
            }
            else
            {
                currentBullet = secondProjectile.GetComponent<Projectile_normal>().skillBullet;

                if (secondProjectile.GetComponent<Projectile_normal>().aoeSkillCheck == true)
                {
                    muzzle = aoeMuzzle;
                }
                else
                    muzzle = normalMuzzle;

                currentProjectile = secondProjectile;
            }
        }
    }

    public void SetThirdprojectile()
    {
        if (currentProjectile == thirdProjectile)
        {
            muzzle = normalMuzzle;
            currentProjectile = baseProjectile;
        }
        else if (projectileCoolDownCheck[3] == false)
        {
            StartCoroutine(CheckCoolDown(3, thirdProjectile.GetComponent<Projectile_normal>().skillCooldown));

            if (thirdProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
            {
                thirdProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
                StartCoroutine(StartBuffTime(3, thirdProjectile.GetComponentInChildren<Skill_Projectile>().buffTime));
            }
            else
            {
                currentBullet = thirdProjectile.GetComponent<Projectile_normal>().skillBullet;

                if (thirdProjectile.GetComponent<Projectile_normal>().aoeSkillCheck == true)
                {
                    muzzle = aoeMuzzle;
                }
                else
                    muzzle = normalMuzzle;

                currentProjectile = thirdProjectile;
            }
        }
    }

    public void SetForthprojectile()
    {
        if (currentProjectile == forthProjectile)
        {
            muzzle = normalMuzzle;
            currentProjectile = baseProjectile;
        }
        else if (projectileCoolDownCheck[4] == false)
        {
            StartCoroutine(CheckCoolDown(4, forthProjectile.GetComponent<Projectile_normal>().skillCooldown));

            if (forthProjectile.GetComponent<Projectile_normal>().buffSkillCheck == true)
            {
                forthProjectile.GetComponent<Projectile_normal>().current_Skill.BuffSkillEffect();
                StartCoroutine(StartBuffTime(4, forthProjectile.GetComponentInChildren<Skill_Projectile>().buffTime));
            }
            else
            {
                currentBullet = forthProjectile.GetComponent<Projectile_normal>().skillBullet;

                if (forthProjectile.GetComponent<Projectile_normal>().aoeSkillCheck == true)
                {
                    muzzle = aoeMuzzle;
                }
                else
                    muzzle = normalMuzzle;

                currentProjectile = forthProjectile;
            }
        }
    }

    //스킬이 버프인거를 체크 한뒤에 버프일 경우 SkillEffect 발동

    #endregion

    private void SetBulletGage()
    {
        int skillNum = 0;

        if (currentProjectile == firstProjectile)
        {
            skillNum = 0;
        }
        else if (currentProjectile == secondProjectile)
        {
            skillNum = 1;
        }
        else if (currentProjectile == thirdProjectile)
        {
            skillNum = 2;
        }
        else if (currentProjectile == forthProjectile)
        {
            skillNum = 3;
        }     

        if (currentProjectile != baseProjectile)
        {      
            skillBulletGage[skillNum].fillAmount -= 1 / currentProjectile.GetComponent<Projectile_normal>().skillBullet;
        }
    }

    private IEnumerator CheckCoolDown(int skill,float coolDown)
    {
        projectileCoolDownCheck[skill] = true;
        float currentCooldown = coolDown;
        skillCooldownImage[skill - 1].fillAmount = 1;
       
        while (true)
        {
            skillCooldownImage[skill - 1].fillAmount -= 1 * Time.deltaTime / currentCooldown;
            yield return new WaitForSeconds(Time.deltaTime);
            coolDown -= Time.deltaTime;

            if (coolDown <= 0)
                break;
        }
        
        projectileCoolDownCheck[skill] = false;
        skillBulletGage[skill - 1].fillAmount = 1f;
    }

    private IEnumerator StartBuffTime(int skill,float buffTime)
    {
        float currentBuffTime = buffTime;

        while (true)
        {
            skillBulletGage[skill - 1].fillAmount -= 1 * Time.deltaTime / currentBuffTime;
            yield return new WaitForSeconds(Time.deltaTime);
            buffTime -= Time.deltaTime;

            if (buffTime <= 0)
                break;
        }

        skillBulletGage[skill - 1].fillAmount = 1;
    }

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

            SetBulletGage();
            currentBullet--;

            if (currentBullet <= 0)
            {
                currentProjectile = baseProjectile;
                muzzle = normalMuzzle;
            }
        }
    }
}