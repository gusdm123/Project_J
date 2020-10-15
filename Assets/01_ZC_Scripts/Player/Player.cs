using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Player : LivingEntity
{
    public static Player instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<Player>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private Touch tempTouchs;
    private Vector3 touchedPos;

    private bool touchOn;

    private static Player m_instance; // 싱글톤이 할당될 static 변수

    private Camera viewCamera; // 메인 카메라 
    private WaitForSeconds time_deltatime; // deltaTime 변수

    public Collider auto_Target; // 자동전투 타겟 Enemy

    public PlayerController controller;

    public bool auto_ModeCheck;

    private void Start()
    {
        health = startingHealth;
        hpCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>(); // 체력바 캔버스
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform); // 체력바 생성
        hpSlider = hpBar.GetComponentInChildren<Slider>();

        var _hpbar = hpBar.GetComponent<EnemyHealthBar>();
        _hpbar.targetTr = this.gameObject.transform;

        controller = GetComponent<PlayerController>(); // 플레이어를 조작해주는 컴포넌트

        time_deltatime = new WaitForSeconds(Time.deltaTime); // 미리 선언을 해줌으로써 지속적인 오버헤드를 줄여준다.
        viewCamera = Camera.main; // 카메라에 메인 카메라를 담아준다.

        StartCoroutine(CoroutineUpdate()); // 코루틴을 이용한 업데이트를 통해서 유동적인 업데이트 관리 및 최적화, 맨 마지막에 시작해 주어야 초기화 오류가 걸리지 않는다.
    }

    private void SearchNewTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, 13f);
        float minDistance = 1000f;

        if (targets.Length > 0)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].tag == "Enemy")
                {
                    if (minDistance > Vector3.Distance(transform.position, targets[i].transform.position))
                    {
                        minDistance = Vector3.Distance(transform.position, targets[i].transform.position);
                        auto_Target = targets[i];
                    }
                }
            }
        }
    }

    private void CheckCoolDown()
    {
        if (Weapon_gun.instance.projectileCoolDownCheck[1] == false)
        {
            Weapon_gun.instance.SetFirstprojectile();
        }
        else if (Weapon_gun.instance.projectileCoolDownCheck[2] == false)
        {
            Weapon_gun.instance.SetSecondprojectile();
        }
        else if (Weapon_gun.instance.projectileCoolDownCheck[3] == false)
        {
            Weapon_gun.instance.SetThirdprojectile();
        }
        else if (Weapon_gun.instance.projectileCoolDownCheck[4] == false)
        {
            Weapon_gun.instance.SetForthprojectile();
        }
    }

    public GameObject aoeMuzzle;

    IEnumerator CoroutineUpdate() // 업데이트 대체 함수
    {
        while (true)
        {

#if UNITY_EDITOR
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                yield return time_deltatime;
            }
            else if (auto_ModeCheck == true)
            {
                if (auto_Target != null)
                {
                    if (Weapon_gun.instance.knowSkillUsing == false)
                    {
                        CheckCoolDown();
                    }
                    controller.LookAt(auto_Target.transform.position);
                    Weapon_gun.instance.Shoot();
                }
                else
                {
                    SearchNewTarget();
                }
            }
            else
            {
                touchOn = false;
                Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;

                groundPlane.Raycast(ray, out rayDistance);

                if (Input.GetMouseButton(0))
                {
                    Vector3 point = ray.GetPoint(rayDistance);
                    controller.LookAt(point);
                    aoeMuzzle.transform.LookAt(point);
                    Weapon_gun.instance.Shoot();
                }
            }
#else
            if (Input.touchCount > 0)
            {    //터치가 1개 이상이면.
                for (int i = 0; i < Input.touchCount; i++)
                {
                    tempTouchs = Input.GetTouch(i);

                    if (tempTouchs.position.y > Screen.height / 4)
                    {
                        touchOn = true;
                        Ray ray = viewCamera.ScreenPointToRay(tempTouchs.position);
                        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                        float rayDistance;

                        groundPlane.Raycast(ray, out rayDistance);

                        Vector3 point = ray.GetPoint(rayDistance);
                        controller.LookAt(point);
                        aoeMuzzle.transform.LookAt(point);
                        Weapon_gun.instance.Shoot();
                        break;   //한 프레임(update)에는 하나만.                       
                    }
                    else
                    {
                        break;
                    }
                }
            }
#endif        
            yield return null;
        }
    }

    protected override void Die()
    {
        UIManager.instance.GameOverActive();
    }
}
