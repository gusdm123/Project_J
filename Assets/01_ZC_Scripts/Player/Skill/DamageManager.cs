using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<DamageManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    public struct DamageType
    {
        public float damage;
        public bool critical;
    }

    private static DamageManager m_instance; // 싱글톤이 할당될 static 변수

    public DamageType CharacterDamage(Transform target)
    {
        DamageType newDamage;
        newDamage.damage = 1;
        newDamage.critical = false;

        if (Random.Range(0, 100) > 70)
        {
            newDamage.critical = true;
            newDamage.damage = 2;
        }

        return newDamage;
    }
}

