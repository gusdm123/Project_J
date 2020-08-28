﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour , IDamageable
{
    public float startingHealth;
    protected float health;
    protected bool dead;

    public void Start()
    {
        health = startingHealth;
    }

    public void TakeHit(float damage, RaycastHit hit)
    {
        health -= damage;

        if (health <= 0) {
            Die();
        }
    }

    public void Die()
    {
        dead = true;
        Destroy(gameObject);
    }
}
