using UnityEngine;
using System.Collections;
using System;

public class shieldPwrup : PwrupBase {

    public PwrupManager.PowerupType powerupType;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(powerupType);
    }

    public override void stopPwrup()
    {
        deactivateShield();
    }

    static public void activateShield()
    {
        GameObject shield = (GameObject)Instantiate(Resources.Load("prefabs/shield"), new Vector3(0,0,-33), Quaternion.identity);
    }

    static public void deactivateShield() // atm not needed
    {
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        Destroy(shield);
    }
}
