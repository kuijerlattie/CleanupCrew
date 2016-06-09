using UnityEngine;
using System.Collections;

public class upScalePwrup : PwrupBase {

    public override void startPwrup()
    {
        Wider();
    }

    public override void stopPwrup()
    {
        ResetScale();
    }

    static public void Wider()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(1.5f, 1, 1);
    }

    static public void ResetScale()
    {
        GameObject paddle;

        paddle = FindObjectOfType<PaddleControls>().gameObject;
        paddle.transform.localScale = new Vector3(1, 1, 1);
    }
}
