using UnityEngine;
using System.Collections;
using System;

public class downScalePwrup : PwrupBase {

    public override void startPwrup()
    {
        Narrow();
    }

    public override void stopPwrup()
    {
        ResetScale();
    }

    static public void Narrow()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(0.5f, 1, 1);
    }

    static public void ResetScale()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(1, 1, 1);
    }
}
