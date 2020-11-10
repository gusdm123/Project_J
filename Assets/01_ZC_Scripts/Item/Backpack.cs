using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public static Backpack instance
    {
        get
        {
            // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
            if (m_instance == null)
            {
                // 씬에서 GameManager 오브젝트를 찾아 할당
                m_instance = FindObjectOfType<Backpack>();
            }

            // 싱글톤 오브젝트를 반환
            return m_instance;
        }
    }

    private static Backpack m_instance; // 싱글톤이 할당될 static 변수

    private Vector3 onPosition; // 오른쪽으로 나왔을때 포지션
    private Vector3 offPosition; // 들어갈때 포지션
    private bool onOffCheck; // 켜지고 꺼지는지 체크

    private float moveSpeed; // 움직일 속도
    private float speed = 2f;

    public int itemCount = 0;
    public int gemStoneCount = 0;
    public int gold = 0;

    // Start is called before the first frame update
    void Start()
    {
        onPosition = new Vector3(150, 720,0);
        offPosition = new Vector3(-200, 720,0);

        onOffCheck = false;
    }

    public void ItemDrop()
    {
        gold += 1000;
        itemCount++;
        gemStoneCount++;
    }

    // Update is called once per frame
    void Update()
    {
        if (itemCount > 0) // 켜졌을 경우
        {
            moveSpeed = Mathf.Lerp(transform.position.x, onPosition.x, Time.deltaTime * speed);
            transform.position = new Vector3(moveSpeed, transform.position.y, 0);
        }
        else
        {
            moveSpeed = Mathf.Lerp(transform.position.x, offPosition.x, Time.deltaTime * speed);
            transform.position = new Vector3(moveSpeed, transform.position.y, 0);
        }
    }
}
