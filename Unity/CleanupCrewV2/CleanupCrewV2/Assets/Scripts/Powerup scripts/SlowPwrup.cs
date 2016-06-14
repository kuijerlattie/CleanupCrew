using UnityEngine;
using System.Collections;
using System;

public class SlowPwrup : PwrupBase {

    public PwrupManager.PowerupType powerupType;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(powerupType);
    }

    public override void stopPwrup()
    {
        ResetSpeed();
    }

    static public void Slow()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for(int i = 0; i < speed.Length; i++)
        {
            if(speed[i].GetComponent<BlobScript>() != null)
                speed[i].targetSpeed *= 0.5f;
        }
    }

    static public void ResetSpeed()
    {
        FixedSpeed[] speed;
        speed = FindObjectsOfType<FixedSpeed>();

        for (int i = 0; i < speed.Length; i++)
        {
            if (speed[i].GetComponent<BlobScript>() != null)
                speed[i].targetSpeed *= 2;
        }
    }
}
