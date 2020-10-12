using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivingEntity : MonoBehaviour , IDamageable
{
    public float startingHealth;
    public GameObject hpBarPrefab; 

    protected Slider hpSlider;
    protected Canvas hpCanvas;
    protected GameObject hpBar;

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
        float deadlyDamage = Random.Range(0f, 100f);
        health -= damage * Weapon_gun.instance.FinalDamage;
        hpSlider.value = health / startingHealth;

        //deadlyDamage < Weapon_gun.instance.deadlyAttack; // 보류

        if (health <= 0) {
            Die();
        }
    }

    protected virtual void Die()
    {
       
    }
}
