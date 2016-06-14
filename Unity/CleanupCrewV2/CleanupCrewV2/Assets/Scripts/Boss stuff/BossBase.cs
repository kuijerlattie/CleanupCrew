﻿using UnityEngine;
using System.Collections;

public class BossBase : MonoBehaviour {

    public bool alive = true;
    public int hitpoints = 1;
    public int maxhitpoints = 10;
    public int phase = 0;
    public bool invincible = true;

    public void Hit(int damage)
    {
        if (invincible == false)
        {
            hitpoints -= damage;
            if (hitpoints <= 0)
            {
                
            }
        }
        
    }

    public void OnDestroy()
    {
        if (!GameManager.IsQuitting)
        {
            EventManager.TriggerEvent("BossDied", this.gameObject);
        }
    }

    public float hitpointsForHud
    { get { return 1 / maxhitpoints * hitpoints; } }
}
