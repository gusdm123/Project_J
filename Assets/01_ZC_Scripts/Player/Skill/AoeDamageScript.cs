using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AoeDamageScript : MonoBehaviour
{
    public float damage = 1.0f;
    public float duration = 0.2f;

    private void Update()
    {
        duration -= Time.deltaTime;

        if (duration < 0f)
            Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamageable damageableObject = other.GetComponent<IDamageable>();

        if (damageableObject != null)
        {
            damageableObject.TakeHit(damage);
        }
    }
}
