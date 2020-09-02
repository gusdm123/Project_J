using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : LivingEntity
{
    Camera viewCamera; // 메인 카메라 
    WaitForSeconds time_deltatime; // deltaTime 변수

    PlayerController controller;

    private void Start()
    {
        controller = GetComponent<PlayerController>(); // 플레이어를 조작해주는 컴포넌트

        time_deltatime = new WaitForSeconds(Time.deltaTime); // 미리 선언을 해줌으로써 지속적인 오버헤드를 줄여준다.
        viewCamera = Camera.main; // 카메라에 메인 카메라를 담아준다.

        StartCoroutine(CoroutineUpdate()); // 코루틴을 이용한 업데이트를 통해서 유동적인 업데이트 관리 및 최적화, 맨 마지막에 시작해 주어야 초기화 오류가 걸리지 않는다.
    }

    IEnumerator CoroutineUpdate() // 업데이트 대체 함수
    {
        while (true)
        {
            if (EventSystem.current.IsPointerOverGameObject() == true)
            {
                yield return time_deltatime;
            }
            else
            {
                Ray ray = viewCamera.ScreenPointToRay(Input.mousePosition);
                Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
                float rayDistance;

                groundPlane.Raycast(ray, out rayDistance);

                if (Input.GetMouseButton(0))
                {
                    Vector3 point = ray.GetPoint(rayDistance);
                    controller.LookAt(point);
                    Weapon_gun.instance.Shoot();
                }
            }

            yield return time_deltatime;
        }
    }
}
