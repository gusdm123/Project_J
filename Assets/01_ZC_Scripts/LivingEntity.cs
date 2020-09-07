using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour , IDamageable
{
    public float startingHealth;
    public GameObject hpBarPrefab; 

    private Slider hpSlider;
    private Canvas hpCanvas;
    private GameObject hpBar;

    protected float health;
    protected bool dead;

    public bool burnCheck = false; // 화상 상태이상 인지 체크하는 변수
    public bool paralysisCheck = false; // 마비 상태이상 인지 체크하는 변수
    public bool freezingCheck = false; // 빙결 상태이상 인지 체크하는 변수
    public bool darknessCheck = false; // 암흑 상태이상 인지 체크하는 변수
    public bool knockBackCheck = false; // 넉백 상태이상 인지 체크하는 변수
    public bool gravityCheck = false;

    public void Start()
    {
        health = startingHealth;
        hpCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>(); // 체력바 캔버스
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform); // 체력바 생성
        hpSlider = hpBar.GetComponentInChildren<Slider>();

        var _hpbar = hpBar.GetComponent<EnemyHealthBar>();
        _hpbar.targetTr = this.gameObject.transform;
    }

    public void TakeHit(float damage)
    {
        health -= damage * Weapon_gun.instance.FinalDamage;
        hpSlider.value = health / startingHealth;

        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        Destroy(hpBar);
        Destroy(gameObject);
    }
}
