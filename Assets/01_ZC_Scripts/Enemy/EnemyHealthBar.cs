﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform targetTr;

    private Canvas canvas;
    private Camera hpCamera;
    private RectTransform rectParent;
    private RectTransform rectHp;

    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(0.5f, 0f, 0f);

        canvas = GetComponentInParent<Canvas>();
        hpCamera = canvas.worldCamera;
        rectParent = canvas.GetComponent<RectTransform>();
        rectHp = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        var screenPos = Camera.main.WorldToScreenPoint(targetTr.position + offset); // 몬스터의 월드 3d좌표를 스크린좌표로 변환

        if (screenPos.z < 0.0f)
        {
            screenPos *= -1.0f;
        }

        var localPos = Vector2.zero;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rectParent, screenPos, hpCamera, out localPos); // 스크린 좌표를 다시 체력바 UI 캔버스 좌표로 변환

        rectHp.localPosition = localPos; // 체력바 위치조정
    }
}
