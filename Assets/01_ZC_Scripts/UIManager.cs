using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<UIManager>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static UIManager m_instance; // 싱글톤이 할당될 static 변수

    public GameObject gameOverCanvas; // 게임 오버시 띄울 캔버스

    public GameObject lootBackPack;
    public GameObject lootBackPackWayPoint;
    public GameObject hpCanvas;

    public GameObject floatingText;
    public GameObject floatingText_Crit;
    public void GameOverActive() // 게임 오버 캔버스를 활성화
    {
        gameOverCanvas.SetActive(true);
    }

    public void  GameRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetFloating(Transform vr, DamageManager.DamageType checkDamage,float damage) //몬스터의 위치, 총알의 공격력을 받아옴
    {
        if (floatingText == null)
        {
            Debug.Log("FloatingTxt Null");
        }

        if (checkDamage.critical == false)
        {
            GameObject textDamage = Instantiate(floatingText);
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(vr.transform.position);

            textDamage.transform.localPosition = uiPosition;
            textDamage.transform.SetParent(hpCanvas.gameObject.transform);
            textDamage.GetComponent<floatDamage>().print((checkDamage.damage * damage).ToString());
        }
        else
        {
            GameObject textDamage = Instantiate(floatingText_Crit);
            Vector3 uiPosition = Camera.main.WorldToScreenPoint(vr.transform.position);

            textDamage.transform.localPosition = uiPosition;
            textDamage.transform.SetParent(hpCanvas.gameObject.transform);
            textDamage.GetComponent<floatDamage>().print((checkDamage.damage * damage).ToString());
        }
    }
}
