using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : LivingEntity
{
    public float speed = 3;
<<<<<<< HEAD
    bool moveCheck = false;
    bool wallCheck = false;
=======

    private bool moveCheck = false; // 움직임
    private bool wallCheck = false; // 벽에 닿았는지
>>>>>>> 948cacd9b2cbfab4ee67acaa8b96bc0246935261

    public float paralysis_time = 0;
    public float freezing_time = 0;
    public float gravity_time = 0;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }
    // Update is called once per frame
    void Update()
    {
        CheckCCTime();

<<<<<<< HEAD
        if (!moveCheck && wallCheck == false)
=======
        if (!moveCheck && knockBackCheck == false && wallCheck == false)
>>>>>>> 948cacd9b2cbfab4ee67acaa8b96bc0246935261
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
}
