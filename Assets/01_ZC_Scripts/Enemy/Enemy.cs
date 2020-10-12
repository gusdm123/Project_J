using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : LivingEntity
{
    public float speed = 3;

    private bool moveCheck = false; // 움직임
    private bool wallCheck = false; // 벽에 닿았는지

    public float paralysis_time = 0;
    public float freezing_time = 0;
    public float gravity_time = 0;

    public float damage = 10f;

    private void Start()
    {
        health = startingHealth;
        hpCanvas = GameObject.Find("HPCanvas").GetComponent<Canvas>(); // 체력바 캔버스
        hpBar = Instantiate<GameObject>(hpBarPrefab, hpCanvas.transform); // 체력바 생성

        hpSlider = hpBar.GetComponentInChildren<Slider>();

        var _hpbar = hpBar.GetComponent<EnemyHealthBar>();
        _hpbar.targetTr = this.gameObject.transform;

        StartCoroutine(CoroutineUpdate());
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;      
    }

    // Update is called once per frame
    IEnumerator CoroutineUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            CheckCCTime();

            if (!moveCheck && knockBackCheck == false && wallCheck == false)
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);               
            }
            else if (wallCheck == true)
            {
                Collider[] targets = Physics.OverlapSphere(transform.position, 13f);

                if (targets.Length > 0)
                {
                    for (int i = 0; i < targets.Length; i++)
                    {
                        if (targets[i] != null)
                        {
                            if (targets[i].tag == "Player")
                            {

                                targets[i].GetComponent<LivingEntity>().TakeHit(damage);
                                yield return new WaitForSeconds(2f);
                            }
                        }
                    }                 
                }
            }          
        }
    }

    private void CheckCCTime()
    {
        if (paralysis_time > 0)
        {
            moveCheck = true;
            paralysis_time -= Time.deltaTime;

            if (paralysis_time < 0)
                paralysis_time = 0;
        }

        if (freezing_time > 0)
        {
            moveCheck = true;
            freezing_time -= Time.deltaTime;

            if (freezing_time < 0)
                freezing_time = 0;
        }

        if (gravity_time > 0)
        {
            moveCheck = true;
            gravity_time -= Time.deltaTime;

            if (gravity_time < 0)
                gravity_time = 0;
        }

        if (paralysis_time + freezing_time + gravity_time <= 0)
            moveCheck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Wall") {
            wallCheck = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "Wall")
        {
            wallCheck = false;
        }
    }

    protected override void Die()
    {
        dead = true;

        if (Weapon_gun.instance.corpseExplosionCheck == true)
        {
            GameObject corpseExplosion = Instantiate(Weapon_gun.instance.corpseExplosionPrefab, gameObject.transform.position, gameObject.transform.rotation);
            corpseExplosion.SetActive(true);
        }

        Destroy(hpBar);
        Destroy(gameObject);
    }
}
