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
        GameObject shield = (GameObject)Instantiate(Resources.Load("prefabs/Shield_Fixed"), new Vector3(-23,0,-31), Quaternion.identity);
    }

    static public void deactivateShield() // atm not needed
    {
        GameObject shield = GameObject.FindGameObjectWithTag("Shield");
        Destroy(shield);
    }
}
