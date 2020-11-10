using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCManager : MonoBehaviour
{
    public static CCManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<CCManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static CCManager m_instance; // 싱글톤이 할당될 static 변수

    public IEnumerator CC_Burn(Collider target, float duration, float damage, float effectCycle) // 화상 적에게 상태이상 지속시간 동안 [데미지 주기] 마다 [지속 데미지]
    {
        damage = damage * 0.8f;

        if (target.GetComponent<LivingEntity>().burnCheck == false) {

            target.GetComponent<LivingEntity>().burnCheck = true;

            while(true){
                yield return new WaitForSeconds(effectCycle);
                if (target != null)
                {
                    target.GetComponent<LivingEntity>().TakeHit(damage);
                    UIManager.instance.SetFloating(target.transform, DamageManager.instance.CharacterDamage(target.transform), damage);
                }
               
                duration -= effectCycle;

                if (duration <= 0)
                    break;
            }

            if (target != null)
            {
                target.GetComponent<LivingEntity>().burnCheck = false;
            }
        }
    }

    public void StartBurn(Collider target, float duration, float damage, float effectCycle)
    {
        StartCoroutine(CC_Burn(target, duration, damage, effectCycle));
    }

    public IEnumerator CC_Paralysis(Collider target, float duration, float effectCycle) // 마비 상태이상 지속시간 동안 [데미지 주기] 마다 0.5초 행동불가
    {
        if (target.GetComponent<LivingEntity>().paralysisCheck == false)
        {

            target.GetComponent<LivingEntity>().paralysisCheck = true;

            while (true)
            {
                yield return new WaitForSeconds(effectCycle);
                if (target != null)
                {
                    target.GetComponent<Enemy>().paralysis_time += 0.5f;
                }

                duration -= effectCycle;

                if (duration <= 0)
                    break;
            }

            if (target != null)
            {
                target.GetComponent<LivingEntity>().paralysisCheck = false;
            }
        }
    }

    public void StartParalysis(Collider target, float duration, float effectCycle)
    {
        StartCoroutine(CC_Paralysis(target, duration, effectCycle));
    }

    public IEnumerator CC_Freezing(Collider target, float duration, float damage, float effectCycle) // 빙결 적 행동불가 및 상태이상 지속시간 동안 [데미지 주기] 마다 [지속 데미지]
    {
        damage = damage * 0.4f;

        if (target.GetComponent<LivingEntity>().freezingCheck == false)
        {

            target.GetComponent<LivingEntity>().freezingCheck = true;
            target.GetComponent<Enemy>().freezing_time += duration;

            while (true)
            {
                yield return new WaitForSeconds(effectCycle);
                if (target != null)
                {
                    target.GetComponent<LivingEntity>().TakeHit(damage);
                    UIManager.instance.SetFloating(target.transform, DamageManager.instance.CharacterDamage(target.transform), damage);
                }

                duration -= effectCycle;

                if (duration <= 0)
                    break;
            }

            if (target != null)
            {
                target.GetComponent<LivingEntity>().freezingCheck = false;
            }
        }
    }

    public void StartFreezing(Collider target, float duration, float damage, float effectCycle)
    {
        StartCoroutine(CC_Freezing(target,duration,damage,effectCycle));
    }

    public IEnumerator CC_Darkness(Collider target, float duration) // 암흑 지속시간 동안 이동방향 반대로
    {
        if (target.GetComponent<LivingEntity>().darknessCheck == false)
        {
            target.GetComponent<LivingEntity>().darknessCheck = true;
            target.transform.eulerAngles = new Vector3(0f, 90f, 0f);

            while (true)
            {
                yield return new WaitForSeconds(Time.deltaTime);

                duration -= Time.deltaTime;

                if (duration <= 0)
                    break;
            }

            if (target != null)
            {
                target.transform.eulerAngles = new Vector3(0f, -90f, 0f);
                target.GetComponent<LivingEntity>().darknessCheck = false;
            }
        }
    }

    public void StartDarkness(Collider target, float duration)
    {
        StartCoroutine(CC_Darkness(target, duration));
    }

    public IEnumerator CC_KnockBack(Collider target, float duration,float power) // 넉백 적을 뒤로 밀어냄 
    {
        if (target.GetComponent<LivingEntity>().knockBackCheck == false)
        {
            target.GetComponent<LivingEntity>().knockBackCheck = true;

            while (true)
            {
                if (target != null)
                {
                    target.transform.Translate(Vector3.back * Time.deltaTime * power);
                }

                yield return new WaitForSeconds(Time.deltaTime);

                duration -= Time.deltaTime;

                if (duration <= 0)
                    break;
            }

            if (target != null)
            {
                target.GetComponent<LivingEntity>().knockBackCheck = false;
            }
        }
    }

    public void StartKnockBack(Collider target, float duration,float power)
    {
        StartCoroutine(CC_KnockBack(target, duration, power));
    }

    public IEnumerator CC_Gravity(Vector3 main, GameObject skill, float duration) // 중력 스킬이 사용 된 지점으로 몬스터 강제 위치
    {
        yield return new WaitForSeconds(0);
        GameObject area = Instantiate(skill,new Vector3(main.x,1f,main.z),transform.rotation);
        area.GetComponent<Skill_stat>().duration_time = duration;
        Destroy(area, duration);
    }

    public void StartGravity(Vector3 main, GameObject skill, float duration)
    {
        StartCoroutine(CC_Gravity(main, skill, duration));
    }

    public IEnumerator Buff_DamageUp(float time)
    {
        Weapon_gun.instance.FinalDamage = 2.0f;

        yield return new WaitForSeconds(time);

        Weapon_gun.instance.FinalDamage = 1.0f;
    }

    public void StartDamageBuff(float time)
    {
        StartCoroutine(Buff_DamageUp(time));
    }
}
