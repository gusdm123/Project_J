using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float speed = 3;
    bool moveCheck = false;
    bool wallCheck = false;

    public float paralysis_time = 0;
    public float freezing_time = 0;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        CheckCCTime();

        if (!moveCheck && wallCheck == false)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
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

        if (paralysis_time + freezing_time <= 0)
            moveCheck = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Wall") {
            wallCheck = true;
        }
    }
}
