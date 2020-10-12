using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetDataManager : MonoBehaviour
{
    public static AssetDataManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<AssetDataManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    struct SkillAsset
    {
        public int s_Num;
        public string s_Link;
    }

    private static AssetDataManager m_instance; // 싱글톤이 할당될 static 변수

    private SkillAsset[] skillList = new SkillAsset[8];

    private void Awake()
    {
        skillList[0].s_Num = 1001;
        skillList[0].s_Link = "Assets/01_ZC_Prefab/Bullet orb01_blue.prefab";

        skillList[1].s_Num = 1002;
        skillList[1].s_Link = "Assets/01_ZC_Prefab/Buff test DamageUp.prefab";

        skillList[2].s_Num = 1003;
        skillList[2].s_Link = "Assets/01_ZC_Prefab/Bullet BurnArrow.prefab";

        skillList[3].s_Num = 1004;
        skillList[3].s_Link = "Assets/01_ZC_Prefab/Bullet DarknessArrow.prefab";

        skillList[4].s_Num = 1005;
        skillList[4].s_Link = "Assets/01_ZC_Prefab/Bullet FreezingArrow.prefab";

        skillList[5].s_Num = 1006;
        skillList[5].s_Link = "Assets/01_ZC_Prefab/Bullet GravityArrow.prefab";

        skillList[6].s_Num = 1007;
        skillList[6].s_Link = "Assets/01_ZC_Prefab/Bullet KnockArrow.prefab";

        skillList[7].s_Num = 1008;
        skillList[7].s_Link = "Assets/01_ZC_Prefab/Bullet MabiArrow.prefab";
    }

    public string SkillAssetSearch(int skillNum)
    {
        string skillLink = null;
        for (int i = 0; i < skillList.Length; i++)
        {
            if (skillList[i].s_Num == skillNum)
            {
                skillLink = skillList[i].s_Link;
                break;
            }
        }
        return skillLink;
    }
}
