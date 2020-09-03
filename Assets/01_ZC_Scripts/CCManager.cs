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
        if (target.GetComponent<LivingEntity>().burnCheck == false) {

            target.GetComponent<LivingEntity>().burnCheck = true;

            while(true){
                yield return new WaitForSeconds(effectCycle);
                if (target != null)
                {
                    target.GetComponent<LivingEntity>().TakeHit(damage);
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
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public IEnumerator CC_Darkness(Collider target, float duration) // 암흑 지속시간 동안 이동방향 반대로
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public IEnumerator CC_KnockBack(Collider target, float duration, float damage, float effectCycle) // 넉백 적을 뒤로 밀어냄 
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }

    public IEnumerator CC_Gravity(Collider target, Collider skill, float duration, float damage, float effectCycle) // 중력 스킬이 사용 된 지점으로 몬스터 강제 위치
    {
        yield return new WaitForSeconds(Time.deltaTime);
    }
}
