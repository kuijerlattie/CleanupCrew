using UnityEngine;
using System.Collections;
using System;

public class shootingPwrup : PwrupBase {

    public PwrupManager.PowerupType type;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(type);
    }

    public override void stopPwrup()
    {
        disallowShooting();
    }

    static public void allowShooting()
    {
        paddleShooting shoot = FindObjectOfType<paddleShooting>();
        shoot.canShoot = true;
    }

    static public void disallowShooting()
    {
        paddleShooting shoot = FindObjectOfType<paddleShooting>();
        shoot.canShoot = false;
    }

}
