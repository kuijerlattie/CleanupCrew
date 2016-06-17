﻿using UnityEngine;
using System.Collections;

public abstract class BossBase : MonoBehaviour {

    public bool alive = true;
    public int hitpoints = 1;
    public int maxhitpoints = 10;
    public int phase = 0;
    public bool invincible = true;

    ParticleSystem spawnparticle;

    public void Hit(int damage)
    {
        if (!invincible && GameManager.instance.CurrentGamestate == GameManager.gamestate.Boss)
        {
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                Die();
            }
            else
                OnHit();
        }
        
    }

    public abstract void OnHit();

    public void OnDestroy()
    {
        if (!GameManager.IsQuitting)
        {
            EventManager.TriggerEvent("BossDied", this.gameObject);
        }
    }

    public abstract void Die();

    public float hitpointsForHud
    { get { return 1 / maxhitpoints * hitpoints; } }
}
