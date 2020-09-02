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

    public void Start()
    {
        health = startingHealth;
        hpCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>(); // 체력바 캔버스
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform); // 체력바 생성
        hpSlider = hpBar.GetComponentInChildren<Slider>();

        var _hpbar = hpBar.GetComponent<EnemyHealthBar>();
        _hpbar.targetTr = this.gameObject.transform;
    }

    public void TakeHit(float damage, Collider hit)
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
