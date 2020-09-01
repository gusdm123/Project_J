using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Projectile_normal : MonoBehaviour
{
    public LayerMask collsionMask;
    public GameObject hitEffect;

    float speed = 10f;
    float damage = 1;

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        float moveDistance = speed * Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            IDamageable damageableObject = other.GetComponent<IDamageable>();

            if (damageableObject != null)
            {
                damageableObject.TakeHit(damage, other);
            }

            GameObject playhitEffect = Instantiate(hitEffect, transform.position, transform.rotation);
            playhitEffect.SetActive(true);
            Destroy(playhitEffect, 1.0f);

            GameObject.Destroy(gameObject);
        }
    }
}
