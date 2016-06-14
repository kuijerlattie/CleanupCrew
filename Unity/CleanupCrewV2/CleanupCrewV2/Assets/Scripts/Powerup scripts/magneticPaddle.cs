using UnityEngine;
using System.Collections;
using System;

public class magneticPaddle : PwrupBase {

    public PwrupManager.PowerupType powerupType;

    public override void startPwrup()
    {
        PwrupManager manager = FindObjectOfType<PwrupManager>();

        manager.ActivatePwrup(powerupType);
    }

    public override void stopPwrup()
    {
        
    }

    static public void magnetic()
    {
        GameObject ball = FindObjectOfType<BallScript>().gameObject;
        GameObject paddle = FindObjectOfType<PaddleControls>().gameObject;

        ball.GetComponent<Rigidbody>().velocity = (paddle.transform.position - ball.transform.position).normalized;        
    }

}
